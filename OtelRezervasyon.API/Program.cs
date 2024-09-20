using Microsoft.EntityFrameworkCore;
using OtelRezervasyon.Persistence;
//OSMANİBOOooooooo

var builder = WebApplication.CreateBuilder(args);

// Veritabanı bağlantısını ayarlama
builder.Services.AddDbContext<OtelRezervasyonDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// CORS servisini ekleme
builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy", builder =>
    {
        builder.AllowAnyOrigin()    // İsteğe bağlı: belirli bir kaynağa izin verebilirsiniz
               .AllowAnyMethod()    // İsteğe bağlı: GET, POST gibi belirli metodlara izin verebilirsiniz
               .AllowAnyHeader();   // İsteğe bağlı: Belirli başlıklara izin verebilirsiniz
    });
});

// Swagger/OpenAPI ayarları
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Geliştirme ortamı için Swagger'ı etkinleştirme
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// CORS middleware'i ekleme (Doğru sırada yer almalı)
app.UseCors("CorsPolicy"); // CorsPolicy adındaki politikayı kullanıyoruz

// Seed verilerini eklemek için gerekli kodu ekleyin
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    try
    {
        // Seed işlemini başlat
        Seed.Initialize(services).Wait();
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An error occurred while seeding the database.");
    }
}

var summaries = new[] { "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching" };

app.MapGet("/weatherforecast", () =>
{
    var forecast = Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast(DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                             Random.Shared.Next(-20, 55),
                             summaries[Random.Shared.Next(summaries.Length)]))
        .ToArray();
    return forecast;
})
.WithName("GetWeatherForecast")
.WithOpenApi();

app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
