using System;
using Autofac;

namespace DotNETStudy.IoC.AutofacConsoleApp
{
    public class Program
    {
        /*
         * Add Autofac references.
         * At application startup…
         * Create a ContainerBuilder.
         * Register components.
         * Build the container and store it for later use.
         * During application execution…
         * Create a lifetime scope from the container.
         * Use the lifetime scope to resolve instances of the components.
         */

        private static IContainer Container { get; set; }

        static void Main(string[] args)
        {
            AutofacExt.InitAutofac();
            var writer = AutofacExt.GetFromAutofac<IOutput>();
            writer.Write("Hello, World!");

            Console.ReadKey();
        }

        static void Main4(string[] args)
        {
            var builder = new ContainerBuilder();

            // 单例
            builder.RegisterType<EnglishStudy>().SingleInstance();
            builder.RegisterType<MathStudy>().AsSelf().WithParameter(new TypedParameter(typeof(string), "section 6"));

            Container = builder.Build();

            using (var scope = Container.BeginLifetimeScope())
            {
                // 解析时传参
                var englishStudy = scope.Resolve<EnglishStudy>(new NamedParameter("record", "section 3"));
                Console.WriteLine(englishStudy.GetHashCode());

                var mathStudy = scope.Resolve<MathStudy>();

                var otherEnglishStudy = scope.Resolve<EnglishStudy>(new NamedParameter("record", "section 4"));
                Console.WriteLine(otherEnglishStudy.GetHashCode());

                englishStudy.Study();
                mathStudy.Study();
            }
        }

        static void Main3(string[] args)
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<EnglishStudy>().AsSelf().As<IStudy>();
            // 注册时传参
            builder.RegisterType<MathStudy>().AsSelf().WithParameter(new TypedParameter(typeof(string), "section 6"));

            Container = builder.Build();

            using (var scope = Container.BeginLifetimeScope())
            {
                // 解析时传参
                var englishStudy = scope.Resolve<EnglishStudy>(new NamedParameter("record", "section 3"));
                var mathStudy = scope.Resolve<MathStudy>();

                englishStudy.Study();
                mathStudy.Study();
            }

            Console.ReadKey();

        }

        static void Main2(string[] args)
        {
            // 注册
            var builder = new ContainerBuilder();
            builder.RegisterType<EnglishStudy>().AsSelf().As<IStudy>();
            builder.RegisterType<MathStudy>().AsSelf();

            Container = builder.Build();

            using (var scope = Container.BeginLifetimeScope())
            {
                // 解析
                var englishStudy = scope.Resolve<EnglishStudy>();
                var mathStudy = scope.Resolve<MathStudy>();

                englishStudy.Study();
                mathStudy.Study();
            }

            Console.ReadKey();
        }

        static void Main1(string[] args)
        {
            var builder = new ContainerBuilder();
            // 注册组件
            // 组件是一种表达式、.NET 类型或其他代码位，这些代码会暴露一项或多项服务，并可以接收其他依赖项。
            builder.RegisterType<ConsoleOutput>().As<IOutput>();
            builder.RegisterType<TodayWriter>().As<IDateWriter>();

            Container = builder.Build();

            WriteDate();
        }

        public static void WriteDate()
        {
            using (var scope = Container.BeginLifetimeScope())
            {
                var writer = scope.Resolve<IDateWriter>();
                writer.WriteDate();
            }
        }
    }
}