using System.Collections.Generic;
using System.Threading.Tasks;
using Moq;
using TaskManager.Api.Models;
using TaskManager.Api.Repositories;
using TaskManager.Api.Services;
using Xunit;

namespace TaskManager.Tests.Services
{
    public class TaskServiceTests
    {
        [Fact]
        public async Task GetAllAsync_ReturnsTasksFromRepository()
        {
            // Arrange: mock repository to return a known list
            var mockRepo = new Mock<ITaskRepository>();
            mockRepo
                .Setup(r => r.GetAllAsync())
                .ReturnsAsync(new List<TaskItem>
                {
                    new TaskItem { Id = 1, Title = "Test Task", IsCompleted = false }
                });

            var service = new TaskService(mockRepo.Object);

            // Act: call the service
            var result = await service.GetAllAsync();

            // Assert: service returns exactly what repo returned
            Assert.Single(result);
            Assert.Equal("Test Task", result[0].Title);
        }
    }
}
