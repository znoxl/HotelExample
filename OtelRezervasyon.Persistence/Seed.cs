using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using OtelRezervasyon.Domain;

namespace OtelRezervasyon.Persistence
{
    public static class Seed
    {
        public static async Task Initialize(IServiceProvider serviceProvider)
        {
            using var context = new OtelRezervasyonDbContext(
                serviceProvider.GetRequiredService<DbContextOptions<OtelRezervasyonDbContext>>());

            // Veritabanında veri var mı kontrol et
            if (await context.Oteller.AnyAsync())
            {
                Console.WriteLine("Zaten veriler mevcut.");
                return;   // Veriler zaten var
            }

            // API'den veri çek
            var httpClient = new HttpClient();
            var response = await httpClient.GetStringAsync("https://jsonplaceholder.typicode.com/users");
            Console.WriteLine(response);

            var options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase // JSON'daki alan isimlerini camelCase olarak ayarlayın
            };

            var users = JsonSerializer.Deserialize<List<UserDto>>(response, options);
            if (users == null)
            {
                Console.WriteLine("Kullanıcılar deserialize edilemedi.");
            }
            else
            {
                Console.WriteLine($"Toplam {users.Count} kullanıcı bulundu.");
            }

            // Kullanıcıları Otel nesnelerine dönüştür
            var oteller = new List<Otel>();
            if (users != null) // Null kontrolü ekledik
            {
                Console.WriteLine("veriler null değil.");
                foreach (var user in users)
                {
                    Console.WriteLine("döngüye girdik.");
                    if (user != null && user.Address != null) // Null kontrolü ekledik
                    {
                        oteller.Add(new Otel
                        {
                            OtelAdi = user.Name ?? string.Empty,
                            Il = user.Address.City ?? string.Empty,
                            Ilce = user.Address.Street ?? string.Empty,
                            YildizSayisi = 3 // Örnek yıldız sayısı
                        });
                    }
                    else
                    {
                        Console.WriteLine($"User ya da Address null: {JsonSerializer.Serialize(user)}");
                    }
                }
            }

            if (oteller.Any())
            {
                try
                {
                    context.Oteller.AddRange(oteller);
                    int changes = await context.SaveChangesAsync();
                    Console.WriteLine($"{changes} kayıt veritabanına kaydedildi.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Hata: {ex.Message}");
                }
            }
        }
    }
}
