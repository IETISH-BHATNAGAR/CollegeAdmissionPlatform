using CollegeAdmissionPlatform.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CollegeAdmissionPlatform.Controllers
{
    public class LoginController : Controller
    {
        private readonly CollegeAdmissionPlatformContext context;

        public LoginController(CollegeAdmissionPlatformContext context)
        {
            this.context = context;
        }
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(Student model)
        {
            var Password = model.PasswordHash;
            var Student = await context.Students
                          .Include(s => s.State)
                          .Include(s => s.District)
                          .Include(s => s.Department)
                          .Include(s => s.Level)
                          .Include(s => s.Course)
                          .Where(s => s.Username == model.Username && s.PasswordHash == Password)
                          .FirstOrDefaultAsync();
            if (Student != null)
            {
                HttpContext.Session.SetString("Name", Student.Name);
                HttpContext.Session.SetInt32("StudentId", Student.StdId);
                return RedirectToAction("Dashboard");
            }
            else
            {
                ViewData["Error"] = "Invalid Username or Password!. Please try again.";
                return View(model);
            }
        }

        public async Task<IActionResult> Dashboard()
        {
            var studentId = HttpContext.Session.GetInt32("StudentId");
            if (studentId == null)
            {
                ViewBag.Error = "Student ID: " + studentId + "Not Found";
                return RedirectToAction("Login");
            }
            var Student = await context.Students
                          .Include(s => s.State)
                          .Include(s => s.District)
                          .Include(s => s.Department)
                          .Include(s => s.Level)
                          .Include(s => s.Course)
                          .FirstOrDefaultAsync(s => s.StdId == studentId);
            if (Student == null) 
            {
                ViewBag.Error = "Student Not Found";
                return RedirectToAction("Login");
            }
            var studentViewModel = new StudentViewModel
            {
                StudentId = Student.StdId,
                FullName = Student.Name,
                ParentName = Student.Pname,
                Dob = Student.DateOfBirth,
                EmailId = Student.Email,
                Phone = Student.PhoneNumber,
                FullAddress = Student.Address,
                City = Student.City,
                StateName = Student.State.StateName,
                DistrictName = Student.District.DistrictName,
                DepartmentName = Student.Department.DepartmentName,
                LevelName = Student.Level.LevelName,
                CourseName = Student.Course.CourseName,
                Username = Student.Username
            };
            return View(studentViewModel);
        }
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }
    }
}
