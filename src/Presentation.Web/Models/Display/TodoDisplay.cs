using System;

namespace Presentation.Web.Models.Display
{
    public class TodoDisplay
    {
        public bool Completed { get; set; }

        public DateTime TodoTime { get; set; }

        public string Description { get; set; }

        public string Type { get; set; }

        public string Title { get; set; }

        public long Id { get; set; }
    }
}