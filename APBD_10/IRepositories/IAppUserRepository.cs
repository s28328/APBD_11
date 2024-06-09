using APBD_10.Models;

namespace APBD_10.IRepositories;

public interface IAppUserRepository
{
    public Task<int> AddUser(AppUser user);
    public Task<AppUser> GetUserByLogin(string login);
    public Task<AppUser> GetUserByRefreshToken(string refreshToken);
    
    public Task SaveChanges();
}