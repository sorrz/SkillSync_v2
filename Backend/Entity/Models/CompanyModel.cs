namespace Entity.Models
{
    public class CompanyModel
    {
        public int Id { get; set; }
        public string CompanyName { get; set; }
        public string ContactName { get; set; }
        public string ContactMail { get; set; }
        public string ContactPhone { get; set; }
        public string PasswordHash { get; set; }
        public List<string> TechStack { get; set; }
        public bool Mentorship { get; set; }
        public int Lia1Spots { get; set; }
        public int Lia2Spots { get; set; }
        public bool HasExjob { get; set; }
        public string Presentation { get; set; }
        public string ImageUrl { get; set; }
    }
}
