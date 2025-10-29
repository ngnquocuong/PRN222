using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MVCWebApp_RazorPages.Models;
using MVCWebApp_RazorPages.Services;
using System.Collections.Generic;
namespace MVCWebApp_RazorPages.Pages.Courses
{
    public class IndexModel : PageModel
    {
        private readonly SubjectServices _subjectServices;
        private readonly CourseServices _courseServices;
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(SubjectServices subjectServices, CourseServices courseServices, ILogger<IndexModel> logger)
        {
            _subjectServices = subjectServices;
            _courseServices = courseServices;
            _logger = logger;
        }

        public List<Course> Courses { get; set; } = new List<Course>(); // Khởi tạo mặc định để tránh null
        public List<Subject> Subjects { get; set; } = new List<Subject>(); // Khởi tạo mặc định để tránh null
        public int? CurSubjectId { get; set; }
        public string? ErrorMessage { get; set; } // Sử dụng string? để rõ ràng về khả năng null

        public IActionResult OnGet(int? subjectId)
        {
            _logger.LogInformation("OnGet called for /Courses/Index with subjectId: {SubjectId}", subjectId);
            try
            {
                Subjects = _subjectServices.GetSubjects() ?? new List<Subject>(); // Đảm bảo Subjects không null
                ViewData["CurSubjectId"] = subjectId;
                Courses = _courseServices.GetCourses(subjectId) ?? new List<Course>(); // Đảm bảo Courses không null
                _logger.LogInformation("Successfully loaded {CourseCount} courses", Courses.Count);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error loading courses for /Courses/Index");
                ErrorMessage = "Không thể tải danh sách khóa học. Vui lòng kiểm tra kết nối cơ sở dữ liệu.";
                Courses = new List<Course>(); // Đảm bảo Courses không null
                Subjects = new List<Subject>(); // Đảm bảo Subjects không null
            }
            return Page();
        }

        public IActionResult OnPostDelete(int? id, int? sid)
        {
            _logger.LogInformation("OnPostDelete called with id: {Id}, sid: {Sid}", id, sid);
            try
            {
                if (id != null)
                {
                    _courseServices.DeleteCourse((int)id);
                    _logger.LogInformation("Successfully deleted course with id: {Id}", id);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting course with id: {Id}", id);
                ErrorMessage = "Không thể xóa khóa học. Vui lòng thử lại.";
            }
            return RedirectToPage("/Courses/Index", new { subjectId = sid });
        }
    }
}
