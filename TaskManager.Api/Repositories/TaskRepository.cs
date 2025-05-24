using Microsoft.EntityFrameworkCore;
using TaskManager.Api.Data;
using TaskManager.Api.Models;

namespace TaskManager.Api.Repositories
{
    public class TaskRepository : ITaskRepository
    {
        private readonly TaskDbContext _db;
        public TaskRepository(TaskDbContext db) => _db = db;
        public async Task<List<TaskItem>> GetAllAsync() =>
            await _db.Tasks.AsNoTracking().ToListAsync();
        public async Task<TaskItem?> GetByIdAsync(int id) =>
            await _db.Tasks.FindAsync(id);
        public async Task<TaskItem> CreateAsync(TaskItem task)
        {
            _db.Tasks.Add(task);
            await _db.SaveChangesAsync();
            return task;
        }

        public async Task<bool> UpdateAsync(TaskItem task)
        {
            var exists = await _db.Tasks.AnyAsync(t => t.Id == task.Id);
            if (!exists) return false;
            _db.Tasks.Update(task);
            return await _db.SaveChangesAsync() > 0;
        }
        public async Task<bool> DeleteAsync(int id)
        {
            var task = await _db.Tasks.FindAsync(id);
            if (task == null) return false;
            _db.Tasks.Remove(task);
            return await _db.SaveChangesAsync() > 0;
        }
    }
}
