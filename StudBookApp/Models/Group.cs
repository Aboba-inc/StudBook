using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudBookApp.Models
{
    public class Group
    {
        public string Name { get; set; }
        public List<Subject> Subjects { get; set; }

        public Group(string name = "", List<Subject>? subjects = null)
        {
            Name = name;
            Subjects = subjects ?? new List<Subject>();
        }
    }
}
