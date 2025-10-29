using MVCWebApp_CRUD.Models;

namespace MVCWebApp_CRUD.Services
{
    public class BaseService
    {
        protected SchoolDbContext _context;
        public BaseService(SchoolDbContext context) { 
            _context = context;
        }
    }
}
