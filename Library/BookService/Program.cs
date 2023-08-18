using BookService.Infrastructure;
using BookService.Interface;
using BookService.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<BookDBContext>(opt => opt.UseSqlServer(builder.Configuration.GetConnectionString("BookDB")));
builder.Services.AddScoped<DbContext, BookDBContext>();
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddScoped<IRepositoryWrapper, RepositoryWrapper>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

    using var scope = app.Services.CreateScope();
    var bookContext = scope.ServiceProvider.GetRequiredService<BookDBContext>();
    bookContext.Database.EnsureCreated();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
