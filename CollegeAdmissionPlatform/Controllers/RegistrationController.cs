using CollegeAdmissionPlatform.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace CollegeAdmissionPlatform.Controllers
{
    public class RegistrationController : Controller
    {
        private readonly CollegeAdmissionPlatformContext context;

        public RegistrationController(CollegeAdmissionPlatformContext context)
        {
            this.context = context;
        }
        public IActionResult Register()
        {
            ViewBag.Departments = new SelectList(context.Departments, "DepartmentId", "DepartmentName");
            ViewBag.Levels = new SelectList(context.Levels, "LevelId", "LevelName");
            ViewBag.States = new SelectList(context.States, "StateId", "StateName");
            return View();
        }
        public async Task<IActionResult> GetDistricts(int stateId)
        {
            var districts = await context.Districts
                            .Where(d => d.StateId == stateId)
                            .Select(d => new { d.DistrictId, d.DistrictName })
                            .ToListAsync();
            return Json(districts);
        }
        
        public async Task<IActionResult> GetCourses(int departmentId, int levelId)
        {
            var courses = await context.Courses
                         .Where(c => c.DepartmentId == departmentId && c.LevelId == levelId)
                         .Select(c => new { c.CourseId, c.CourseName })
                         .ToListAsync();
            return Json(courses);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(Student student)
        {
            var UsernameExist = await context.Students.AnyAsync(s => s.Username == student.Username);
            if (UsernameExist)
            {
                ModelState.AddModelError("Username", "The username is already taken. Please choose another one.");
                PopulateViewData(student);      
                return View(student);
            }
            else
            {

                student.RegistrationDate = DateTime.Now;

                context.Add(student);
                await context.SaveChangesAsync();
                TempData["Message"] = "The user has been registered successfully!";

                // Clear form state
                ModelState.Clear();

                PopulateViewData(student);
                return RedirectToAction(nameof(Register));
            }

        }

        private void PopulateViewData(Student student = null)
        {
            ViewBag.Departments = new SelectList(context.Departments, "DepartmentId", "DepartmentName", student.DepartmentId);
            ViewBag.States = new SelectList(context.States, "StateId", "StateName", student.StateId);
            ViewBag.Levels = new SelectList(context.Levels, "LevelId", "LevelName");
            if (student != null)
            {
                ViewBag.Districts = new SelectList(context.Districts.Where(d => d.StateId == student.StateId), "DistrictId", "DistrictName", student.DistrictId);
                ViewBag.Courses = new SelectList(context.Courses.Where(c => c.DepartmentId == student.DepartmentId && c.LevelId == student.LevelId), "CourseId", "CourseName", student.CourseId);
            }
            else
            {
                ViewBag.Districts = new SelectList(Enumerable.Empty<SelectListItem>(), "DistrictId", "DistrictName");
                ViewBag.Courses = new SelectList(Enumerable.Empty<SelectListItem>(), "CourseId", "CourseName");
            }
        }
    }
    
}
