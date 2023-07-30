using System;

namespace StudBookApp.Model
{
    public class Cathedra
    {
        public int Number { get; set; }
        public Specialty[]? Specialties { get; set; }

        public Cathedra(int number)
        {
            if (number < 1)
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
