using System;

namespace StudBookApp.Model
{
    internal class Subject
    {
        public string Name { get; set; }
        public double Credits { get; set; }
        public int Grade { get; set; }

        public Subject(string name, double creadits = 0.0, int grade = 0)
        {
            if (creadits < 0 || creadits > 10)
            {
                throw new ArgumentOutOfRangeException(nameof(creadits));
            }

            Name = name;
            Credits = creadits;
            Grade = grade;
        }
    }
}
