﻿using System;
using Core.Domain.Model.TodoLists;

namespace Core.Domain.Model.Todos
{
    public class Todo : EntityBase<Todo>
    {
        public virtual string Title { get; set; }

        public virtual bool Completed { get; set; }

        public virtual string Description { get; set; }

        public virtual string Type { get; set; }

        public virtual DateTime TodoTime { get; set; }

        public virtual TodoList List { get; set; }
    }
}
