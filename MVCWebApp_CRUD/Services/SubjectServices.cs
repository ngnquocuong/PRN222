using MVCWebApp_CRUD.Models;

namespace MVCWebApp_CRUD.Services
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
