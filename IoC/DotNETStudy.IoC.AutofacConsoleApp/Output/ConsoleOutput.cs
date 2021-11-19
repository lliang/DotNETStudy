using System;

namespace DotNETStudy.IoC.AutofacConsoleApp
{
    public class ConsoleOutput : IOutput
    {
        public void Write(string content)
        {
            Console.WriteLine(content);
        }
    }
}
