using AutoMapper;
using JokesApi.DataContext;
using JokesApi.Repositories.Author;
using JokesApi.Repositories.Category;
using JokesApi.Repositories.Joke;
using JokesApi.Repositories.Language;
using JokesApi.Services.Author;
using JokesApi.Services.Category;
using JokesApi.Services.Joke;
using JokesApi.Services.Language;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<JokesDataContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnectionString")));
builder.Services.AddAutoMapper(typeof(Program).Assembly);
builder.Services.AddTransient<IAuthorsRepository, AuthorsRepository>();
builder.Services.AddTransient<ICategoriesRepository, CategoriesRepository>();
builder.Services.AddTransient<IJokesRepository, JokesRepository>();
builder.Services.AddTransient<ILanguagesRepository, LanguagesRepository>();
builder.Services.AddTransient<IAuthorsService, AuthorsService>();
builder.Services.AddTransient<ICategoriesService, CategoriesService>();
builder.Services.AddTransient<IJokesService, JokesService>();
builder.Services.AddTransient<ILanguagesService, LanguagesService>();
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
