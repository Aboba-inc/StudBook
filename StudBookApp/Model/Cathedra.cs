using System;

namespace StudBookApp.Model
{
    internal class Cathedra
    {
        public int Number { get; set; }
        public Specialty[]? Specialties { get; set; }

        public Cathedra(int number)
        {
            if (number < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(number));
            }

            Number = number;
        }

        public Cathedra(int number, Specialty[] specialties) : this(number)
        {
            Specialties = specialties;
        }
    }
}
