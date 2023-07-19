namespace StudBookApp.Model
{
    internal class Subject
    {
        public string Name { get; set; }
        public double Score { get; set; }

        public double Credits { get; set; }

        public Subject(string name = "Name", double score = 0.0, double creadits = 0.0)
        {
            Name = name;
            Score = score;
            Credits = creadits;
        }
    }
}
