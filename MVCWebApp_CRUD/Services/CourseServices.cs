using Microsoft.EntityFrameworkCore;
using MVCWebApp_CRUD.Models;

namespace MVCWebApp_CRUD.Services
{
    public class CourseServices: BaseService
    {
        public CourseServices(SchoolDbContext context) : base(context)
        {
        }

       // Phương thức GetCourses: Lấy danh sách các khóa học từ database
    // Tham số SubjectId (nullable int) dùng để lọc các khóa học theo môn học nếu có
    public List<Course> GetCourses(int? SubjectId)
        {
            // Kiểm tra nếu SubjectId là null (nghĩa là không có lọc theo môn học)
            if (SubjectId == null)
                // Truy vấn tất cả các khóa học từ bảng Courses trong database
                // Include(c => c.Subject): Bao gồm thông tin môn học liên quan (navigation property)
                // ToList(): Chuyển kết quả truy vấn thành danh sách và trả về
                return _context
                    .Courses
                    .Include(c => c.Subject)
                    .ToList();
            // Nếu SubjectId không null (có lọc theo môn học)
            else
                // Truy vấn các khóa học có SubjectId khớp với giá trị truyền vào
                // Include(c => c.Subject): Bao gồm thông tin môn học liên quan
                // Where(c => c.SubjectId == SubjectId): Lọc các khóa học theo SubjectId
                // ToList(): Chuyển kết quả thành danh sách và trả về
                return _context
                    .Courses
                    .Include(c => c.Subject)
                    .Where(c => c.SubjectId == SubjectId)
                    .ToList();
        }

        // Phương thức GetCourse: Lấy thông tin chi tiết của một khóa học dựa trên CourseId
        // Tham số CourseId (nullable int) là Id của khóa học cần lấy
        // Trả về một đối tượng Course hoặc null nếu không tìm thấy
        public Course? GetCourse(int? CourseId)
        {
            // Truy vấn khóa học từ bảng Courses
            // Include(c => c.Subject): Bao gồm thông tin môn học liên quan
            // Include(c => c.Students): Bao gồm danh sách sinh viên tham gia khóa học (navigation property)
            // FirstOrDefault(c => c.Id == CourseId): Lấy khóa học đầu tiên có Id khớp với CourseId, hoặc null nếu không tìm thấy
            return _context.Courses
                .Include(c => c.Subject)
                .Include(c => c.Students)
                .FirstOrDefault(c => c.Id == CourseId);
        }

        // Phương thức UpdateCourse: Cập nhật thông tin một khóa học
        // Tham số course là đối tượng Course chứa thông tin mới cần cập nhật
        public void UpdateCourse(Course course)
        {
            // Tìm khóa học hiện có trong database dựa trên Id của khóa học truyền vào
            // FirstOrDefault trả về khóa học đầu tiên khớp với Id, hoặc null nếu không tìm thấy
            var old = _context.Courses.FirstOrDefault(c => c.Id == course.Id);
            // Kiểm tra nếu khóa học tồn tại
            if (old != null)
            {
                // Cập nhật các thuộc tính của khóa học hiện có bằng giá trị từ đối tượng course
                old.Title = course.Title; // Cập nhật tiêu đề khóa học
                old.StartDate = course.StartDate; // Cập nhật ngày bắt đầu
                old.EndDate = course.EndDate; // Cập nhật ngày kết thúc
                old.SubjectId = course.SubjectId; // Cập nhật Id của môn học liên quan

                // Dòng code bị comment: _context.Courses.Update(course);
                // Phương thức Update của EF Core có thể được dùng để cập nhật toàn bộ đối tượng
                // Tuy nhiên, ở đây tác giả chọn cách cập nhật từng thuộc tính thủ công

                // Lưu các thay đổi vào database
                // SaveChanges() sẽ thực thi câu lệnh SQL UPDATE để cập nhật dữ liệu
                _context.SaveChanges();
            }
            // Nếu không tìm thấy khóa học (old == null), phương thức không làm gì
        }

        // Phương thức DeleteCourse: Xóa một khóa học dựa trên CourseId
        // Tham số CourseId là Id của khóa học cần xóa
        public void DeleteCourse(int CourseId)
        {
            // Tìm khóa học trong database dựa trên CourseId
            // Include(c => c.Students): Bao gồm danh sách sinh viên liên quan để xử lý quan hệ
            // FirstOrDefault trả về khóa học đầu tiên khớp với Id, hoặc null nếu không tìm thấy
            var course = _context.Courses
                .Include(c => c.Students)
                .FirstOrDefault(c => c.Id == CourseId);
            // Kiểm tra nếu khóa học tồn tại
            if (course != null)
            {
                // Xóa tất cả sinh viên liên quan đến khóa học này
                // Students là một navigation property (danh sách sinh viên tham gia khóa học)
                // Clear() xóa tất cả quan hệ giữa khóa học và sinh viên
                // Điều này cần thiết để tránh lỗi vi phạm ràng buộc khóa ngoại khi xóa
                course.Students.Clear();

                // Xóa khóa học khỏi database
                // Remove(course) đánh dấu khóa học để xóa (DELETE trong SQL)
                _context.Remove(course);

                // Lưu các thay đổi vào database
                // SaveChanges() thực thi câu lệnh SQL DELETE để xóa khóa học
                _context.SaveChanges();
            }
            // Nếu không tìm thấy khóa học, phương thức không làm gì
        }

        // Phương thức AddCourse: Thêm một khóa học mới vào database
        // Tham số course là đối tượng Course chứa thông tin khóa học mới
        public void AddCourse(Course course)
        {
            // Thêm đối tượng course vào bảng Courses trong database
            // Add(course) đánh dấu đối tượng để thêm (INSERT trong SQL)
            _context.Courses.Add(course);

            // Lưu các thay đổi vào database
            // SaveChanges() thực thi câu lệnh SQL INSERT để thêm khóa học
            _context.SaveChanges();
        }
}

}
