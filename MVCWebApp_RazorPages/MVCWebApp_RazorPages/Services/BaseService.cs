using MVCWebApp_RazorPages.Models;

namespace MVCWebApp_RazorPages.Services
{
    public class BaseService
    {
        protected SchoolDbContext _context;
        public BaseService(SchoolDbContext context) { 
            _context = context;
        }
    }
}
