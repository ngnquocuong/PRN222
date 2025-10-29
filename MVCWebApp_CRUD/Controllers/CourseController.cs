using Microsoft.AspNetCore.Mvc;
using MVCWebApp_CRUD.Models;
using MVCWebApp_CRUD.Services;

namespace MVCWebApp_CRUD.Controllers
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
            ViewData["CurSubjectId"] = id;
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

        public IActionResult Add(int? id)
        {
            //lay du lieu List of Subject
            var Subjects = _subjectServices.GetSubjects();
            ViewData["Subjects"] = Subjects;
            ViewData["CurSubjectId"] = id;
            return View();
        }

        [HttpPost]
        // Phương thức DoAdd: Xử lý hành động thêm một khóa học mới
// Tham số newCourse: Đối tượng Course chứa thông tin khóa học mới do người dùng gửi lên (thường từ form)
public IActionResult DoAdd(Course newCourse)
        {
            // Gọi phương thức AddCourse từ _courseServices để thêm khóa học mới vào database
            // AddCourse sẽ thực hiện INSERT dữ liệu vào bảng Courses
            _courseServices.AddCourse(newCourse);

            // Chuyển hướng người dùng về action "List" (danh sách khóa học)
            // new {id = newCourse.SubjectId} truyền tham số id (SubjectId) để hiển thị danh sách khóa học theo môn học
            // RedirectToAction tạo một HTTP redirect  đến URL tương ứng
            return RedirectToAction("List", new { id = newCourse.SubjectId });
        }

        // Phương thức Edit: Hiển thị form chỉnh sửa thông tin khóa học
        // Tham số id: Id của khóa học cần chỉnh sửa (nullable int, vì id có thể không được cung cấp)
        public IActionResult Edit(int? id)
        {
            // Lấy danh sách tất cả môn học (subjects) từ _subjectServices
            // GetSubjects trả về danh sách môn học để hiển thị trong dropdown (chọn môn học cho khóa học)
            var Subjects = _subjectServices.GetSubjects();

            // Lưu danh sách môn học vào ViewData với key "Subjects"
            // ViewData được sử dụng để truyền dữ liệu từ controller đến view
            ViewData["Subjects"] = Subjects;

            // Lấy thông tin khóa học từ _courseServices dựa trên id
            // GetCourse trả về đối tượng Course hoặc null nếu không tìm thấy
            var Course = _courseServices.GetCourse(id);

            // Kiểm tra nếu khóa học không tồn tại (Course == null)
            if (Course == null)
            {
                // Lưu thông báo lỗi vào ViewData với key "ErrMess"
                // Thông báo này sẽ được hiển thị trên view nếu không tìm thấy khóa học
                ViewData["ErrMess"] = "Khong ton tai course theo yeu cau";

                // Trả về view (Edit.cshtml) mà không truyền model (Course)
                // View sẽ hiển thị thông báo lỗi
                return View();
            }
            // Nếu khóa học tồn tại
            else
            {
                // Trả về view (Edit.cshtml) và truyền đối tượng Course làm model
                // View sẽ hiển thị form chỉnh sửa với thông tin của khóa học
                return View(Course);
            }
        }
        //public IActionResult Edit(int? id)
        //{
        //    var Subjects = _subjectServices.GetSubjects();
        //    ViewData["Subjects"] = Subjects;
        //    var Course = _courseServices.GetCourse(id);
        //    if (Course == null)
        //    {
        //        ViewData["ErrMess"] = "Khong ton tai course theo yeu cau";
        //        return View();
        //    }
        //    else 
        //        return View(Course);
        //}

        public IActionResult DoEdit(Course newCourse)
        {
            //nhan vao thong tin cua Course
            //mang thong tin nay insert vao database
            _courseServices.UpdateCourse(newCourse);
            //redirect tro lai trang list.
            return RedirectToAction("List", new { id = newCourse.SubjectId });
        }

        public IActionResult DoDelete(int? id, int? sid)
        {
            if (id != null)
                _courseServices.DeleteCourse((int)id);
            return RedirectToAction("List", new {id = sid});
        }
    }
}
