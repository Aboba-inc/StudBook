using ClosedXML.Excel;
using DocumentFormat.OpenXml.Spreadsheet;
using DocumentFormat.OpenXml.Wordprocessing;
using StudBookApp.Models;
using System.Linq;
using System.Text;
using System.Text.Json;
using Group = StudBookApp.Models.Group;

namespace XlsToJsonConverter
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string learningPrograms = "data.xlsx";
            string subjects = "subjects.xlsx";

            using (var subjectsWorkbook = new XLWorkbook(Path.Combine(Environment.CurrentDirectory, subjects)))
            {
                var sWorkbook = subjectsWorkbook.Worksheet(1).RowsUsed(r => r.Cell(1).GetText() != "Група");

                var groups = sWorkbook
                    .GroupBy(s => s.Cell(1).Value)
                    .Select(gr =>
                    new Group()
                    {
                        Name = gr.Key.ToString(),
                        Faculty = int.Parse(gr.FirstOrDefault()?.Cell(2).GetText() ?? "0"),
                        Course = int.Parse(gr.FirstOrDefault()?.Cell(4).GetText() ?? "0"),
                        Subjects = gr
                                    .Select(s => new Subject()
                                    {
                                        Name = s.Cell(6).GetText(),
                                        Credits = (int.TryParse(s.Cell(9).GetText(), out int credits) ? credits : 0) / 30f
                                    })
                                    .ToList()
                    })
                    .ToList();

                //string json = JsonSerializer.Serialize(groups);
                //File.WriteAllText("groups.json", json);

                //foreach (var gr in groups)
                //{
                //    Console.WriteLine($"Group = {gr.Name} | Fac = {gr.Faculty} | Course = {gr.Course}");
                //    foreach (var sub in gr.Subjects)
                //    {
                //        Console.WriteLine($"\t{sub.Name} | {sub.Credits}");
                //    }
                //}
            }
        }
    }
}