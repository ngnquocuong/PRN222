using MVCWebApp_View.Models;

namespace MVCWebApp_View.Services
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
