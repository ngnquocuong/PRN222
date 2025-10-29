using MVCWebApp_View.Models;

namespace MVCWebApp_View.Services
{
    public class BaseService
    {
        protected SchoolDbContext _context;
        public BaseService(SchoolDbContext context) { 
            _context = context;
        }
    }
}
