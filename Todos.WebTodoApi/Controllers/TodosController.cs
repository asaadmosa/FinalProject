using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Todos.Contracts.Repositories;
using Todos.DataModel;

namespace WebTodoApi.Controllers
{
    
    [Route("todos")]//the route is todos
    [ApiController]
    [EnableCors("Policy1")]
    public class TodosController : ControllerBase
    {
        private readonly ITodosRepository _todosRepository;
        public TodosController(ITodosRepository todosRepository)
        {
            _todosRepository = todosRepository;
        }
        
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
        
        [HttpGet("TodoGroup/{groupId}/Items/{itemId}")]
        public async Task<ActionResult> DeleteItemsOfGroup(Guid groupId, Guid itemId)
        {
            try
            {
                await _todosRepository.DeleteItem(groupId, itemId);
                return Ok();
            }
            catch
            {
                return NotFound();
            }
        }

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


        [HttpPost]
        public async Task<ActionResult> AddTodoGroup([FromBody] TodoList todoGroup)
        {
            try
            {
                var res = await _todosRepository.AddTodoGroup(todoGroup);
                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }


    }
}
