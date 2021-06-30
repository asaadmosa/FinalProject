
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Todos.DataModel;

namespace Todos.Contracts.Repositories
{
    public interface ITodosRepository
    {
        Task <List<TodoList>> GetAllGroups(bool includeItems);
        Task<TodoList> GetTodoGroup(Guid todoGroupId);
        Task DeleteGroup(Guid todoGroupId);
        Task<TodoList> ModifyTodoGroup(TodoList todoGroup);
        Task<TodoList> AddTodoGroup(TodoList todoGroup);
        Task<List<TodoItem>> GetTodoItems(Guid todoGroupId);
        Task<TodoItem> GetTodoItem(Guid todoGroupId,Guid todoItemId);
        Task DeleteItem(Guid todoGroupId, Guid todoItemId);

        


        /*
        Task<Course> AddCourse(Course value);
        Task<Instructor> AddInstructor(Instructor value);
        Task DeleteCourse(Guid id);
        Task DeleteInstructor(Guid id);
        Task<List<Course>> GetAllCourses();
        Task<List<Instructor>> GetAllInstructors();
        Task ModifyCourse(Guid id, Course value);
        Task ModifyInstructor(Guid id, Instructor value);
        */
    }
}
