using EmailMessageService.Interfaces;
using EmailMessageService.Services;
using RabbitMQ.Client;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<IMailService, MailService>();
builder.Services.AddSingleton<ConnectionFactory>(sp =>
{
    var configuration = sp.GetRequiredService<IConfiguration>();
    var RabbitMQServer = builder.Configuration["RabbitMQ:RabbitURL"];
    var RabbitMQUserName = builder.Configuration["RabbitMQ:Username"];
    var RabbutMQPassword = builder.Configuration["RabbitMQ:Password"];

    var factory = new ConnectionFactory()
    {
        HostName = RabbitMQServer,
        UserName = RabbitMQUserName,
        Password = RabbutMQPassword
    };

    return factory;
});
builder.Services.AddHostedService<RabbitMQConsumerService>();

var configuration = new ConfigurationBuilder()
        .SetBasePath(builder.Environment.ContentRootPath)
        .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

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
