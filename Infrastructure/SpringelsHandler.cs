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
        private readonly skrzynexContext _db;
        private readonly GPIOHandler _gpioHandler;
        private int _actionChangeInterval = 15;

        public SpringelsHandler(skrzynexContext db,GPIOHandler gpioHandler)
        {
            _db = db;
            _gpioHandler = gpioHandler;
        }

        public SpringelsHandler(skrzynexContext db)
        {
            _db = db;
        }

        public SpringelsHandler(GPIOHandler gpioHandler)
        {
            _gpioHandler = gpioHandler;
        }

        #region UpdateStatus

        public void UpdateSprinkelsStatus()
        {
            var nowDate = DateTime.Now;
            var activeTask = _db.Sprinkeltasks.FirstOrDefault(x => x.StartDate < nowDate && x.EndDate >= nowDate);
            if (activeTask == null)
            {
                _gpioHandler.ClearAllLines();
            }
            else
            {
                var actions = _db.Sprinkletaskaction.Where(x => x.TaskId == activeTask.Id);
                UpdateLinesStatuses(actions);
            }
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

        #endregion

        public void SetNewAction(int mode, DateTime from, DateTime to,int? customActionInterval=null)
        {
            if (customActionInterval.HasValue) _actionChangeInterval = customActionInterval.Value;
            var newTask = new Sprinkeltasks()
            {
                CreationTime = DateTime.Now,
                StartDate = from,
                EndDate = to,
                WateringMode = mode,
            };
            _db.Add(newTask);
            _db.SaveChanges();
            List<Sprinkletaskaction> tasksActionsList;
            switch (mode)
            {
                case 1:
                    tasksActionsList = GetSigleLineActionForTask(newTask.Id, from, to);
                    break;
                case 2:
                case 4:
                default:
                throw new Exception("Not implemented action mode");
            }
            _db.AddRange(tasksActionsList);
            _db.SaveChanges();
            Console.WriteLine($"Task {newTask.Id} created ");
        }

        private List<Sprinkletaskaction> GetSigleLineActionForTask(int taskId, DateTime from, DateTime to)
        {
            var result = new List<Sprinkletaskaction>();
            var line = 0;
            for (var date = from; date < to; date=date.AddMinutes(_actionChangeInterval))
            {
                result.Add(new Sprinkletaskaction()
                {
                    TaskId = taskId,
                    StartDate =  date,
                    EndDate = date.AddMinutes(_actionChangeInterval),
                    Line = line+1,
                });
                line = (line + 1) % 4;
            }
            return result;
        }

        public void PrintSprinklersStatuses()
        {
            PrintLineStatusInfo(1);
            PrintLineStatusInfo(2);
            PrintLineStatusInfo(3);
            PrintLineStatusInfo(4);
        }

        private void PrintLineStatusInfo(int lineNr)
        {
            var text = $"Line{lineNr} status: {_gpioHandler.GetLineStatus(lineNr)}";
            Console.WriteLine(text);
        }
    }
}
