using MVCWebApp_CRUD_session.Models;

namespace MVCWebApp_CRUD_session.Services
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
