using BookStore.Api.Interfaces;
using BookStore.Api.Repositories;
using BookStore.Api.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddSingleton<IBookRepository, InMemoryBookRepository>();
builder.Services.AddSingleton<BookDiscountService>();
builder.Services.AddScoped<BookCatalogService>();

var app = builder.Build();

app.MapControllers();

app.Run();