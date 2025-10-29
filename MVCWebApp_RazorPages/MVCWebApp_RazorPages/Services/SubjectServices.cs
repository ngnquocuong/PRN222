using MVCWebApp_RazorPages.Models;

namespace MVCWebApp_RazorPages.Services
{
    public class SubjectServices:BaseService
    {
        public SubjectServices(SchoolDbContext context): base(context) 
        {
        }

        public List<Subject> GetSubjects()
        {
            return _context.Subjects.ToList();
        }
    }
}
