using TaskManager.Api.Models;
using TaskManager.Api.Repositories;

namespace TaskManager.Api.Services
{
    public class TaskService : ITaskService
    {
        private readonly ITaskRepository _repo;
        public TaskService(ITaskRepository repo) => _repo = repo;

        public Task<List<TaskItem>> GetAllAsync() =>
            _repo.GetAllAsync();

        public Task<TaskItem?> GetByIdAsync(int id) =>
            _repo.GetByIdAsync(id);

        public Task<TaskItem> CreateAsync(TaskItem task)
        {
            // you could add business rules here
            return _repo.CreateAsync(task);
        }

        public async Task<bool> UpdateAsync(int id, TaskItem task)
        {
            if (id != task.Id) return false;         // 400 Bad Request logic
            return await _repo.UpdateAsync(task);
        }

        public Task<bool> DeleteAsync(int id) =>
            _repo.DeleteAsync(id);
    }
}
