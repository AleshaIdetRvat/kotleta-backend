namespace MariyaBackend.Models.Interfaces;

public interface IUserRepository
{
    public Task<long?> RegisterUser(string login, string password);
    public Task<bool> Authorize(string login, string password);
}
