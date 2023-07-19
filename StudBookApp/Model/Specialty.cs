using System;

namespace StudBookApp.Model
{
    internal class Specialty
    {
        public int Number { get; set; }
        public LearningProgram[]? LearningPrograms { get; set; }

        public Specialty(int number)
        {
            if (number < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(number));
            }

            Number = number;
        }

        public Specialty(int number, LearningProgram[] learningPrograms) : this(number)
        {
            LearningPrograms = learningPrograms;
        }
    }
}
