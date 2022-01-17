using System;
using System.Net;
using System.Net.Http;
using System.Threading;
using Polly;
using Polly.Timeout;

namespace DotNETStudy.Polly.ConsoleApp
{
    /*
     * Polly 是一个被 .NET 基金会认可的弹性和瞬态故障处理库
     * 允许我们以非常顺畅和线程安全的方式来执行诸如重试、断路、超时、故障恢复等策略，
     * 其主要功能如下：
     * 1. 重试
     * 2. 断路器
     * 3. 超时检测
     * 4. 缓存
     * 5. 降级
     * 
     * Polly 的策略主要由“故障”和“动作”两个部分组成
     * “故障”可以包括异常、超时等情况
     * “动作”则包括 Fallback(降级)、重试（Retry）、熔断（Circuit-Breaker）等。
     * 
     * 策略则用来执行业务代码，当业务代码出现了“故障”中的情况时就开始执行“动作”。
     * 
     * Polly 错误处理使用三步曲：
     * 1. 定义条件：定义你要处理的错误异常或返回结果
     * 2. 定义处理方式：重试、熔断、回退
     * 3. 执行
     */
    class Program
    {
        static void Main(string[] args)
        {
            TestPolly();
            Console.ReadLine();
        }

        static void TestPolly()
        {
            var policy = CreatePolly();
            for (int i = 0; i < 100; i++)
            {
                Console.WriteLine($"--------第{i}次请求--------");
                policy.Execute(() =>
                {
                    if (i < 10)
                    {
                        Thread.Sleep(3000);
                    }
                    else
                    {
                        Console.WriteLine($"{DateTime.Now}: 请求成功");
                    }
                });
                Thread.Sleep(1000);
            }
        }

        static ISyncPolicy CreatePolly()
        {
            // 超时 1 秒
            var timeoutPolicy = Policy.Timeout(1, TimeoutStrategy.Pessimistic, (context, timeSpan, task) =>
            {
                Console.WriteLine("执行超时，抛出 TimeoutRejectedException 异常");
            });

            // 重试 2 次
            var retryPolicy = Policy.Handle<Exception>()
                .WaitAndRetry(
                2,
                retryAttempt => TimeSpan.FromSeconds(2),
                (exception, timespan, retryCount, context) =>
                {
                    Console.WriteLine($"{DateTime.Now} - 重试 {retryCount} 次 - 抛出 {exception.GetType()}-{timespan.TotalMilliseconds}");
                });

            // 连续发生两次故障，就熔断5秒
            var circuitBreakerPolicy = Policy.Handle<Exception>()
                .CircuitBreaker(
                // 熔断前允许出现几次错误
                2,
                // 熔断时间
                TimeSpan.FromSeconds(5),
                // 熔断时触发 OPEN
                onBreak: (ex, breakDelay) =>
                 {
                     Console.WriteLine($"{DateTime.Now} - 断路器：开启状态（熔断时触发）");
                 },
                // 熔断恢复时触发 CLOSE
                onReset: () =>
                {
                    Console.WriteLine($"{DateTime.Now} - 断路器：关闭状态（熔断恢复时触发）");
                },
                // 熔断时间到了之后触发，尝试放行少量（1次）的请求
                onHalfOpen: () =>
                {
                    Console.WriteLine($"{DateTime.Now} - 断路器：半开启状态（熔断时间到了之后触发）");
                }
                );

            // 回退策略，降级！
            var fallbackPolicy = Policy.Handle<Exception>()
                .Fallback(() =>
                {
                    Console.WriteLine("这是一个Fallback");
                }, exception =>
                {
                    Console.WriteLine($"Fallback异常：{exception.GetType()}");
                });

            // 策略从右到左依次进行调用
            // 首先判断调用是否超时，如果超时就会触发异常，发生超时故障，然后就触发重试策略；
            // 如果重试两次中只要成功一次，就直接返回调用结果
            // 如果重试两次都失败，第三次再次失败，就会发生故障
            // 重试之后是断路器策略，所以这个故障会被断路器接收，当断路器收到两次故障，就会触发熔断，也就是说断路器开启
            // 断路器开启的3秒内，任何故障或者操作，都会通过断路器到达回退策略，触发降级操作
            // 3秒后，断路器进入到半开启状态，操作可以正常执行
            return Policy.Wrap(fallbackPolicy, circuitBreakerPolicy, retryPolicy, timeoutPolicy);
        }

        /// <summary>
        /// 定义异常错误的条件
        /// </summary>
        static void ExceptionCondition()
        {
            // 单个异常类型
            Policy.Handle<Exception>();

            // 限定条件的单个异常
            Policy.Handle<Exception>(ex => ex.Message == "请求超时");

            // 多个异常类型
            Policy.Handle<Exception>().Or<ArgumentException>();

            // 限定条件的多个异常
            Policy.Handle<Exception>(ex => ex.Message == "请求超时")
                .Or<ArgumentException>(ex => ex.ParamName == "ID");

            // Inner Exception 异常里面的异常类型
            Policy.HandleInner<Exception>().OrInner<ArgumentException>(ex => ex.ParamName == "ID");
        }

        /// <summary>
        /// 定义返回结果的条件
        /// </summary>
        static void ReturnCondition()
        {
            // 返回结果加限定条件
            Policy.HandleResult<HttpResponseMessage>(r => r.StatusCode == HttpStatusCode.NotFound);

            // 处理多个返回结果
            Policy.HandleResult<HttpResponseMessage>(r => r.StatusCode == HttpStatusCode.InternalServerError)
                .OrResult(r => r.StatusCode == HttpStatusCode.BadGateway);

            // 处理元类型结果（用 .Equals）
            Policy.HandleResult(HttpStatusCode.InternalServerError)
                .OrResult(HttpStatusCode.BadGateway);
        }

        /// <summary>
        /// 重试
        /// 当发生某种错误或者返回某种结果的时候进行重试
        /// </summary>
        static void Retry()
        {
            // 重试 1 次
            Policy.Handle<Exception>().Retry();

            // 重试 3 次
            Policy.Handle<Exception>().Retry(3);

            // 重试 3 次
            Policy.Handle<Exception>().Retry(3, (exception, retryCount) =>
            {
                // do something
            });

            // 不断重试，直到成功
            Policy.Handle<Exception>().RetryForever();

            // 不断重试，带 action 参数在每次重试的时候执行
            Policy.Handle<Exception>().RetryForever(exception =>
            {
                // do something
            });

            // 重试 3 次，每次等待 5 s
            Policy.Handle<Exception>().WaitAndRetry(
                3,
                retryAttempt => TimeSpan.FromSeconds(5),
                (exception, timeSpan, retryCount, context) =>
                {
                    // do something
                });

            // 重试 3 次，分别等待 1、2、3 秒
            Policy.Handle<Exception>().WaitAndRetry(new[]
            {
                TimeSpan.FromSeconds(1),
                TimeSpan.FromSeconds(2),
                TimeSpan.FromSeconds(3)
            });
        }

        /// <summary>
        /// 熔断
        /// 熔断也可以被作为当遇到某种错误场景下的一个操作
        /// 
        /// 打开（Open）- 熔断器打开状态，此时对目标服务的调用都直接返回错误，熔断周期内不会走网络请求，当熔断周期结束时进入半开状态；
        /// 关闭（Closed）- 关闭状态下正常发送网络请求，但会记录符合熔断条件的连续执行次数，如果错误数量达到设定的阈值（如果在没有达到阈值之前恢复正常，
        /// 之前的累积次数将会归零），熔断状态进入到打开状态；
        /// 半开（Half-Open）- 半开状态下允许定量的服务请求，如果调用都成功则认为恢复了，关闭熔断器，否则认为还没好，又回到熔断器打开状态；
        /// </summary>
        static void CircuitBreaker()
        {
            Policy.Handle<Exception>().CircuitBreaker(
                // 熔断前允许出现几次错误
                3,
                // 熔断时间
                TimeSpan.FromSeconds(5),
                // 熔断时触发
                onBreak: (ex, breakDelay) =>
                {
                    Console.WriteLine("断路器：开启状态（熔断时触发）");
                },
                // 熔断恢复时触发
                onReset: () =>
                {
                    Console.WriteLine("断路器：关闭状态（熔断恢复时触发）");
                },
                // 熔断时间到了之后触发，尝试放行少量（1次）的请求
                onHalfOpen: () =>
                {
                    Console.WriteLine("断路器：半开启状态（熔断时间到了之后触发）");
                }
                );
        }

        /// <summary>
        /// Polly中关于超时的两个策略：一个是悲观策略（Pessimistic），一个是乐观策略（Optimistic）。
        /// 其中，悲观策略超时后会直接抛异常，而乐观策略则不会，而只是触发 CancellationTokenSource.Cancel函数，需要等待委托自行终止操作。
        /// 一般情况下，我们都会用悲观策略。
        /// </summary>
        static async void Timeout()
        {
            // 设置超时时间为 30 s
            Policy.Timeout(30, onTimeout: (context, TimeSpan, task, ex) =>
            {
                // do something
            });

            // 超时分为乐观超时与悲观超时，乐观超时依赖于 CancellationToken，它假设我们的具体执行的任务都支持 CancellationToken。
            // 那么在进行 timeout 的时候，它会通知执行线程取消并终止执行线程，避免额外的开销。
            HttpResponseMessage httpResponse = await Policy.TimeoutAsync(30)
                .ExecuteAsync(
                async ct => await new HttpClient().GetAsync(""),
                CancellationToken.None
                );

            // 悲观超时与乐观超时的区别在于，如果执行的代码不支持取消 CancellationToken,
            // 它还会继续执行，这会是一个比较大的开销。
            Policy.Timeout(30, TimeoutStrategy.Pessimistic);
        }

        /// <summary>
        /// 舱壁
        /// 用来限制某一个操作的最大并发执行数量
        /// </summary>
        static void Bulkhead()
        {
            Policy.Bulkhead(12);

            // 同时，我们还可以控制一个等待处理的队列长度
            Policy.Bulkhead(12, 2);

            // 以及当请求执行操作被拒绝的时候，执行回调
            Policy.Bulkhead(12, context =>
            {
                // do something
            });
        }

        /// <summary>
        /// 回退（Fallback）
        /// 降级的目的就是当某个服务提供者发生故障的时候，向调用方返回一个替代响应或者错误响应。
        /// </summary>
        static void Fallback()
        {
            Policy.Handle<Exception>().Fallback(() =>
            {
                // do something
            });
        }

        /// <summary>
        /// 组合 Polly
        /// </summary>
        static void Wrap()
        {
            // 使用 Wrap 方法，将多个 policy 组合起来，其中策略的优先级是右到左。
            var policyWrap = Policy.Wrap();
        }
    }
}
