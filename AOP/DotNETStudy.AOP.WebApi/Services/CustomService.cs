namespace DotNETStudy.AOP.WebApi.Services
{
    public class CustomService : ICustomService
    {
        public void Call()
        {
            Console.WriteLine("service calling...");
        }
    }
}
