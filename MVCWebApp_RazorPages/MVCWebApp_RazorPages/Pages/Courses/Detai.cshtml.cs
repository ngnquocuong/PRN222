using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MVCWebApp_RazorPages.Models;
using MVCWebApp_RazorPages.Services;

namespace MVCWebApp_RazorPages.Pages.Courses
{    public class DetailModel : PageModel
    {
        private readonly CourseServices _courseServices;

        public DetailModel(CourseServices courseServices)
        {
            _courseServices = courseServices;
        }

        public Course Course { get; set; }

        public IActionResult OnGet(int id)
        {
            Course = _courseServices.GetCourse(id);
            if (Course == null)
            {
                return Page();
            }
            return Page();
        }
    }
}