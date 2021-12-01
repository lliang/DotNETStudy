using System;

namespace DotNETStudy.Auth.JwtWebApi.Dtos
{
    public record UserDto(Guid UserId, string UserName, string Password, string PhoneNumber)
    {
    }
}
