using Microsoft.EntityFrameworkCore;
using MVCWebApp_View.Models;

namespace MVCWebApp_View.Services
{
    public class CourseServices: BaseService
    {
        public CourseServices(SchoolDbContext context) : base(context)
        {
        }

        public List<Course> GetCourses(int? SubjectId)
        {
            if (SubjectId == null)
                return _context
                    .Courses
                    .Include(c => c.Subject)
                    .ToList();
            else return _context
                    .Courses
                    .Include(c => c.Subject)
                    .Where(c => c.SubjectId == SubjectId)
                    .ToList(); 
        }

        public Course? GetCourse(int CourseId)
        {
            return _context.Courses
                .Include(c => c.Subject)
                .Include(c => c.Students)
                .FirstOrDefault(c => c.Id == CourseId);
        }
    }
}
