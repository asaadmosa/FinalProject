using System;

namespace Todos.DataModel
{
    public class TodoItem
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsCompleted { get; set; }

        public TodoList CurrentList { get; set; }
        public Guid CurrentListId { get; set; }
        public TodoItem() { }
    }
}
