using System;
using System.Threading;
using SkrzynexConsoleApp;
using SkrzynexConsoleApp.DbModel;
using SkrzynexConsoleApp.Infrastructure;
using Unosquare.RaspberryIO;
using Unosquare.RaspberryIO.Gpio;

namespace skrzynexconsoleapp
{
    class Program
    {
        static void Main(string[] args)
        {
            var gpioHandler = new GPIOHandler();
            //while (true)
            //{
                try
                {
                    Wait();
                    gpioHandler.ClearAllLines();
                    gpioHandler.UpdateSprinklesLinesStatues(true,false,false,false);
                    Console.WriteLine("Line1");
                    Wait();
                    gpioHandler.ClearAllLines();
                    gpioHandler.UpdateSprinklesLinesStatues(false, true, false, false);
                    Console.WriteLine("Line2");
                    Wait();
                    gpioHandler.ClearAllLines();
                    gpioHandler.UpdateSprinklesLinesStatues(false, false, true, false);
                    Console.WriteLine("Line3");
                    Wait();
                    gpioHandler.ClearAllLines();
                    gpioHandler.UpdateSprinklesLinesStatues(false, false, false, true);
                    Console.WriteLine("Line4");
                    Wait();
                    gpioHandler.ClearAllLines();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            //}
        }

        private static void Wait()
        {
            Thread.Sleep(new TimeSpan(0, 0, 0, 15));
        }
    }
}
