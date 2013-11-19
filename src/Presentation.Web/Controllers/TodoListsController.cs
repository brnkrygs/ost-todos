using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Core.Domain.Model;
using Core.Domain.Model.TodoLists;
using Core.Domain.Model.Todos;
using Presentation.Web.Models.Display;
using Presentation.Web.Models.Input;

namespace Presentation.Web.Controllers
{
    public class TodoListsController : ControllerBase
    {
        private IRepository<TodoList> _repo;

        public TodoListsController(IRepository<TodoList> repo)
        {
            _repo = repo;
        }

        [Authorize]
        public IEnumerable<TodoListDisplay> Get()
        {
            var todos = _repo.FindBy(x => x.Owner == LoadUser());
            var displays = todos.Select(x => new TodoListDisplay()
                {
                    Name = x.Name,
                    Id = x.Id,
                    Todos = x.Todos.Select(t => new TodoDisplay() { Id = t.Id, Title = t.Title, Completed = t.Completed , OrderNum = t.OrderNum}).ToList()
                }).ToList();
            return displays;
        }

        [Authorize]
        [HttpPost]
        public HttpResponseMessage Post(TodoListInput list)
            {
            var entity = new TodoList()
                {
                    Name = list.Name,
                    Owner = LoadUser()
                };
            _repo.Store(entity);
            return Request.CreateResponse(HttpStatusCode.OK, new TodoListDisplay() { Name = entity.Name, Id = entity.Id });
        }

        [Authorize]
        [HttpPost]
        public HttpResponseMessage MergeList(long Id, long Id2)
        {
            var list = _repo.Get(Id);
            var list2 = _repo.Get(Id2);
            foreach (var todo in list2.Todos)
            {
                list.AddTodo(todo);
            }
            _repo.Store(list);
            _repo.Delete(list2);
            return Request.CreateResponse(HttpStatusCode.OK,
                                          list.Todos.Select(
                                              t =>
                                              new TodoDisplay() {Id = t.Id, Title = t.Title, Completed = t.Completed, OrderNum = t.OrderNum})
                                              .ToList());
        }



        [Authorize]
        [HttpDelete]
        public HttpResponseMessage Delete(long Id)
        {
            var list = _repo.Load(Id);
            _repo.Delete(list);
            return Request.CreateResponse(HttpStatusCode.NoContent);
        }

        [Authorize]
        [HttpPost]
        public HttpResponseMessage Todos(long Id, TodoInput todoInput)
        {
            
            var list = _repo.Get(Id);
            var orderNum = 1;
            if (list.Todos.Count > 0)
            {
                orderNum = list.Todos.Max(x => x.OrderNum)+1;
            }
            var todo = new Todo() { Title = todoInput.Title, Completed = false , OrderNum = orderNum};
            list.AddTodo(todo);
            _repo.Store(list);
                return Request.CreateResponse(HttpStatusCode.OK, new TodoDisplay { Title = todoInput.Title, Id = todo.Id, Completed = false , OrderNum = orderNum});
            
        }

        [Authorize]
        [HttpGet]
        public IEnumerable<TodoDisplay> Todos(long Id)
        {
            var list = _repo.Get(Id);
            return
                list.Todos.Select(t => new TodoDisplay() { Id = t.Id, Title = t.Title, Completed = t.Completed , OrderNum = t.OrderNum}).ToList();
            ;
        }
    }
}