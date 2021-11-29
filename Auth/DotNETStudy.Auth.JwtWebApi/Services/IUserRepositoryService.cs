using DotNETStudy.Auth.JwtWebApi.Dtos;
using DotNETStudy.Auth.JwtWebApi.Models;

namespace DotNETStudy.Auth.JwtWebApi.Services
{
    public interface IUserRepositoryService
    {
        UserDto GetUser(UserModel userModel);
    }
}
