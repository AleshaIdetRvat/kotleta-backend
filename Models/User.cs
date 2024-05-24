namespace MariyaBackend.Models;

public class User
{
    public required long Id { get; set; }
    public required string Login { get; set; }
    public required string Password { get; set; }
}
