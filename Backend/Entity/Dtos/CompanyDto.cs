namespace Entity.Dtos
{
    public class CompanyDto
    {
        public int Id { get; set; }
        public string CompanyName { get; set; }
        public string ContactName { get; set; }
        public string ContactMail { get; set; }
        public string ContactPhone { get; set; }
        public List<string> TechStack { get; set; }
        public bool Mentorship { get; set; }
        public int LiaSpots { get; set; }
        public bool HasExjob { get; set; }
        public string Presentation { get; set; }
        public string ImageUrl { get; set; }
    }
}
