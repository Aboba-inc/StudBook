using System;
using System.Collections.Generic;

namespace StudBookApp.Models
{
    public class Specialty
    {
        public int Number { get; set; }
        public List<LearningProgram>? LearningPrograms { get; set; }

        public Specialty(int number, List<LearningProgram>? learningPrograms = null)
        {
            if (number < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(number));
            }

            Number = number;
            LearningPrograms = learningPrograms ?? new List<LearningProgram>();
        }
    }
}
}
