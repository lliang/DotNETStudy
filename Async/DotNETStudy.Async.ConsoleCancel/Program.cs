using System.Net.Http;
using System.Threading.Tasks;

namespace DotNETStudy.Async.ConsoleCancel
{
    class Program
    {
        static void Main(string[] args)
        {
        }

        static async Task Download1Async(string url, int n)
        {
            using (HttpClient client = new HttpClient())
            {
                for (int i = 0; i < n; i++)
                {
                    System.Console.WriteLine(await client.GetStringAsync(url));
                }
            }
        }
    }
}
