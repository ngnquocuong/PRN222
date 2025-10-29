using System;
using System.Collections.Generic;

namespace MVCWebApp_View.Models;

public partial class Course
{
    public int Id { get; set; }

    public string Title { get; set; } = null!;

    public DateOnly StartDate { get; set; }

    public DateOnly EndDate { get; set; }

    public int SubjectId { get; set; }

    public virtual Subject Subject { get; set; } = null!;

    public virtual ICollection<Student> Students { get; set; } = new List<Student>();
}
