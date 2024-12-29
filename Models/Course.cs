namespace CollegeAdmissionPlatform.Models;

public partial class Course
{
    public int CourseId { get; set; }

    public string CourseName { get; set; } = null!;

    public int DepartmentId { get; set; }

    public int LevelId { get; set; }

    public DateTime CreatedDate { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public virtual Department Department { get; set; } = null!;

    public virtual Level Level { get; set; } = null!;

    public virtual ICollection<Student> Students { get; set; } = new List<Student>();
}
