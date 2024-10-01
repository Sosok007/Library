using System.Text.Json.Serialization;

namespace Biblioteka.PL.Models;

public class AuthorViewModel
{
    [JsonPropertyName("id")]
    public Guid Id { get; set; }
    [JsonPropertyName("firstname")]
    public string Firstname { get; set; }
    [JsonPropertyName("lastname")]
    public string Lastname { get; set; }
    [JsonPropertyName("patronymic")]
    public string Patronymic { get; set; }
    [JsonPropertyName("books")]
    public List<Guid> SelectedBookIds { get; set; } = []; // Для хранения выбранных идентификаторов книг
    [JsonIgnore]
    public List<BookViewModel> Books { get; set; } = [];
}