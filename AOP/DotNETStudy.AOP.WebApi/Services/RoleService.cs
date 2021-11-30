using DotNETStudy.AOP.WebApi.Dtos;

namespace DotNETStudy.AOP.WebApi.Services
{
    public class RoleService : IRoleService
    {
        public async Task<Guid> CreateAsync(CreateRoleRequest request)
        {
            await Task.Delay(500);
            return await Task.Run(() => Guid.NewGuid());
        }
    }
}
