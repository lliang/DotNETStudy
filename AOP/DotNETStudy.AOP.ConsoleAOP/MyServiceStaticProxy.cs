using System;

namespace DotNETStudy.AOP.ConsoleAOP
{
    public class MyServiceStaticProxy : MyService
    {
        void Interceptor(Action action)
        {
            Console.WriteLine("before");
            action();
            Console.WriteLine("after");
        }

        public override void Calc(int i)
        {
            Interceptor(() => base.Calc(i));
        }

        public override void Calc(long i)
        {
            Interceptor(() => base.Calc(i));
        }
    }
}
