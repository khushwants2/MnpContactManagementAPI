using Microsoft.EntityFrameworkCore;
using MNPBusinessLogic;
using MNPDatabaseRepository.Models;
using MNPInterfaces;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Host.ConfigureAppConfiguration((hostingContext, configuration) =>
{
    configuration.Sources.Clear();
    configuration.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
    configuration.Build();
});
// create Logging
builder.Host
    .ConfigureLogging((logging) => 
    { 
        logging.ClearProviders();
    })
    .UseSerilog((hostingContext, loggerConfiguration) =>
    {
        loggerConfiguration.WriteTo.Console();
     });
builder.Host.ConfigureServices((services) => 
{
    string dbName = Guid.NewGuid().ToString();
    services.AddDbContext<MnpContactManagementContext>();
    //services.AddDbContext<MnpContactManagementContext>(options =>
    //{
    //    options.UseInMemoryDatabase(dbName);
    //});
    services.AddTransient<IContactManagementBusinessLogic, ContactManagementBusinessLogic>();
});
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

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
