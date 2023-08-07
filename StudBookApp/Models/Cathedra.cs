using System;
using System.Collections.Generic;

namespace StudBookApp.Models
{
    public class Cathedra
    {
        public int Number { get; set; }
        public List<Specialty>? Specialties { get; set; }

        public Cathedra(int number, List<Specialty>? specialties = null)
        {
            if (number < 1)
            {
                throw new ArgumentOutOfRangeException(nameof(number));
            }

            Number = number;
            Specialties = specialties ?? new List<Specialty>();
        }
    }
}
