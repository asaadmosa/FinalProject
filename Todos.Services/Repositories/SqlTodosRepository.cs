using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
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
        public async Task<List<TodoList>> GetAllGroups(bool includeItems)
        {
            //var r = ReadFromMemory();
            //_todoDataContext.TodoGroups.AddRange(r);
            //await _todoDataContext.SaveChangesAsync();

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
/*
        private static List<TodoGroup> ReadFromMemory()
        {
            var TodoGroup = new List<TodoGroup>();
            var globalCounter = 1;
            for (int i = 0; i < 5; i++)
            {
                TodoGroup group = new()
                {
                    //Id = i,
                    Name = $"Group No.{i}"
                };
                for (int j = 0; j < 3; j++)
                {
                    group.Items.Add(new TodoItem
                    {
                        //Id = globalCounter,
                        Name = $"Todo {j} Group {i}",
                        Description = "Desc",
                        GroupId = i,
                        IsCompleted = false
                    });
                    globalCounter++;
                }
                TodoGroup.Add(group);
            }
            return TodoGroup;
        }
*/
        public async Task<List<TodoItem>> GetTodoItems(Guid todoGroupId)
        {
            var group = GetTodoGroup(todoGroupId);
            List<TodoItem> items = (await group).Items;
            return items;
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
