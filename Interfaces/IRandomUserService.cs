using RandomUsersApp.DTOs;

namespace RandomUsersApp.Interfaces
{
    public interface IRandomUserService
    {
        Task<UserDto?> GetRandomUserAsync();
    }
}