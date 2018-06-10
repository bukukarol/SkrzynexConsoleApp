using System;

namespace SkrzynexConsoleApp.DbModel
{
    public partial class Sprinkletaskaction
    {
        public int Id { get; set; }
        public int TaskId { get; set; }
        public int Line { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public Sprinkeltasks Task { get; set; }
    }
}
