using Microsoft.EntityFrameworkCore;
using TaskManager.Api.Data;
using TaskManager.Api.Repositories;
using TaskManager.Api.Services;

var builder = WebApplication.CreateBuilder(args);

// 1. Add MVC controllers
builder.Services.AddControllers();

// 2. Register EF DbContext, repos, services
builder.Services.AddDbContext<TaskDbContext>(opts =>
    opts.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddScoped<ITaskRepository, TaskRepository>();
builder.Services.AddScoped<ITaskService, TaskService>();

// 3. Swagger services
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// 4. Swagger middleware (only in Development, or remove the if-guard to enable always)
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();                // <— serve /swagger/v1/swagger.json
    app.UseSwaggerUI(c =>            // <— serve the UI at /swagger
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "TaskManager API v1");
        // c.RoutePrefix = string.Empty;   // uncomment to serve UI at root "/"
    });
}

app.UseHttpsRedirection();
app.UseAuthorization();

// 5. Map your controllers
app.MapControllers();

app.Run();
