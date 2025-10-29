using MVCWebApp_CRUD_session.Models;

namespace MVCWebApp_CRUD_session.Services
{
    public class BaseService
    {
        protected SchoolDbContext _context;
        public BaseService(SchoolDbContext context) { 
            _context = context;
        }
    }
}
