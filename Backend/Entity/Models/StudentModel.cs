namespace Entity.Models
{
    public class StudentModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string MailAddress { get; set; }
        public string? PasswordHash { get; set; }
        public string StudentSalt { get; set; }
        public List<string> TechStack { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime Graduation { get; set; }
        public DateTime StartLia1 { get; set; }
        public DateTime EndLia1 { get; set; }
        public DateTime StartLia2 { get; set; }
        public DateTime EndLia2 { get; set; }
        public string Presentation { get; set; }
        public string ImageUrl { get; set; }
        public List<string>? ConnectedTo { get; set; }
        public string LinkedInProfile { get; set; }
    }
}
