using Biblioteka.Entities;

namespace Biblioteka.API.Models;

public class CreatBook
{
    public string Name { get; set; }
    public DateTime? Created { get; set; }
    public string City { get; set; }
    public string Publisher { get; set; }
    public string ISBN { get; set; }
    public List<Guid> Avtors { get; set; } = new ();
}