using System;
using System.IO;
using System.Text;
using System.Net.Http;
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
        static async Task Main(string[] args)
        {
            Console.WriteLine($"Before call `CalcAsync` method: CalcAsync-ThreadId: {Thread.CurrentThread.ManagedThreadId}");
            await Calc1Async(50);
            Console.WriteLine($"After call `CalcAsync` method: CalcAsync-ThreadId: {Thread.CurrentThread.ManagedThreadId}");

            // 如果想在异步方法中暂停一段时间，不要用 Thread.Sleep(), 因为它会阻塞调用线程，而要用 await Task.Delay()
        }

        // await 调用的等待期间，.NET 会把当前的线程返回给线程池，等异步方法调用执行完毕后，框架
        // 会从线程池再取出来一个线程（也可能是前面的线程）执行后续的代码
        // Thread.CurrentThread.ManagedThreadId 获得当前线程 Id
        // 验证：在耗时异步（写入大字符串）操作前后分别打印线程 Id
        static async Task Main5(string[] args)
        {
            Console.WriteLine($"Current thread id: {Thread.CurrentThread.ManagedThreadId}");

            // 如果写入内容少，可能会发现线程 Id 不变
            // 到要等待的时候，如果发现已经执行结束了，那就没必要再切换线程了，剩下的代码就继续在之前的线程上继续执行了
            StringBuilder stringBuilder = new StringBuilder();
            for (int i = 0; i < 1000; i++)
            {
                stringBuilder.AppendLine($"Number: {i}");
            }

            await File.WriteAllTextAsync("d:/number.txt", stringBuilder.ToString());

            Console.WriteLine($"Writed thread id: {Thread.CurrentThread.ManagedThreadId}");
        }

        // 通过反编译软件查看代码
        // await、async 是“语法糖”，最终编译成“状态机调用”
        // async 的方法会被 C# 编译器编译成一个类，会主要根据 await 调用进行切分为多个状态
        // 对 async 方法的调用会被拆分为对 MoveNext 的调用
        static async Task Main4(string[] args)
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

        static async Task<decimal> Calc1Async(int n)
        {
            /*            // 问题代码：并没有手动放到线程中执行
                        Console.WriteLine($"CalcAsync-ThreadId: {Thread.CurrentThread.ManagedThreadId}");

                        decimal result = 1;

                        Random random = new Random();

                        for (int i = 0; i < n * n; i++)
                        {
                            result += (decimal)random.NextDouble();
                        }

                        return result;*/

            return await Task.Run(() =>
            {
                Console.WriteLine($"CalcAsync-ThreadId: {Thread.CurrentThread.ManagedThreadId}");

                decimal result = 1;

                Random random = new Random();

                for (int i = 0; i < n * n; i++)
                {
                    result += (decimal)random.NextDouble();
                }

                return result;
            });
        }

        static Task<decimal> Calc2Async(int n)
        {
            return Task.Run(() =>
            {
                Console.WriteLine($"CalcAsync-ThreadId: {Thread.CurrentThread.ManagedThreadId}");

                decimal result = 1;

                Random random = new Random();

                for (int i = 0; i < n * n; i++)
                {
                    result += (decimal)random.NextDouble();
                }

                return result;
            });
        }

        /*
         * async 方法缺点：
         * 1. 异步方法会生成一个类，运行效率没有普通方法高；
         * 2. 可能会占用非常多的线程；
         */
        static async Task<string> ReadFile1Async(int num)
        {
            switch (num)
            {
                case 1:
                    return await File.ReadAllTextAsync("d:/hello.txt");
                case 2:
                    return await File.ReadAllTextAsync("d:/number.txt");
                default:
                    throw new ArgumentException("num invalid");
            }
        }

        /*
         * 不用 async：不“拆完了再装”
         * 发编译调用该方法代码：只是普通的方法调用
         * 优点：运行效率高，不会造成线程浪费
         * 如果一个异步方法只是对别的异步方法调用的转发，并没有太多复杂的逻辑
         * （比如等待 A 的结果，再调用 B；把 A 调用的返回值拿到内部做一些处理再返回），
         * 那么就可以去掉 async 关键字。
         */
        static Task<string> ReadFile2Async(int num)
        {
            switch (num)
            {
                case 1:
                    return File.ReadAllTextAsync("d:/hello.txt");
                case 2:
                    return File.ReadAllTextAsync("d:/number.txt");
                default:
                    throw new ArgumentException("num invalid");
            }
        }
    }
}
