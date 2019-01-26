using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TodoApi.Models;
using TodoApi.Models.Persistance;

namespace TodoApi.Controllers 
{
    [Route ("api/[controller]")]
    [ApiController]
    public class TodoController : ControllerBase 
    {
        private IUnitOfWork _uow;
        public TodoController (IUnitOfWork uow) 
        {
            _uow = uow;
            if(_uow.TodoItems.Count() == 0)
            {
                _uow.TodoItems.Add(new TodoItem {Name = "Item1", IsComplete = false});
            }
        }

        // GET: api/Todo
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TodoItem>>> GetTodoItems () 
        {
            return Ok(await _uow.TodoItems.FindAsync());
        }

        // GET: api/Todo/5
        [HttpGet ("{id}")]
        public async Task<ActionResult<TodoItem>> GetTodoItem (String id) 
        {
            var todoItem = await _uow.TodoItems.FindAsync (id);

            if (todoItem == null) {
                return NotFound ();
            }

            return todoItem;
        }

        // POST: api/Todo
        [HttpPost]
        public async Task<ActionResult<TodoItem>> PostTodoItem (TodoItem todoItem) 
        {
            await _uow.TodoItems.Add (todoItem);

            return CreatedAtAction ("GetTodoItem", new { id = todoItem.Id }, todoItem);
        }

        // PUT: api/Todo/5
        [HttpPut ("{id}")]
        public async Task<IActionResult> PutTodoItem (String id, TodoItem todoItem) 
        {
            await _uow.TodoItems.Update(todoItem);

            return NoContent ();
        }

        // DELETE: api/Todo/5
        [HttpDelete ("{id}")]
        public async Task<ActionResult<TodoItem>> DeleteTodoItem (String id) 
        {
            var todoItem = await _uow.TodoItems.FindAsync (id);

            if (todoItem == null) {
                return NotFound ();
            }

            await _uow.TodoItems.Remove(todoItem);

            return todoItem;
        }
    }
}