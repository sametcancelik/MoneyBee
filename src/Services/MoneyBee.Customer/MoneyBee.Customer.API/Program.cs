using MoneyBee.Customer.API.Middlewares;
using MoneyBee.Customer.Application;
using MoneyBee.Customer.Infrastructure;
using MoneyBee.Customer.Infrastructure.Persistence;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureServices(builder.Configuration);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
var app = builder.Build();
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var context = services.GetRequiredService<CustomerDbContext>();
        context.Database.EnsureCreated(); 
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Hata: {ex.Message}");
    }
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.UseMiddleware<ExceptionHandlingMiddleware>();
app.MapControllers();

app.Run();