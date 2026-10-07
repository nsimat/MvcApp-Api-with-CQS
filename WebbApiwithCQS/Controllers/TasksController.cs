using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using WebbApiwithCQS.Controllers.Infrastructure;
using WebbApiwithCQS.Domain.Commands;
using WebbApiwithCQS.Domain.DTOs;
using WebbApiwithCQS.Domain.Entities;
using WebbApiwithCQS.Domain.Repositories;
using WebbApiwithCQS.Domain.Queries;

namespace WebbApiwithCQS.Controllers
{
    [EnableCors("AnyOrigin")]
    [Route("api/[controller]")]
    [ApiController]
    public class TasksController(ITaskRepository taskRepository) : ControllerBase
    {
        private readonly ITaskRepository _taskRepository = taskRepository ?? throw new ArgumentNullException(nameof(taskRepository));

        // GET: api/<TasksController>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var tasks = await _taskRepository.ExecuteAsync(new GetTasksQuery());

            return this.FromResult(tasks);
        }

        // GET api/<TasksController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var task = await _taskRepository.ExecuteAsync(new GetTaskByIdQuery(id));

            return this.FromResult(task);
        }

        // POST api/<TasksController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] TaskToCreate task)
        {
            if (!ModelState.IsValid)
                return BadRequest("The model for creating a task is not validated!");

            //string title = task.Title;


            var command = new InsertTaskCommand(task.Title);

            var result = await _taskRepository.ExecuteAsync(command);

            return this.FromResult(result);
        }

        // PUT api/<TasksController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] TaskToUpdate task)
        {
            if (!ModelState.IsValid)
                return BadRequest("The model for updating a task is not validated!");

            var newTask = new Tache()
            {
                Id = id,
                Titre = task.Title,
                DateCreation = task.DateOfCreation
            };

            var result = await _taskRepository.ExecuteAsync(new UpdateTaskCommand(id, newTask));

            return this.FromResult(result);
        }

        // DELETE api/<TasksController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var changes = await _taskRepository.ExecuteAsync(new DeleteTaskCommand(id));

            return this.FromResult(changes);

        }

        [HttpPatch("cloture/{id}")]
        public async Task<IActionResult> ClotureTask(int id)
        {
            var result = await _taskRepository.ExecuteAsync(new TaskClosureCommand(id));

            return this.FromResult(result);
        }
    }
}
