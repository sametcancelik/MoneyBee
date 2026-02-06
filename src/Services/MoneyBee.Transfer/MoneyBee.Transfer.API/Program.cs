using Microsoft.AspNetCore.RateLimiting;
using MoneyBee.Shared.Middlewares;
using MoneyBee.Transfer.Application;
using MoneyBee.Transfer.Infrastructure;
using MoneyBee.Transfer.Infrastructure.Persistence;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureServices(builder.Configuration);

builder.Services.AddRateLimiter(options =>
{
    options.AddFixedWindowLimiter("ApiRateLimit", opt =>
    {
        opt.Window = TimeSpan.FromMinutes(1);
        opt.PermitLimit = 100; //
        opt.QueueLimit = 0;
    });
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(); //

var app = builder.Build();
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var context = services.GetRequiredService<TransferDbContext>();
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

app.UseMiddleware<ApiKeyMiddleware>(); //

app.UseRateLimiter(); //

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();