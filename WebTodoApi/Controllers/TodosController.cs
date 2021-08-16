using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Todos.Contracts.Repositories;
using Todos.DataModel;

namespace Todos.WebTodoApi.Controllers
{
    
    [Route("todos")]//the route is todos
    [ApiController]
    [EnableCors()]
    public class TodosController : ControllerBase
    {
        private readonly ITodosRepository _todosRepository;
        public TodosController(ITodosRepository todosRepository)
        {
            _todosRepository = todosRepository;
        }


        //get the number of the lists
        [HttpGet("TodoGroup/listsNumber")]
        public async Task<ActionResult<int>> GetListsNumber()
        {
            try
            {
                var result = await _todosRepository.GetListCount();
                return Ok(result);
            }
            catch
            {
                return NotFound();
            }
        }

        //get the total number of items
        [HttpGet("TodoGroup/ItemsNumber")]
        public async Task<ActionResult<int>> GetItemsCount()
        {
            try
            {
                var result = await _todosRepository.GetItemsCount();
                return Ok(result);
            }
            catch
            {
                return NotFound();
            }
        }


        //get the total number of Active items
        [HttpGet("TodoGroup/ActiveItemsNumber")]
        public async Task<ActionResult<int>> GetActiveItemssNumber()
        {
            try
            {
                var result =await _todosRepository.GetActiveItemsCount();
                return Ok(result);
            }
            catch
            {
                return NotFound();
            }
        }

        //Showing the collection of lists that you have
        [HttpGet()]
        public async Task<ActionResult<List<TodoList>>> GetGroups()
        {
            try
            {
                var result = await _todosRepository.GetAllGroups(true);
                return Ok(result);
            }
            catch
            {
                return NotFound();
            }
        }

        //Showing the list description, icon and items in this list
        [HttpGet("TodoGroup/{groupId}")]
        public async Task<ActionResult<TodoList>> GetGroupById(Guid groupId)
        {
            try
            {
                var result = await _todosRepository.GetTodoGroup(groupId);
                return Ok(result);
            }
            catch
            {
                return NotFound();
            }
        }

        //show all items of list
        [HttpGet("TodoGroup/{groupId}/Items")]
        public async Task<ActionResult<List<TodoItem>>> GetItemsOfGroup(Guid groupId)
        {
            try
            {
                var result = await _todosRepository.GetTodoItems(groupId);
                return Ok(result);
            }
            catch
            {
                return NotFound();
            }
        }

        //show an item of list
        [HttpGet("TodoGroup/{groupId}/Items/{itemId}")]
        public async Task<ActionResult<TodoItem>> GetItemsOfGroup(Guid groupId,Guid itemId)
        {
            try
            {
                var result = await _todosRepository.GetTodoItem(groupId,itemId);
                return Ok(result);
            }
            catch
            {
                return NotFound();
            }
        }
        
        //delete list
        [HttpDelete("TodoGroup/{groupId}")]
        public async Task<ActionResult> DeleteGroup(Guid groupId)
        {
            try
            {
                await _todosRepository.DeleteGroup(groupId);
                return Ok();
            }
            catch
            {
                return NotFound();
            }
        }

        //modify list
        [HttpPut]
        public async Task<ActionResult> ModifyTodoGroup([FromBody] TodoList todoGroup)
        {        
            try
            {
                var res = await _todosRepository.ModifyTodoGroup(todoGroup);
                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }

        //create new item
        [HttpPost("item")]
        public async Task<ActionResult<TodoItem>> AddTodoGroup([FromBody] TodoItem todoItem)
        {
            try
            {
                var res = await _todosRepository.AddTodoItem(todoItem);
                return Ok(res);
            }
            catch
            {
                return BadRequest();
            }
        }


        //create new list
        [HttpPost]
        public async Task<ActionResult<TodoList>> AddTodoItem([FromBody] TodoList todoGroup)
        {
            try
            {
                var res = await _todosRepository.AddTodoGroup(todoGroup);
                return Ok(res);
            }
            catch
            {
                return BadRequest();
            }
        }

    }
}
