using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Todos.DataModel;
using Todos.WebTodoApi.Models.Dtos;

namespace Todos.WebTodoApi.Mapers
{
    public class TodoGroupMapper
    {
        public static TodoGroupDto Map(TodoGroup group)
        {
            TodoGroupDto dto = new(group.Id, group.Name, group.Items.Select(x => x.Name).ToList());
            return dto;
        }
    }
}
