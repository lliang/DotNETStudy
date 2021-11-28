using System;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

/// <summary>
/// “异步方法”：用 async 关键字修饰的方法
/// 1. 异步方法的返回值一般是 Task<T>, T 是真正的返回值类型；
/// 2. 异步方法的名字以 Async 结尾；
/// 3. 即使方法没有返回值，也最好把返回值声明为非泛型的 Task；
/// 4. 调用泛型方法时，一般在方法调用前加上 await 关键字，这样拿到的返回值就是泛型指定的 T 类型；
/// 5. 泛型方法的“传染性”：一个方法内部如果有 await 调用，则这个方法必须修饰为 async；
/// 6. 同样的功能，既有同步方法，又有异步方法，首先使用异步方法。
/// </summary>
namespace DotNETStudy.Async.ConsoleApp
{
    class Program
    {
        // 通过反编译软件查看代码
        // await、async 是“语法糖”，最终编译成“状态机调用”
        static async Task Main(string[] args)
        {
            using (HttpClient httpClient = new HttpClient())
            {
                string html = await httpClient.GetStringAsync("https://docs.microsoft.com/zh-cn/");
                Console.WriteLine(html);
            }

            string destFilePaht = "d:/hello.txt";
            await File.WriteAllTextAsync(destFilePaht, "Hello async and await");

            string content = await File.ReadAllTextAsync(destFilePaht);
            Console.WriteLine(content);
        }

        static void Main3(string[] args)
        {
            // 异步委托示例
            ThreadPool.QueueUserWorkItem(async s =>
            {
                while (true)
                {
                    await Task.Delay(1000);
                    await File.WriteAllTextAsync("d:/hello.txt", DateTime.Now.ToString());
                }
            });

            /*            ThreadPool.QueueUserWorkItem(s =>
                        {
                            while (true)
                            {
                                Console.WriteLine(DateTime.Now);
                            }
                        });*/

            Console.ReadKey();
        }

        static async Task Main2(string[] args)
        {
            var length = await DownloadService.DownloadHtmlAsync("https://docs.microsoft.com/zh-cn/", @"d:\html.txt");
            System.Console.WriteLine(length);

            // 不使用 await 关键字，通过 Task<T>.Result 属性取得异步方法返回值
            Task<string> readTask = File.ReadAllTextAsync(@"d:\html.txt");
            string htmlString = readTask.Result;
            Console.WriteLine(htmlString.Substring(0, 20));

            // 不使用 await 关键字，通过 Task.Wait() 方法等待异步无返回值方法返回
            Task writeTask = File.WriteAllTextAsync("d:/hello.txt", "Hello, Async!");
            writeTask.Wait();
        }

        static async Task Main1(string[] args)
        {
            string fileName = "d:/hello.txt";
            File.Delete(fileName);

            // await 等待写入文件完成
            // 当写入数据量大的时候，不等待就读取文件，会造成 System.IO.IOException（读写进程争用文件句柄）
            await File.WriteAllTextAsync(fileName, "Hello, World!");

            /*            // 示例：异步写入文件不等待
                        StringBuilder sb = new StringBuilder();
                        for (int i = 0; i < 1000; i++)
                        {
                            sb.AppendLine($"Number: {i}");
                        }
                        // System.IO.IOException:“The process cannot access the file 'd:\hello.txt' because it is being used by another process.”
                        File.WriteAllTextAsync(fileName, sb.ToString());*/

            string str = await File.ReadAllTextAsync(fileName);
            Console.WriteLine(str);
        }
    }
}
