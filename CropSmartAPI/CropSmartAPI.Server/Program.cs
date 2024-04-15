using CropSmartAPI.Core.Services;
using CropSmartAPI.Core.Services.Interfaces;
using CropSmartAPI.Core.SessionObjects;
using CropSmartAPI.DAL.Context;
using Microsoft.EntityFrameworkCore;
using static CSharpFunctionalExtensions.Result;

var configuration = GetConfiguration();
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddMediatR(c => c.RegisterServicesFromAssemblyContaining<Program>());

// Add services to the container.
var dbContextOptions = builder.Services.AddDbContextFactory<DataContext>();

builder.Services.AddTransient<IFertilizerService, FertilizerService>();
builder.Services.AddTransient<IFertilityPredictionService, FertilityPredictionService>();
builder.Services.AddTransient<IUserService, UserService>();
builder.Services.AddTransient<IFieldService, FieldService>();
builder.Services.AddTransient<ICropService, CropService>();
builder.Services.AddTransient<ISessionControlService, SessionControlService>();
builder.Services.AddSingleton<ISessionList, SessionList>();


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddHostedService(provider =>
{
    int sessionLifetimeMinutes = configuration.GetValue<int>("Session:SessionLifetimeMinutes");
    int checkIntervalMinutes = configuration.GetValue<int>("Session:CheckIntervalMinutes");
    var memoryStore = provider.GetRequiredService<ISessionList>();

    return new SessionCleanupService(memoryStore, sessionLifetimeMinutes, checkIntervalMinutes);
});

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

CreateDbIfNotExists(app);

app.Run();


IConfiguration GetConfiguration()
{
    var builder = new ConfigurationBuilder()
        .SetBasePath(Directory.GetCurrentDirectory())
        .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
        .AddEnvironmentVariables();

    return builder.Build();
}

async void CreateDbIfNotExists(IHost host)
{
    using (var scope = host.Services.CreateScope())
    {
        var services = scope.ServiceProvider;
        try
        {
            var context = services.GetRequiredService<DataContext>();
            await context.Database.EnsureCreatedAsync();
        }
        catch (Exception ex)
        {
            var logger = services.GetRequiredService<ILogger<Program>>();
            logger.LogError(ex, "An error occurred creating the DB.");
        }
    }
}
