using FlightBookingSystem.Domain.Interfaces;
using FlightBookingSystem.Infrastructure.Repositories;
using FlightBookingSystem.Application.Interfaces;
using FlightBookingSystem.Application.Services;
using FlightBookingSystem.Domain.Entities;
using FlightBookingSystem.Infrastructure.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// get connection string from appsettings.json
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// adding DbContext with SQL Server provider
builder.Services.AddDbContext<FlightDbContext>(options =>
    options.UseSqlServer(connectionString));

// adding Identity
builder.Services.AddIdentity<User, IdentityRole>()
    .AddEntityFrameworkStores<FlightDbContext>()
    .AddDefaultTokenProviders();

// registering services and repositories
builder.Services.AddScoped<IFlightRepository, FlightRepository>();
builder.Services.AddScoped<IFlightService, FlightService>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// http pipeline configuration
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication(); 
app.UseAuthorization();

app.MapControllers();

app.Run();