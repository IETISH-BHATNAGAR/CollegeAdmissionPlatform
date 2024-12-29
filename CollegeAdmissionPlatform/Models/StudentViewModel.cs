namespace CollegeAdmissionPlatform.Models
{
    public class StudentViewModel
    {
        public int StudentId { get; set; }
        public string FullName { get; set; }
        public string ParentName { get; set; }
        public DateOnly Dob { get; set; }
        public string EmailId { get; set; }
        public string Phone { get; set; }
        public string FullAddress { get; set; }
        public string City { get; set; }
        public string StateName { get; set; }
        public string DistrictName { get; set; }
        public string DepartmentName { get; set; }
        public string LevelName { get; set; }
        public string CourseName { get; set; }
        public string Username { get; set; }
    }
}
