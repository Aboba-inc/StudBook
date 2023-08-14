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
            Name = name;
            Credits = credits;
            Grade = grade;
        }
    }
}
