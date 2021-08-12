using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Todos.Contracts.Repositories;
using Todos.DataModel;

namespace Todos.Services.Repositories
{
    public class JsonTodosRepository : ITodosRepository
    {
        private readonly string jsonUrl;

        public JsonTodosRepository(IConfiguration configuration)
        {
            this.jsonUrl = configuration["jsonUrl"];
        }

        public Task<TodoList> AddTodoGroup(TodoList todoGroup)
        {
            throw new NotImplementedException();
        }

        public Task DeleteGroup(int todoGroupId)
        {
            throw new System.NotImplementedException();
        }

        public Task DeleteGroup(Guid todoGroupId)
        {
            throw new NotImplementedException();
        }

        public Task DeleteItem(Guid todoGroupId, Guid todoItemId)
        {
            throw new NotImplementedException();
        }

        public Task<int> GetActiveItemsCount()
        {
            throw new NotImplementedException();
        }

        public async Task<List<TodoList>> GetAllGroups(bool includeItems)
        {
            await Task.Delay(1000);
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", jsonUrl);
            var jsonContent = File.ReadAllText(filePath);
            return JsonConvert.DeserializeObject<List<TodoList>>(jsonContent);
        }

        public Task<int> GetItemsCount()
        {
            throw new NotImplementedException();
        }

        public Task<int> GetListCount()
        {
            throw new NotImplementedException();
        }

        public Task<TodoList> GetTodoGroup(Guid todoGroupId)
        {
            throw new System.NotImplementedException();
        }

        public Task<TodoItem> GetTodoItem(Guid todoGroupId, Guid todoItemId)
        {
            throw new System.NotImplementedException();
        }

        public Task<List<TodoItem>> GetTodoItems(Guid todoGroupId)
        {
            throw new System.NotImplementedException();
        }

        public Task<TodoList> ModifyTodoGroup(TodoList todoGroup)
        {
            throw new NotImplementedException();
        }
    }
}
