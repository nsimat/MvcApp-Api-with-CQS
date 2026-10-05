using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using WebbApiwithCQS.Domain.Commands;
using WebbApiwithCQS.Domain.DTOs;
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
            var tasks = await _taskRepository.Execute(new GetTasksQuery());
            return Ok(tasks);
        }

        // GET api/<TasksController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var task = await _taskRepository.Execute(new GetTaskByIdQuery(id));

            return task is null ? BadRequest($"The task with ID {id} does not exist!") : Ok(task);
        }

        // POST api/<TasksController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] TaskToCreate task)
        {
            if (!ModelState.IsValid)
                return BadRequest("The model for creating a task is not validated!");

            string title = task.Title;


            var command = new InsertTaskCommand(title);

            var result = await _taskRepository.Execute(command);

            return result ? NoContent() : BadRequest($"Task with title '{task.Title}' has not been created!");
        }

        // PUT api/<TasksController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] TaskToUpdate task)
        {
            if (!ModelState.IsValid)
                return BadRequest("The model for updating a task is not validated!");

            var taskToUpdate = await _taskRepository.Execute(new GetTaskByIdQuery(id));

            if (taskToUpdate is null)
                return BadRequest($"The task with ID {id} does not exist!");

            taskToUpdate.Titre = task.Title;
            taskToUpdate.DateCreation = task.DateOfCreation;

            var command = new UpdateTaskCommand(id, taskToUpdate);

            bool changes = await _taskRepository.Execute(command);

            return changes ? NoContent() : BadRequest($"Task with ID {id} has not been updated!");

        }

        // DELETE api/<TasksController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var changes = await _taskRepository.Execute(new DeleteTaskCommand(id));

            return changes ? NoContent() : BadRequest($"Something happened when deleting task with ID {id}!");

        }

        [HttpPatch("cloture/{id}")]
        public async Task<IActionResult> ClotureTask(int id)
        {
            var taskToClose = await _taskRepository.Execute(new GetTaskByIdQuery(id));

            if (taskToClose is null)
                return BadRequest($"The task with ID {id} does not exist!");

            taskToClose.Cloturee = true;
            var command = new PatchTaskCommand(id, taskToClose);

            var changes = await _taskRepository.Execute(command);

            return changes ? NoContent() : BadRequest($"Something happened while trying to end the task with ID {id}!");
        }
    }
}
