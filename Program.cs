using System;
using System.Globalization;
using SkrzynexConsoleApp.DbModel;
using SkrzynexConsoleApp.Infrastructure;

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
                    if (args[0].ToLower().Contains("status"))
                    {
                        var gpioHandler = new GPIOHandler();
                        var handler = new SpringelsHandler(gpioHandler);
                        handler.PrintSprinklersStatuses();
                    }
                    else
                    {
                        var dateFrom = DateTime.ParseExact(args[0], "yyyy_MM_dd_HH_mm", CultureInfo.InvariantCulture);
                        var dateTo = DateTime.ParseExact(args[1], "yyyy_MM_dd_HH_mm", CultureInfo.InvariantCulture);
                        var sprinkelsHandler = new SpringelsHandler(new skrzynexContext());
                        var interval = args.Length > 2 ? Convert.ToInt32(args[2]) : (int?)null;
                        sprinkelsHandler.SetNewAction(1, dateFrom, dateTo, interval);
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
