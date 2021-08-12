using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Todos.Contracts.Repositories;
using Todos.DataAccess;
using Todos.DataModel;

namespace Todos.Services.Repositories
{
    public class SqlTodosRepository : ITodosRepository
    {
        private readonly TodosDataContext _todoDataContext;

        public SqlTodosRepository(TodosDataContext todoDataContext)
        {
            this._todoDataContext = todoDataContext;
        }

        //get all lists
        public async Task<List<TodoList>> GetAllGroups(bool includeItems)
        {
            if (!includeItems)
            {
                var items = await _todoDataContext.TodoGroups.ToListAsync();
                return items;
            }
            else
            {
                var items = await _todoDataContext.TodoGroups.Include(x=>x.Items).ToListAsync();
                return items;
            }
        }

        //get the list count
        public Task<int> GetListCount()
        {
                var count = _todoDataContext.TodoGroups.ToListAsync().Result.Count;
                return Task.FromResult(count);           
        }

        //get all items by listI d
        public async Task<List<TodoItem>> GetTodoItems(Guid todoGroupId)
        {
            var group = GetTodoGroup(todoGroupId);
            List<TodoItem> items = (await group).Items;
            return items;
        }

        //get items count
        public Task<int> GetItemsCount()
        {
            var lists = GetAllGroups(true);
            int count = 0;
            lists.Result.ForEach(list => count+=list.Items.Count);
            return Task.FromResult(count);
        }

        //get active items count
        public Task<int> GetActiveItemsCount()
        {
            var lists = GetAllGroups(true);
            int count = 0;
            lists.Result.ForEach(list => count += list.Items.FindAll(item =>item.IsCompleted==false).Count);
            return Task.FromResult(count);
        }

        public Task DeleteGroup(Guid todoGroupId)
        {
            _todoDataContext.TodoGroups.Remove(GetTodoGroup(todoGroupId).Result);
            _todoDataContext.SaveChangesAsync();
            return Task.CompletedTask;
        }



        public Task DeleteItem(Guid todoGroupId,Guid todoItemId)
        {
            if (GetTodoItem(todoGroupId,todoItemId).Result.Id != todoItemId)
                throw new ArgumentOutOfRangeException(nameof(todoItemId));

            _todoDataContext.TodoItems.Remove(GetTodoItem(todoGroupId, todoItemId).Result);
            _todoDataContext.SaveChangesAsync();
            return Task.CompletedTask;
        }


        public async Task<TodoList> GetTodoGroup(Guid todoGroupId)
        {
            var todogroup = (await GetAllGroups(true)).Find(x => x.Id.Equals(todoGroupId));
            return todogroup;
        }

        public async Task<TodoItem> GetTodoItem(Guid todoGroupId,Guid todoItemId)
        {
            var todoItem = (await GetTodoItems(todoGroupId)).Find(x=>x.Id.Equals(todoItemId));
            return todoItem;
        }

        public Task<TodoList> AddTodoGroup(TodoList todoGroup)
        {
            _todoDataContext.AddAsync(todoGroup);
            _todoDataContext.SaveChangesAsync();
            return Task.FromResult(todoGroup);
        }

        public Task<TodoList> ModifyTodoGroup(TodoList todoGroup)
        {
            _todoDataContext.Update(todoGroup);
            _todoDataContext.SaveChangesAsync();
            return Task.FromResult(todoGroup);
        }
    }
}
