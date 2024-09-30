namespace Biblioteka.Entities;

public class Avtor
{
    public Guid Id { get; set; }
    public string Firstname { get; set; }
    public string Lastname { get; set; }
    public string Patronymic { get; set; }
    public List<Book> Books { get; set; } = new ();
}
