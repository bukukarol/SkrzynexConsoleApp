using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SkrzynexConsoleApp.DbModel;
using Unosquare.RaspberryIO;
using Unosquare.RaspberryIO.Gpio;

namespace SkrzynexConsoleApp.Infrastructure
{
    public class SpringelsHandler
    {
        private skrzynexContext _db;
        private GPIOHandler _gpioHandler;


        public SpringelsHandler(skrzynexContext db,GPIOHandler gpioHandler)
        {
            _db = db;
            _gpioHandler = gpioHandler;
        }

        public void UpdateSprinkelsStatus()
        {
            var nowDate = DateTime.Now;
            var activeAction = _db.Sprinkeltasks.FirstOrDefault(x => x.StartDate < nowDate && x.EndDate >= nowDate);
            if(activeAction==null) return;
            UpdateLinesStatuses(activeAction.Sprinkletaskaction);
        }

        private void UpdateLinesStatuses(IEnumerable<Sprinkletaskaction> taskes)
        {
            var nowDate = DateTime.Now;
            var activeActions = taskes.Where(x => x.StartDate < nowDate && x.EndDate >= nowDate).ToList();
            var line1Action = activeActions.Any(x => x.Line == 1);
            var line2Action = activeActions.Any(x => x.Line == 2);
            var line3Action = activeActions.Any(x => x.Line == 3);
            var line4Action = activeActions.Any(x => x.Line == 4);
            _gpioHandler.UpdateSprinklesLinesStatues(line1Action, line2Action, line3Action, line4Action);
        }
    }
}
