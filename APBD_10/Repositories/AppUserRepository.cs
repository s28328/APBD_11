using APBD_10.Context;
using APBD_10.IRepositories;
using APBD_10.Models;
using Microsoft.EntityFrameworkCore;

namespace APBD_10.Repositories;

public class AppUserRepository:IAppUserRepository
{
    private readonly HospitalDbContext _context;

    public AppUserRepository(HospitalDbContext context)
    {
        _context = context;
    }

    public async Task<int> AddUser(AppUser user)
    {
        _context.Users.Add(user);
        await _context.SaveChangesAsync();
        return 1;
    }

    public async Task<AppUser> GetUserByLogin(string login)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Login == login);
        return user;
    }

    public async Task<AppUser> GetUserByRefreshToken(string refreshToken)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.RefreshToken == refreshToken);
        return user;
    }

    public async Task SaveChanges()
    {
        await _context.SaveChangesAsync();
    }
}