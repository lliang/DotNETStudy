using AspectCore.DynamicProxy.Parameters;
using DotNETStudy.AOP.WebApi.Dtos;
using DotNETStudy.AOP.WebApi.Validations;

namespace DotNETStudy.AOP.WebApi.Services
{
    /// <summary>
    /// 角色服务
    /// </summary>
    public interface IRoleService
    {
        /// <summary>
        /// 创建角色
        /// </summary>
        /// <param name="request">创建角色请求参数</param>
        Task<Guid> CreateAsync([NotNull][Valid] CreateRoleRequest request);
    }
}