using System;
using System.Collections.Generic;

namespace StudBookApp.Model
{
    internal class Cathedra
    {
        public int Number { get; set; }
        public List<Subject> Subjects { get; set; }

        public Cathedra(int number)
        {
            if (number < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(number));
            }

            Number = number;
        }

        public Cathedra(int number, List<Subject> subjects) : this(number)
        {
            Subjects = subjects;
        }
    }
}
