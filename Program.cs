using System;
using System.Globalization;
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
            

            try
            {
                if (args.Length == 0)
                {
                    var gpioHandler = new GPIOHandler();
                    var sprinkelsHandler = new SpringelsHandler(new skrzynexContext(), gpioHandler);
                    sprinkelsHandler.UpdateSprinkelsStatus();
                }
                else
                {
                    var dateFrom = DateTime.ParseExact(args[0],"yyyy_MM_dd_HH_mm",CultureInfo.InvariantCulture);
                    var dateTo = DateTime.ParseExact(args[1], "yyyy_MM_dd_HH_mm", CultureInfo.InvariantCulture);
                    var sprinkelsHandler = new SpringelsHandler(new skrzynexContext());
                    sprinkelsHandler.SetNewAction(1,dateFrom,dateTo);

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            
        }

    }
}
