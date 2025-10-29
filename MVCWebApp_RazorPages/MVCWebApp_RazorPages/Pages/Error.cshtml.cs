using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MVCWebApp_RazorPages.Pages
{
    public class ErrorModel : PageModel
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public string RequestedUrl { get; set; }

        public void OnGet(int? statusCode = null)
        {
            StatusCode = statusCode ?? 500;
            RequestedUrl = Request.Path + Request.QueryString;
            Message = StatusCode switch
            {
                404 => "Page not found.",
                500 => "Internal server error.",
                _ => "An unexpected error occurred."
            };
        }
    }
}