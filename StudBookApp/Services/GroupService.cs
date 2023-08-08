using StudBookApp.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;

namespace StudBookApp.Services
{
    public class GroupService
    {
        readonly string _projectDirectory = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.Parent.FullName;
        public async Task<IEnumerable<Group>> GetGroups()
        {
            var dataFile = $"groups.json";
            List<Group> items;

            //read data
            using (var sr = new StreamReader(Path.Combine(_projectDirectory, dataFile)))
            {
                var json = sr.ReadToEnd();
                items = JsonSerializer.Deserialize<List<Group>>(json);
            }

            return items;
        }
    }
}
