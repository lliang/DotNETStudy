using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace DotNETStudy.Async.ConsoleApp
{
    internal class DownloadService
    {
        public static async Task<int> DownloadHtmlAsync(string url, string fileName)
        {
            string htmlString;
            // 进阶使用 HttpClientFactory
            using (HttpClient httpClient = new HttpClient())
            {
                htmlString = await httpClient.GetStringAsync(url);
            }
            await File.WriteAllTextAsync(fileName, htmlString);

            return htmlString.Length;
        }
    }
}
