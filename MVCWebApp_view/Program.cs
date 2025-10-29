using Microsoft.EntityFrameworkCore;
using MVCWebApp_View.Models;
using MVCWebApp_View.Services;


//config xem project dung nhung service nao (config DI)
var builder = WebApplication.CreateBuilder(args);

//config services WebApp muon su dung.
builder.Services.AddControllersWithViews();//su dung mo hinh MVC.
//builder.Services.AddRazorPages();//su dung mo hinh RazorPages
//builder.Services.AddServerSideBlazor();//su dung mo hinh Blazor
//builder.Services.AddControllers();//viet ung dung API

//config service ket noi DB dung SchoolDbContet voi constr doc trong appsettings.json, the "MyCon"
builder.Services.AddDbContext<SchoolDbContext>(
    opt => opt.UseSqlServer(builder.Configuration.GetConnectionString("MyCon"))
    );

builder.Services.AddScoped<SubjectServices>();
builder.Services.AddScoped<CourseServices>();

//tao 1 application theo cac service da dang ky o tren
var app = builder.Build();

//config middleware pipeline

//Dinh nghia cac pattern routing - co che chung cua toan bo project
app.MapControllerRoute(
    name: "pattern1",
    pattern: "/{controller=Course}/{action=List}/{id?}"//id chinh la key cua routedata
    );

//run application da tao o tren
app.Run();
