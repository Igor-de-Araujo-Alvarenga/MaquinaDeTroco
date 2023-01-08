using Domain;
using Infrastructure.Context;
using Infrastructure.Controllers;
using Infrastructure.Interfaces;
using Infrastructure.Repository;
using Infrastructure.Utils;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var connectionString = builder.Configuration.GetConnectionString("MaquinaDeTrocoDB");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString, b => b.MigrationsAssembly("MaquinaDeTroco.Api")));

builder.Services.AddScoped(typeof(IRepository<Money>), typeof(MoneyRepository));
builder.Services.AddTransient(typeof(ICashRegister), typeof(CashRegister));
builder.Services.AddTransient(typeof(ICashRegisterUtils), typeof(CashRegisterUtils));
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "Origin",
                      policy  =>
                      {
                          policy.WithOrigins("https://localhost:7042")
                          .AllowAnyMethod()
                          .AllowAnyHeader();
                      });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    using (var scope = app.Services.CreateScope())
    {
        var _machineUtils = scope.ServiceProvider.GetRequiredService<ICashRegisterUtils>();
        _machineUtils.Reset();
    }
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
app.UseCors("Origin");
app.Run();
