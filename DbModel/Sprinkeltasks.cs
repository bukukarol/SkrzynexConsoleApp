using System;
using System.Collections.Generic;

namespace SkrzynexConsoleApp.DbModel
{
    public partial class Sprinkeltasks
    {
        public Sprinkeltasks()
        {
            Sprinkletaskaction = new HashSet<Sprinkletaskaction>();
        }

        public int Id { get; set; }
        public int WateringMode { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime CreationTime { get; set; }
        public sbyte Deleted { get; set; }

        public ICollection<Sprinkletaskaction> Sprinkletaskaction { get; set; }
    }
}
