using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MVCWebApp_RazorPages.Models;
using MVCWebApp_RazorPages.Services;

namespace MVCWebApp_RazorPages.Pages.Courses
{
    public class CreateModel : PageModel
    {
        private readonly CourseServices _courseServices;
        private readonly SubjectServices _subjectServices;

        public CreateModel(CourseServices courseServices, SubjectServices subjectServices)
        {
            _courseServices = courseServices;
            _subjectServices = subjectServices;
        }

        [BindProperty]
        public Course Course { get; set; } = new Course(); // Khởi tạo mặc định để tránh null
        public List<Subject> Subjects { get; set; } = new List<Subject>(); // Thêm thuộc tính Subjects, khởi tạo mặc định
        public string? ErrorMessage { get; set; } // Có thể null

        public IActionResult OnGet(int? subjectId)
        {
            try
            {
                Subjects = _subjectServices.GetSubjects() ?? new List<Subject>(); // Đảm bảo Subjects không null
                ViewData["CurSubjectId"] = subjectId; // Giữ trạng thái lọc nếu có
            }
            catch (Exception ex)
            {
                ErrorMessage = "Không thể tải danh sách môn học. Vui lòng thử lại.";
                Subjects = new List<Subject>(); // Đảm bảo Subjects không null
            }
            return Page();
        }

        public IActionResult OnPost(int? subjectId)
        {
            if (!ModelState.IsValid)
            {
                Subjects = _subjectServices.GetSubjects() ?? new List<Subject>(); // Tải lại Subjects nếu validation thất bại
                ViewData["CurSubjectId"] = subjectId;
                return Page();
            }

            try
            {
                _courseServices.AddCourse(Course);
                return RedirectToPage("/Courses/Index", new { subjectId });
            }
            catch (Exception ex)
            {
                ErrorMessage = "Không thể thêm khóa học. Vui lòng thử lại.";
                Subjects = _subjectServices.GetSubjects() ?? new List<Subject>(); // Tải lại Subjects
                ViewData["CurSubjectId"] = subjectId;
                return Page();
            }
        }
    }
}