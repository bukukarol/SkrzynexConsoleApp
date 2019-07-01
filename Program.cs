using System;
using System.Globalization;
using SkrzynexConsoleApp.DbModel;
using SkrzynexConsoleApp.Infrastructure;
using Unosquare.RaspberryIO;
using Unosquare.WiringPi;

namespace skrzynexconsoleapp
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Pi.Init<BootstrapWiringPi>();
                if (args.Length == 0)
                {
                    var gpioHandler = new GPIOHandler();
                    var sprinkelsHandler = new SpringelsHandler(new skrzynexContext(), gpioHandler);
                    sprinkelsHandler.UpdateSprinkelsStatus();
                }
                else
                {
                    if (args[0].ToLower().Contains("status"))
                    {
                        var gpioHandler = new GPIOHandler();
                        var handler = new SpringelsHandler(gpioHandler);
                        handler.PrintSprinklersStatuses();
                    }
                    else if (args[0].ToLower().Contains("testall"))
                    {
                        var gpioHandler = new GPIOHandler();
                        gpioHandler.TurnOnAllLines();
                    }
                    else
                    {
                        var dateFrom = DateTime.ParseExact(args[0], "yyyy_MM_dd_HH_mm", CultureInfo.InvariantCulture);
                        var minutesOfWatering = Int32.Parse(args[1],CultureInfo.InvariantCulture);
                        var dateTo = dateFrom.AddMinutes(minutesOfWatering);
                        var repeatDays = Int32.Parse(args[2], CultureInfo.InvariantCulture);
                        var interval = args.Length > 2 ? Convert.ToInt32(args[3]) : (int?)null;
                        var sprinkelsHandler = new SpringelsHandler(new skrzynexContext());
                        sprinkelsHandler.SetNewAction(1, dateFrom, dateTo, interval);
                        for (int i = 1; i <= repeatDays; i++)
                        {
                            sprinkelsHandler.SetNewAction(1, dateFrom.AddDays(i), dateTo.AddDays(i), interval);
                        }
                        
                    }
                    
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            
        }

    }
}
