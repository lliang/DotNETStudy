using System;

namespace DotNETStudy.IoC.AutofacConsoleApp
{
    public class EnglishStudy : IStudy
    {
        private string? _record = default;

        public EnglishStudy()
        {

        }

        public EnglishStudy(string record)
        {
            _record = record;
        }

        public void Study()
        {
            Console.WriteLine($"Study {_record ?? "section 1"} english.");
        }
    }
}
