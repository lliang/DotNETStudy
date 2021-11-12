using System;

namespace DotNETStudy.AOP.ConsoleAOP
{
    /// <summary>
    /// 主逻辑
    /// </summary>
    public class MyService
    {
        public virtual void Calc(int i)
        {
            Console.WriteLine((i + 1) / i);
        }

        public virtual void Calc(long i)
        {
            Console.WriteLine((i + 1) / i);
        }
    }
}
