using System;
using System.Collections.Generic;

namespace StudBookApp.Models
{
    public class Faculty
    {
        public int Number { get; }
        public List<Cathedra> Cathedras { get; set; }

        public Faculty(int number, List<Cathedra>? cathedras = null)
        {
            if (number < 1 || number > 8)
            {
                throw new ArgumentOutOfRangeException(nameof(number));
            }

            Number = number;
            Cathedras = cathedras ?? new List<Cathedra>();
        }
    }
}
