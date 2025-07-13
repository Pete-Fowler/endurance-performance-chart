using PerformanceManagementChart.Server.Services;

DotNetEnv.Env.Load();

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IMetricsService, MetricsService>();
builder.Services.AddScoped<IntervalsIcuApiService>();
builder.Services.AddScoped<Func<string, IActivityApiService>>(provider =>
    serviceType =>
    {
        return serviceType switch
        {
            "intervals" => provider.GetRequiredService<IntervalsIcuApiService>(),
            _ => throw new ArgumentException("Unknown activity api service type."),
        };
    }
);

var app = builder.Build();

app.UseDefaultFiles();
app.UseStaticFiles();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.MapFallbackToFile("/index.html");

app.Run();
