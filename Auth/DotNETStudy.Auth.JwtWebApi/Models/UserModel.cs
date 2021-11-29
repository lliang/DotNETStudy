using System.ComponentModel.DataAnnotations;

namespace DotNETStudy.Auth.JwtWebApi.Models
{
    public record UserModel([Required] string UserName, [Required] string Password);
}
