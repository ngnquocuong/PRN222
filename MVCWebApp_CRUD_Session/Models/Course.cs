using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

//Thêm `using System.Text.Json.Serialization` để sử dụng `[JsonIgnore]`.
//-Áp dụng `[JsonIgnore]` cho `Subject` và `Students` để ngăn tuần tự hóa các navigation properties,
//ttránh lỗi vòng lặp hoặc dữ liệu dư thừa.
namespace MVCWebApp_CRUD_session.Models;

public partial class Course
{
    public int Id { get; set; }

    public string Title { get; set; } = null!;

    public DateOnly StartDate { get; set; }

    public DateOnly EndDate { get; set; }

    public int SubjectId { get; set; }

    [JsonIgnore]
    public virtual Subject Subject { get; set; } = null!;

    [JsonIgnore]
    public virtual ICollection<Student> Students { get; set; } = new List<Student>();
}