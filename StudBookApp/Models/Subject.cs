using System;

namespace StudBookApp.Models
{
    public class Subject
    {
        public string Name { get; set; }
        public double Credits { get; set; }
        public int Grade { get; set; }

        public Subject(string name = "", double credits = 0.0, int grade = 0)
        {
            if (credits < 0 || credits > 10)
            {
                throw new ArgumentOutOfRangeException(nameof(credits));
            }

            if (grade < 0 || grade > 100)
            {
                throw new ArgumentOutOfRangeException(nameof(grade));
            }

            Name = name;
            Credits = credits;
            Grade = grade;
        }
    }
}
