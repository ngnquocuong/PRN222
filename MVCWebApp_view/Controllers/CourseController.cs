using Microsoft.AspNetCore.Mvc;
using MVCWebApp_View.Services;

namespace MVCWebApp_View.Controllers
{
    public class CourseController : Controller
    {
        SubjectServices _subjectServices;
        CourseServices _courseServices;

        public CourseController(SubjectServices subjectServices, CourseServices courseServices)
        {
            _subjectServices = subjectServices;
            _courseServices = courseServices;
        }

        public void Index()
        {
            //chi lam 1 logic nao do, ko can tra ve giao dien
        }

        public IActionResult List(int? id)
            //id la SubjectId. truong hop id null -> lay all course cua all subject
        {
            //logic nao do o day
            //lay danh sach tat ca Subject
            var Subjects = _subjectServices.GetSubjects();//truyen xuong view bang ViewData
            ViewData["Subjects"] = Subjects;
            //ViewBag.Subjects = Subjects;
            var Courses = _courseServices.GetCourses(id); //truyen xuong view bang Model
            //lay danh sach cac course cua subject duoc chon

            return View(Courses);//Model = Courses
            //html = output tra ve cua ham View()
            //chinh la: load file list.cshtml -> html = <div>Xau ky tu nao do </div>
            //return html
        }

        public IActionResult Detail(int id)
            //id la CourseId. Truong hop id=0 -> thong bao ko tim thay course co Courseid=0
        {
            //lay toan bo thong tin cua Course
            var Course = _courseServices.GetCourse(id);//truyen xuong view bang Model

            return View(Course);//Model = Course
        }
    }
}
