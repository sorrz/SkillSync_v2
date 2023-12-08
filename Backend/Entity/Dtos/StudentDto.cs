namespace Entity.Dtos
{
    public class StudentDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string MailAddress { get; set; }
        public string PasswordHash {  get; set; }
        public List<string> TechStack { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime Graduation { get; set; }
        public DateTime Lia1Start { get; set; }
        public DateTime Lia1End { get; set; }
        public DateTime Lia2Start { get; set; }
        public DateTime Lia2End { get; set; }
        public string Presentation { get; set; }
        public string ImageUrl { get; set; }
        public List<string> ConnectedTo { get; set; }
        public string LinkedInProfile {  get; set; }

    }
    
}
