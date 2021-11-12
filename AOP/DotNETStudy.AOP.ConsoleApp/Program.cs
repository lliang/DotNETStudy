using System;
using System.Diagnostics;
using System.IO;

namespace DotNETStudy.AOP.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                var proxy = new MyService2Proxy(new Interceptor());
                proxy.GetFileStream("xxx.xxx");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            Console.ReadKey();
        }
    }

    /// <summary>
    /// 存在问题：没有参数检查逻辑，程序不健壮
    /// 待实现功能：需要给调用者提示，并记录日志
    /// </summary>
    public class MyService
    {
        public int Calc(int a, int b)
        {
            // 如果 a 是 0 会抛异常
            return (a + b) / a; // 主要的业务逻辑
        }

        public Stream GetFileStream(string filePath)
        {
            // 如果文件路径不存在会抛异常
            return new FileStream(filePath, FileMode.Open);// 主要的业务逻辑
        }
    }

    #region 方案1: 参数检查 或者 异常捕获

    /// <summary>
    /// 方案1
    /// 存在不足：判断逻辑或者 try-catch 代码重复。
    /// </summary>
    public class MyService1
    {
        public int Calc(int a, int b)
        {
            if (a == 0)
            {
                throw new Exception("参数不能为0");
            }

            return (a + b) / a;
        }

        public Stream GetFileStream(string filePath)
        {
            if (!File.Exists(filePath))
            {
                throw new Exception("文件路径不存在");
            }

            return new FileStream(filePath, FileMode.Open);
        }

        public int Calc1(int a, int b)
        {
            try
            {
                return (a + b) / a;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);// 输出错误信息：系统级别的逻辑
                throw new Exception("错误，请稍后再试一下");
            }
        }

        public Stream GetFileStream1(string filePath)
        {
            try
            {
                return new FileStream(filePath, FileMode.Open);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);// 输出错误信息：系统级别的逻辑
                throw new Exception("错误，请稍后再试一下");
            }
        }
    }

    #endregion

    #region 方案2：代理类 + 拦截器

    public class MyService2
    {
        public virtual void Calc(int a, int b)
        {
            // 如果 a 是 0 会抛异常
            Console.WriteLine((a + b) / a); // 主要的业务逻辑
        }

        public virtual void GetFileStream(string filePath)
        {
            // 如果文件路径不存在会抛异常
            var stream = new FileStream(filePath, FileMode.Open);// 主要的业务逻辑
            Console.WriteLine($"Stream length: {stream.Length}");
        }
    }

    /// <summary>
    /// 生成代理类，子类重写父类方法
    /// </summary>
    public class MyService2Proxy : MyService2
    {
        readonly Interceptor _interceptor;

        public MyService2Proxy(Interceptor interceptor)
        {
            _interceptor = interceptor;
        }

        public override void Calc(int a, int b)
        {
            var invoke = new Invoketion(() => base.Calc(a, b));

            _interceptor.Intercept(invoke);
        }

        public override void GetFileStream(string filePath)
        {
            var invoke = new Invoketion(() => base.GetFileStream(filePath));

            _interceptor.Intercept(invoke);
        }
    }

    public class Invoketion
    {
        Action _action;

        public Invoketion(Action action)
        {
            _action = action;
        }

        public object Target { get; set; }

        public void Proceed()
        {
            _action();
        }
    }

    /// <summary>
    /// 拦截器
    /// AOP：可以对业务方法进行环绕
    /// 好处：1. 代码复用；2. 非侵入式。
    /// IOC：降低对象之间的依赖和复杂度
    /// AOP：降低业务逻辑之间的依赖和复杂度
    /// IOC + AOP：极大的提高代码的利用率
    /// </summary>
    public class Interceptor
    {
        /// <summary>
        /// 拦截
        /// </summary>
        /// <param name="invoketion"></param>
        /// <exception cref="Exception"></exception>
        public void Intercept(Invoketion invoketion)
        {
            // 这里写的都是一些系统级别的逻辑
            try
            {
                var stopwatch = new Stopwatch();
                stopwatch.Start();
                invoketion.Proceed();// 执行主逻辑
                stopwatch.Stop();
                Console.WriteLine(stopwatch.ElapsedMilliseconds);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);// 输出错误信息：系统级别的逻辑
                throw new Exception("错误，请稍后再试一下");
            }
        }
    }

    #endregion
}
