using System.Collections.Generic;

namespace StudBookApp.Model
{
    public class LearningProgram
    {
        public string Name { get; set; }
        public List<Subject>? Subjects { get; set; }

        public LearningProgram(string name)
        {
            Name = name;
        }

        public LearningProgram(string name, List<Subject> subjects) : this(name)
        {
            Subjects = subjects;
        }
    }
}
