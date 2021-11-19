using System;

namespace DotNETStudy.IoC.AutofacConsoleApp
{
    public class MathStudy : IStudy
    {
        private string? _record = default;

        public MathStudy()
        {

        }

        public MathStudy(string record)
        {
            _record = record;
        }

        public void Study()
        {
            Console.WriteLine($"Study {_record ?? "section 1"} math.");
        }
    }
}
