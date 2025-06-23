using System;
using System.Threading;
using OtpNet;

class Program
{
    static void Main()
    {
        Console.WriteLine("Totp");
        Console.WriteLine("1. Check Totp code");
        string check = Console.ReadLine();

        if (check == "1")
        {
            while (true)
            {
                Console.WriteLine("Enter code");
                string base32 = Console.ReadLine();

                if (string.IsNullOrEmpty(base32))
                {
                    Console.Clear();
                    Console.WriteLine("Please don't enter a null value");
                    continue;
                }
                Console.Clear();

                if (base32.Any(char.IsDigit))
                {
                    Console.Clear();
                    Console.WriteLine("Dont enter a number");
                    continue;
                }

                Console.Clear();

                var KeyByte = Base32Encoding.ToBytes(base32);
                var totp = new Totp(KeyByte);

                while (true)
                {
                    string code = totp.ComputeTotp();
                    int secondsleft = totp.RemainingSeconds();

                    Console.Clear();
                    Console.WriteLine($"TOTP Code: {code}");
                    Console.WriteLine($"Seconds left: {secondsleft}");
                    Console.WriteLine("Press Enter to stop the program");

                    if (Console.KeyAvailable)
                    {
                        var key = Console.ReadKey(true);
                        if (key.Key == ConsoleKey.Enter)
                        {
                            Environment.Exit(0);
                        }
                    }

                    Thread.Sleep(1000);
                }
            }
        }
    }
}
