using Microsoft.EntityFrameworkCore; // Nhập namespace để sử dụng Entity Framework Core
using MVCWebApp_RazorPages.Models; // Nhập namespace để sử dụng SchoolDbContext và các model
using MVCWebApp_RazorPages.Services; // Nhập namespace để sử dụng các service như SubjectServices, CourseServices

// Tạo builder để cấu hình ứng dụng và các dịch vụ (Dependency Injection - DI)
var builder = WebApplication.CreateBuilder(args);

// Cấu hình các dịch vụ (services) mà ứng dụng sẽ sử dụng
// Thêm hỗ trợ cho Razor Pages (thay vì MVC)
//builder.Services.AddRazorPages(); // Đăng ký dịch vụ Razor Pages để sử dụng mô hình Razor Pages

// Cấu hình kết nối cơ sở dữ liệu bằng SchoolDbContext
// Sử dụng chuỗi kết nối "MyCon" từ file appsettings.json
builder.Services.AddRazorPages(options =>
{
    // Thiết lập route mặc định: URL gốc "/" sẽ ánh xạ đến "/Courses/Index"
    options.Conventions.AddPageRoute("/Courses/Index", "");
});
builder.Services.AddDbContext<SchoolDbContext>(
    opt => opt.UseSqlServer(builder.Configuration.GetConnectionString("MyCon")) // Sử dụng SQL Server làm provider cho EF Core
);
builder.Services.AddScoped<SubjectServices>();
builder.Services.AddScoped<CourseServices>();

// Tạo ứng dụng dựa trên các dịch vụ đã đăng ký ở trên
var app = builder.Build();


app.MapRazorPages();

// Khởi động ứng dụng và bắt đầu lắng nghe các HTTP request
app.Run();