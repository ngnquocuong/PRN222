using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MVCWebApp_RazorPages.Models;
using MVCWebApp_RazorPages.Services;
namespace MVCWebApp_RazorPages.Pages.Courses
{
    public class EditModel : PageModel
    {
        private readonly CourseServices _courseServices;
        private readonly SubjectServices _subjectServices;

        public EditModel(CourseServices courseServices, SubjectServices subjectServices)
        {
            _courseServices = courseServices;
            _subjectServices = subjectServices;
        }

        [BindProperty]
        public Course Course { get; set; } = new Course(); // Khởi tạo mặc định
        public List<Subject> Subjects { get; set; } = new List<Subject>(); // Thêm thuộc tính Subjects
        public string? ErrorMessage { get; set; }

        public IActionResult OnGet(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            try
            {
                var course = _courseServices.GetCourse(id.Value);
                if (course == null)
                {
                    return NotFound();
                }
                Course = course;
                Subjects = _subjectServices.GetSubjects() ?? new List<Subject>(); // Đảm bảo Subjects không null
            }
            catch (Exception ex)
            {
                ErrorMessage = "Không thể tải thông tin khóa học. Vui lòng thử lại.";
                Subjects = new List<Subject>(); // Đảm bảo Subjects không null
            }
            return Page();
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                Subjects = _subjectServices.GetSubjects() ?? new List<Subject>(); // Tải lại Subjects
                return Page();
            }

            try
            {
                _courseServices.UpdateCourse(Course);
                return RedirectToPage("/Courses/Index");
            }
            catch (Exception ex)
            {
                ErrorMessage = "Không thể cập nhật khóa học. Vui lòng thử lại.";
                Subjects = _subjectServices.GetSubjects() ?? new List<Subject>(); // Tải lại Subjects
                return Page();
            }
        }
    }
}