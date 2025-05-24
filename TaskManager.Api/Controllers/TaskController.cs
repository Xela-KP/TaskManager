using Microsoft.AspNetCore.Mvc;
using TaskManager.Api.Models;
using TaskManager.Api.Services;

namespace TaskManager.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TasksController : ControllerBase
    {
        private readonly ITaskService _svc;
        public TasksController(ITaskService svc) => _svc = svc;

        [HttpGet]
        public async Task<ActionResult<List<TaskItem>>> GetAll() =>
            Ok(await _svc.GetAllAsync());

        [HttpGet("{id:int}")]
        public async Task<ActionResult<TaskItem>> GetById(int id)
        {
            var task = await _svc.GetByIdAsync(id);
            return task is not null ? Ok(task) : NotFound();
        }

        [HttpPost]
        public async Task<ActionResult<TaskItem>> Create(TaskItem task)
        {
            var created = await _svc.CreateAsync(task);
            return CreatedAtAction(nameof(GetById),
                                   new { id = created.Id },
                                   created);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Update(int id, TaskItem task)
        {
            var ok = await _svc.UpdateAsync(id, task);
            return ok ? NoContent() : NotFound();
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var ok = await _svc.DeleteAsync(id);
            return ok ? NoContent() : NotFound();
        }
    }
}
