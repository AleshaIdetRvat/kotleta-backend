using MariyaBackend.Models.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace MariyaBackend.Models;

public class UserRepository : IUserRepository
{
    private readonly ApiDbContext _context;
    public UserRepository(ApiDbContext context)
    {
        _context = context;
    }
    public async Task<bool> Authorize(string login, string password)
    {
        if (string.IsNullOrEmpty(login) ||
            string.IsNullOrEmpty(password) ||
            login.Length < 5 ||
            password.Length < 5)
        {
            return false;
        }

        var user = await _context.Users.FirstOrDefaultAsync(x => x.Login == login && x.Password == password);

        return user is not null ? true : false;
    }

    public async Task<long?> RegisterUser(string login, string password)
    {
        if (string.IsNullOrEmpty(login) ||
            string.IsNullOrEmpty(password) ||
            login.Length < 5 ||
            password.Length < 5)
        {
            return new long?();
        }

        var possibleUser = await _context.Users.FirstOrDefaultAsync(x => x.Login == login);
        if (possibleUser is not null)
        {
            return null;
        }

        var maxId = await _context.Users.MaxAsync(u => (int?)u.Id) ?? 0;

        var addedUser = new User
        {
            Id = maxId + 1,
            Login = login,
            Password = password,
        };

        await _context.Users.AddAsync(addedUser);
        await _context.SaveChangesAsync();

        return addedUser.Id;
    }
}
