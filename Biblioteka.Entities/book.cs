namespace Biblioteka.Entities;

public class Book
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public DateTime? Created { get; set; }
    public string City { get; set; }
    public string Publisher { get; set; }
    public string ISBN { get; set; }
    public List<Avtor> Avtors { get; set; } = new ();
}
