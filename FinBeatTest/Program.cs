using Data.Context;
using FinBeatTest;
using FinBeatTest.Middleware;
using Microsoft.EntityFrameworkCore;
using Services.Mapping;
using Services.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ApplicationContext>(options =>
              options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddTransient<IRepository, Repository>();
builder.Services.AddTransient<ICodeValueService, CodeValueService>();

builder.Services.AddSingleton<QueryMapping>();
builder.Services.AddSingleton<ServiceMapping>();

builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseLoggingMiddleware();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
