using AutoMapper;
using BookService.Infrastructure;
using BookService.Interface;
using BookService.Mappers;
using BookService.Repositories;
using BookService.Validators;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var mapperConfig = new MapperConfiguration(mc =>
{
    mc.AddProfile(new AuthorProfile());
    mc.AddProfile(new BookProfile());
    mc.AddProfile(new RentProfile());
});

IMapper mapper = mapperConfig.CreateMapper();
builder.Services.AddSingleton(mapper);

builder.Services.AddDbContext<BookDBContext>(opt => opt.UseSqlServer(builder.Configuration.GetConnectionString("BookDB")));
builder.Services.AddScoped<DbContext, BookDBContext>();
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddScoped<IRepositoryWrapper, RepositoryWrapper>();
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddValidatorsFromAssemblyContaining<BaseValidator>();

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
