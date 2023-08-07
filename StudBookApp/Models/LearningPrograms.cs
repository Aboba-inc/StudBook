using System.Collections.Generic;

namespace StudBookApp.Models
{
    public class LearningProgram
    {
        public string Name { get; set; }
        public List<Subject> Subjects { get; set; }

        public LearningProgram(string name, List<Subject>? subjects = null)
        {
            Name = name;
            Subjects = subjects ?? new List<Subject>();
        }
    }
}
