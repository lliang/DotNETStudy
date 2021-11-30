using DotNETStudy.AOP.WebApi.Dtos;
using DotNETStudy.AOP.WebApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace DotNETStudy.AOP.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        public RolesController(IRoleService service)
        {
            RoleService = service;
        }

        /// <summary>
        /// 角色服务
        /// </summary>
        public IRoleService RoleService { get; }

        /// <summary>
        /// 创建角色
        /// </summary>
        /// <param name="request">创建角色请求参数</param>
        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] CreateRoleRequest request)
        {
            var id = await RoleService.CreateAsync(request);
            return Ok(new { data = id });
        }
    }
}
