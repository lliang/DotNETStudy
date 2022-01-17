using Consul;
using Microsoft.AspNetCore.Mvc;

namespace DotNETStudy.Consul.ConsulApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServicesController : ControllerBase
    {
        // GET: api/<ServicesController>
        [HttpGet]
        public async Task<List<string>> Get()
        {
            var consulClient = new ConsulClient(consulConfig =>
            {
                consulConfig.Address = new Uri("http://127.0.0.1:8500");
            });

            var queryResult = await consulClient.Health.Service("ConsulApi", "", true);
            var result = new List<string>();
            foreach (var service in queryResult.Response)
            {
                result.Add(service.Service.Address + ":" + service.Service.Port);
            }

            return result;
        }
    }
}
