using MediatR;
using Castle.DynamicProxy;
using Microsoft.Extensions.DependencyInjection;
using DotNETStudy.AOP.ConsoleAOP;

/*
 * AOP 实现的方案
 * AOP：可以实现系统逻辑和主逻辑分离，并且系统级逻辑复用
 * 主逻辑：不得不写的逻辑
 * 系统逻辑：除了主逻辑之外的逻辑：日志、异常处理
 * 
 */

/*
 * 方案1：静态代理
 * 缺点：一个目标类对应一个代理类，如果有多个目标类，必须得有多个代理类
 */
var staticProxy = new MyServiceStaticProxy();
staticProxy.Calc(1);

/*
 * 方案2：动态代理，自动根据目标类生成代理类
 * 原理：继承：子类重写父类，只是这个类可以自动生成
 */
var factory = new ProxyGenerator();// 可以根据目标类生成子类（代理类），并且将系统级别逻辑织入到主逻辑

var dynamicProxy = factory.CreateClassProxy<MyService>(new MyInterceptor());
// dynamicProxy.GetType().BaseType: Name = "MyService"
dynamicProxy.Calc(10);

/*
 * 方案3：管道
 * 
 */
IServiceCollection services = new ServiceCollection();
services.AddTransient(typeof(IPipelineBehavior<,>), typeof(MyPipelineBehavior<,>));
services.AddMediatR(System.Reflection.Assembly.GetExecutingAssembly());
var provider = services.BuildServiceProvider();
var mediator = provider.GetService<IMediator>();
mediator?.Send(new MyRequest
{
    Number = 20
});