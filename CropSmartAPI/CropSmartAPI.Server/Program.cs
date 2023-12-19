using CropSmartAPI.Core.Services;
using CropSmartAPI.Core.Services.Interfaces;
using CropSmartAPI.DAL.Context;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddMediatR(c => c.RegisterServicesFromAssemblyContaining<Program>());

// Add services to the container.
var dbContextOptions = builder.Services.AddDbContextFactory<DataContext>();

builder.Services.AddTransient<IFertilizerService, FertilizerService>();
builder.Services.AddTransient<IUserService, UserService>();


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
