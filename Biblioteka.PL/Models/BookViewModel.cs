using System.Text.Json.Serialization;

namespace Biblioteka.PL.Models;

public class BookViewModel
{
    [JsonPropertyName("id")]
    public Guid Id { get; set; }
    [JsonPropertyName("name")]
    public string Name { get; set; }
    [JsonPropertyName("created")]
    public DateTime? Created { get; set; }
    [JsonPropertyName("city")]
    public string City { get; set; }
    [JsonPropertyName("publisher")]
    public string Publisher { get; set; }
    [JsonPropertyName("isbn")]
    public string ISBN { get; set; }
    [JsonPropertyName("avtors")]
    public List<Guid> SelectedAuthorIds { get; set; } = []; // Для хранения выбранных идентификаторов авторов
    
    [JsonIgnore]
    public List<AuthorViewModel> Authors { get; set; } = [];
}