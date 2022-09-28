namespace GoCode.Application.Common.Dtos
{
    public class CourseInfoDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int XP { get; set; }
        public string Description { get; set; }
        public int PassPercentTreshold { get; set; }
        public bool IsUserSignedUp { get; set; }
    }
}
