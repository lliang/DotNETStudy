namespace DotNETStudy.IoC.ConsoleIoC
{
    class Program
    {
        static void Main(string[] args)
        {
            Stone stone = new Stone { Age = 5000 };
            Monkey monkey = (Monkey)stone;
            System.Console.WriteLine(monkey.Age);
        }
    }

    class Stone
    {
        public int Age { get; set; }

        public static explicit operator Monkey(Stone stone)
        {
            Monkey monkey = new Monkey
            {
                Age = stone.Age / 500
            };
            return monkey;
        }
    }

    class Monkey
    {
        public int Age { get; set; }
    }
}
