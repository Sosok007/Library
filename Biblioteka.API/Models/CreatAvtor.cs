using Biblioteka.Entities;

namespace Biblioteka.API.Models;

public class CreatAvtor
{
    public string Firstname { get; set; }
    public string Lastname { get; set; }
    public string Patronymic { get; set; }
    public List<Guid> Books { get; set; } = new ();
}