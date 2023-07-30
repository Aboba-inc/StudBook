using System;

namespace StudBookApp.Model
{
    public class Faculty
    {
        public int Number { get; }
        public Cathedra[]? Cathedras;
 
        public Faculty(int number)
        {
            if (number < 1 || number > 8)
            {
                throw new ArgumentOutOfRangeException(nameof(number));
            }

            Number = number;
        }

        public Faculty(int number, Cathedra[] cathedras) : this(number)
        {
            Cathedras = cathedras;
        }
    }
}
