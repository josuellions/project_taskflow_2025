using Microsoft.EntityFrameworkCore;
using taskflow.API.Contracts;
using taskflow.API.Filter;
using taskflow.API.Repositories;
using taskflow.API.Repositories.DataAccess;
using taskflow.API.UseCases.Projects.DeleteCurrent;
using taskflow.API.UseCases.Projects.GetCurrent;
using taskflow.API.UseCases.Projects.PostCurrent;
using taskflow.API.UseCases.Projects.PutCurrent;
using taskflow.API.UseCases.Report.GetCurrent;
using taskflow.API.UseCases.Tasks.DeleteCurrent;
using taskflow.API.UseCases.Tasks.GetCurrent;
using taskflow.API.UseCases.Tasks.PostCurrent;
using taskflow.API.UseCases.Tasks.PutCurrent;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Injeção dependencias da aplicação
builder.Services.AddScoped<PostCurrentProjectUseCase>();
builder.Services.AddScoped<GetCurrentProjectUseCase>();
builder.Services.AddScoped<PutCurrentProjectUseCase>();
builder.Services.AddScoped<DeleteCurrentProjectUseCase>();
builder.Services.AddScoped<PostCurrentTaskUseCase>();
builder.Services.AddScoped<GetCurrentTaskUseCase>();
builder.Services.AddScoped<PutCurrentTaskUseCase>();
builder.Services.AddScoped<DeleteCurrentTaskUseCase>();
builder.Services.AddScoped<GetCurrentReportUseCase>();
builder.Services.AddScoped<IProjectRepository, ProjectRepository>();
builder.Services.AddScoped<ITaskRepository, TaskRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IReportRepository, ReportRepository>();
builder.Services.AddScoped <ITaskHistoryRepository, TaskHistoryRepository > ();

//Custom tratativas de error
builder.Services.AddMvc(options => options.Filters.Add(typeof(ExceptionFilter)));

builder.Services.AddDbContext<TaskFlowDbContext>(options =>
{
    //Acesso Banco Dados
    //options.UseSqlite(@"Data Source=P:\Teste Vagas\2025\projeto_eclipseworks\db\taskflowDB.db");
    options.UseSqlite(@"Data Source=C:\Users\Josuel\source\repos\taskflow\src\taskflow.API\DataBase\taskflowDB.db");
}
);
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
