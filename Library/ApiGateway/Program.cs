using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
string envJson = "";
if (Environment.GetEnvironmentVariable("CONTAINER_ENV").Equals("docker"))
{
    envJson = "OcelotDocker.json";
}
else if (Environment.GetEnvironmentVariable("CONTAINER_ENV").Equals("k8s"))
{
    envJson = "OcelotK8S.json";
}

builder.Configuration.SetBasePath(builder.Environment.ContentRootPath)
    .AddJsonFile(envJson, optional: false, reloadOnChange: true)
    .AddEnvironmentVariables();
builder.Services.AddOcelot(builder.Configuration);
builder.Logging.ClearProviders();
builder.Logging.AddConsole();
builder.Logging.SetMinimumLevel(LogLevel.Information);

builder.Services.AddSwaggerGen();

builder.Services.AddSwaggerForOcelot(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();
app.UseExceptionHandler("/Error");


app.UseSwaggerForOcelotUI(opt =>
{
    opt.PathToSwaggerGenerator = "/swagger/docs";
});

app.UseStaticFiles();

app.UseRouting();

await app.UseOcelot();
app.Run();
