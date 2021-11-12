using System;
using Castle.DynamicProxy;

namespace DotNETStudy.AOP.ConsoleAOP
{
    /// <summary>
    /// 拦截器
    /// 系统级别的逻辑
    /// </summary>
    public class MyInterceptor : IInterceptor
    {
        public void Intercept(IInvocation invocation)
        {
            var name = invocation.Method.Name;
            Console.WriteLine($"Execute `{name}` method before");
            invocation.Proceed();
            Console.WriteLine($"Execute `{name}` method after");
        }
    }
}
