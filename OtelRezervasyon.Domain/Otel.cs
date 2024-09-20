using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using System.Linq;
using System.Threading.Tasks;

namespace OtelRezervasyon.Domain
{
    public class Otel
    {
        public int Id { get; set; }
        public string OtelAdi { get; set; } = string.Empty;
        public string Il { get; set; } = string.Empty;
        public string Ilce { get; set; } = string.Empty;
        public int YildizSayisi { get; set; }
    }

    public class UserDto
    {
        [JsonPropertyName("name")]
        public string? Name { get; set; } // Kullanıcı adı

        [JsonPropertyName("username")]
        public string? Username { get; set; } // Kullanıcı adı (username)

        [JsonPropertyName("email")]
        public string? Email { get; set; } // Email

        [JsonPropertyName("address")]
        public AddressDto? Address { get; set; } // Adres

        [JsonPropertyName("phone")]
        public string? Phone { get; set; } // Telefon

        [JsonPropertyName("website")]
        public string? Website { get; set; } // Web sitesi

        [JsonPropertyName("company")]
        public CompanyDto? Company { get; set; } // Şirket bilgileri
    }

    public class AddressDto
    {
        public string? Street { get; set; } // Sokak
        public string? Suite { get; set; } // Daire
        public string? City { get; set; } // Şehir
        public string? Zipcode { get; set; } // Posta kodu
        public GeoDto? Geo { get; set; } // Coğrafi bilgiler
    }

    public class GeoDto
    {
        public string? Lat { get; set; } // Enlem
        public string? Lng { get; set; } // Boylam
    }

    public class CompanyDto
    {
        public string? Name { get; set; } // Şirket adı
        public string? CatchPhrase { get; set; } // Slogan
        public string? Bs { get; set; } // İş bilgileri
    }
}
