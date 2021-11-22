using System;

namespace DotNETStudy.AOP.ConsoleDP
{
    public class Service : IService
    {
        public void Run([Valid] ISample sample)
        {
            Console.WriteLine("Ok");
        }
    }
}
