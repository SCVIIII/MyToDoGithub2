using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MyToDo.Api;
using MyToDo.Api.Context;
using MyToDo.Api.Context.Repository;
using MyToDo.Api.Extensions;
using MyToDo.Api.Service;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddTransient<MT2_IToDoService, MT2_ToDoService>();

builder.Services.AddDbContext<MT2_MyToDoContext>(options =>
{

    var connectionString = builder.Configuration.GetConnectionString("ToDoConnection");

    options.UseSqlite(connectionString);

}).AddUnitOfWork<MT2_MyToDoContext>()
.AddCustomRepository<MT2_ToDo, ToDoRepository>()
.AddCustomRepository<MT2_Memo, MemoRepository>()
.AddCustomRepository<MT2_User, UserRepository>();

builder.Services.AddTransient<MT2_IToDoService,MT2_ToDoService>();
builder.Services.AddTransient<MT2_IMemoService, MT2_MemoService>();
builder.Services.AddTransient<MT2_ILoginService, MT2_LoginService>();

//Ìí¼ÓAutoMapper
var automapperconfig = new MapperConfiguration(config =>
{
    config.AddProfile(new MT2_AutoMapperProFile().MapperCE);
});

builder.Services.AddSingleton(automapperconfig.CreateMapper());
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
