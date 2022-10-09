namespace WebApplication_Vitalik.Models
{
    public class Subject
    {
        public int Id { get; set; }
        public string? NameSubject { get; set; }
        public string? Info { get; set; }
        public int? Level { get; set; }
        public int Type { get; set; }
        public int? NumberRoom { get; set; }
        public bool Block { get; set; }
        public int? Campus { get; set; }
        public int? securType { get; set; }
        public string? MinTime { get; set; }

        public Subject()
        {
        }

        public Subject(string namesubject)
        {
            NameSubject = namesubject;
        }
    }
}
