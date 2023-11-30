namespace Api.Dtos
{
    public class StudentDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string MailAddress { get; set; }
        public List<string> TechStack { get; set; }
        public string PhoneNumber { get; set; }
        public LiaPeriodDto Liaison1 { get; set; }
        public LiaPeriodDto Liaison2 { get; set; }
        public string Presentation { get; set; }
        public string ImageUrl { get; set; }
    }
    public class LiaPeriodDto
    {
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
    }
}
