using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CollegeAdmissionPlatform.Models;

public partial class Student
{
    public int StdId { get; set; }

    [DisplayName("Full-Name")]
    [Required(ErrorMessage = "Full-Name field is required.")]
    public string Name { get; set; } = null!;

    [DisplayName("Parent's/Guardian Name")]
    [Required(ErrorMessage = "Parent's/Guardian Name field is required.")]
    public string Pname { get; set; } = null!;

    [DisplayName("Date of Birth")]
    [Required(ErrorMessage = "Date of Birth field is required.")]
    public DateOnly DateOfBirth { get; set; }

    [DisplayName("Email Id")]
    [Required(ErrorMessage = "Email Id field is required.")]
    [EmailAddress(ErrorMessage = "Try using a valid Email Id")]
    public string Email { get; set; } = null!;

    [DisplayName("Phone Number")]
    [Required(ErrorMessage = "Phone Number field is required.")]
    [RegularExpression(@"^[6-9]\d{9}$",
        ErrorMessage = "Try using a valid Phone number")]
    public string PhoneNumber { get; set; } = null!;

    [DisplayName("Full-Address")]
    [Required(ErrorMessage = "Full-Address field is required.")]
    public string Address { get; set; } = null!;

    [Required(ErrorMessage = "City field is required.")]
    public string City { get; set; } = null!;

    [Required(ErrorMessage = "District field is required.")]
    public int DistrictId { get; set; }

    [DisplayName("State")]
    [Required(ErrorMessage = "State field is required.")]
    public int StateId { get; set; }

    [Required(ErrorMessage = "Username field is required.")]
    public string Username { get; set; } = null!;

    [Required(ErrorMessage = "Password field is required.")]
    [DataType(DataType.Password)]
    [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$",
        ErrorMessage = "Password must be at least 8 characters long, " +
        "and include at least one uppercase letter, one lowercase letter, " +
        "one digit, and one special character.")]
    public string PasswordHash { get; set; } = null!;

    public DateTime RegistrationDate { get; set; }

    [DisplayName("Department")]
    [Required(ErrorMessage = "Department field is required")]
    public int DepartmentId { get; set; }

    [DisplayName("Level")]
    [Required(ErrorMessage = "Level field is required")]
    public int LevelId { get; set; }
    public int CourseId { get; set; }

    public virtual Course Course { get; set; } = null!;

    public virtual Department Department { get; set; } = null!;

    public virtual District District { get; set; } = null!;

    public virtual Level Level { get; set; } = null!;

    public virtual State State { get; set; } = null!;
}
