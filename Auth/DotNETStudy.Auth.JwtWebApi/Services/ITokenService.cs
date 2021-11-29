using DotNETStudy.Auth.JwtWebApi.Dtos;

namespace DotNETStudy.Auth.JwtWebApi.Services
{
    public interface ITokenService
    {
        string BuildToken(string key, string issuer, UserDto user);
    }
}
