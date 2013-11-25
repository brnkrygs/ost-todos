using System;

namespace Presentation.Web.Models.Input
{
    public class TodoInput
    {
        public string Title { get; set; }

        public bool Completed { get; set; }

        public DateTime TodoTime { get; set; }

        public string Description { get; set; }

        public string Type { get; set; }
    }
}