using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Todos.DataModel
{
    public class TodoList
    {
        public Guid Id { get; set; }
        public string Capation { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public string Color { get; set; } 
        public List<TodoItem> Items { get; set; }
        [JsonConstructor]
        public TodoList(Guid id, string capation, string description, string imageUrl, string color, List<TodoItem> items)
        {
            Id = id;
            Capation = capation;
            Description = description;
            ImageUrl = imageUrl;
            Color = color;
            Items = items;
        }

        TodoList()
        {
            this.Items = new List<TodoItem>();
        }
    }
}
