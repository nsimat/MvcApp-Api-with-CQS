using Microsoft.AspNetCore.Mvc;
using WebAppMVC.Models;
using WebAppMVC.ViewModels;

namespace WebAppMVC.Controllers
{
    public class TasksController(IHttpClientFactory httpClient) : Controller
    {
        private readonly HttpClient _httpClient = httpClient.CreateClient("AppClient") ?? throw new ArgumentNullException(nameof(httpClient));

        // GET: TasksController
        public async Task<ActionResult> Index()
        {
            var tasks = await _httpClient.GetFromJsonAsync<IEnumerable<Tache>>("api/tasks");
            return View(tasks);
        }

        // GET: TasksController/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var task = await _httpClient.GetFromJsonAsync<Tache>($"api/tasks/{id}");

            if (task is null)
                return BadRequest($"Task with ID {id} not found!");

            var taskViewModel = new TaskViewModel()
            {
                Id = task.Id,
                Title = task.Titre,
                DateCreation = task.DateCreation,
                IsClosed = task.Cloturee.HasValue && task.Cloturee.Value
            };

            return View(taskViewModel);
        }

        // GET: TasksController/Create
        public ActionResult Create()
        {
            var task = new TaskViewModel();

            return View(task);
        }

        [HttpPost]
        public async Task<IActionResult> Create(TaskViewModel task)
        {
            if (!ModelState.IsValid)
                return View(task);

            var response = await _httpClient
                .PostAsJsonAsync(
                    "api/tasks",
                    new
                    {
                        Titre = task.Title
                    });

            return response.IsSuccessStatusCode ? RedirectToAction("Index") : View(task);
        }


        // GET: TasksController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            var task = await _httpClient.GetFromJsonAsync<Tache>($"api/tasks/{id}");

            if (task is null)
                return BadRequest($"Task with ID {id} not found!");

            var taskViewModel = new TaskViewModel
            {
                Id = task.Id,
                Title = task.Titre,
                DateCreation = task.DateCreation,
                IsClosed = task.Cloturee.HasValue && task.Cloturee.Value
            };

            return View(taskViewModel);
        }

        [HttpPut]
        [HttpPost]
        public async Task<IActionResult> Edit(int id, TaskViewModel task)
        {
            if (!ModelState.IsValid)
                return View(task);

            if (id != task.Id)
                return BadRequest("Task ID mismatch!");

            var response = await _httpClient
                .PutAsJsonAsync(
                    $"api/tasks/{id}",
                    new
                    {
                        task.Title,
                        DateOfCreation = task.DateCreation
                    });

            return response.IsSuccessStatusCode ? RedirectToAction("Index") : View(task);
        }


        // GET: TasksController/Delete/5
        [HttpGet("Tasks/Delete/{id:int}", Name = "MvcTasksDelete")]
        public async Task<IActionResult> Delete(int id)
        {
            var task = await _httpClient.GetFromJsonAsync<Tache>($"api/tasks/{id}");

            if (task is null)
                return BadRequest($"Task with ID {id} was not found!");

            var taskViewModel = new TaskViewModel
            {
                Id = task.Id,
                Title = task.Titre,
                DateCreation = task.DateCreation,
                IsClosed = task.Cloturee.HasValue && task.Cloturee.Value
            };

            return View(taskViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var response = await _httpClient.DeleteAsync($"api/tasks/{id}");

            return response.IsSuccessStatusCode
                ? RedirectToAction("Index")
                : BadRequest($"Failed to delete task with Id {id}!");
        }

        [HttpPost]
        public async Task<IActionResult> CompleteTask(int id)
        {
            var response = await _httpClient.PatchAsync($"api/tasks/cloture/{id}", null);

            return response.IsSuccessStatusCode
                ? RedirectToAction("Index")
                : BadRequest($"Failed to complete task with ID {id}!");
        }
    }
}
