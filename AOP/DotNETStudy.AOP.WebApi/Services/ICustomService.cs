using DotNETStudy.AOP.WebApi.Attributes;

namespace DotNETStudy.AOP.WebApi.Services
{
    public interface ICustomService
    {
        [CustomInterceptor]
        void Call();
    }
}
