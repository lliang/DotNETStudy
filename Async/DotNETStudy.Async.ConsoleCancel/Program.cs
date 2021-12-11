using System.Net.Http;
using System.Threading.Tasks;

namespace DotNETStudy.Async.ConsoleCancel
{
    /*
        CancellationToken 结构体

        None：空

        bool IsCancellationRequested 是否取消
        (*)Register(Action callback) 注册取消监听
        ThrowIfCancellationRequested() 如果任务被取消，执行到这就抛异常。

        Thread.Abort()

        CancellationTokenSource
        CancelAfter() 超时后发出取消信号
        Cancel() 发出取消信号
        CancellationToken Token
     */
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
