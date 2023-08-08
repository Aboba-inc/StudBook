using System.Collections.Generic;

namespace StudBookApp.Models
{
    public class Group
    {
        public string Name { get; set; }
        public int Faculty { get; set; }
        public int Course { get; set; }
        public List<Subject> Subjects { get; set; }

        public Group(string name = "", int fac = 1, int course = 1, List<Subject>? subjects = null)
        {
            Name = name;
            Faculty = fac;
            Course = course;
            Subjects = subjects ?? new List<Subject>();
        }

        public Group() { }

        public override string ToString()
        {
            return Name;
        }
    }
}
