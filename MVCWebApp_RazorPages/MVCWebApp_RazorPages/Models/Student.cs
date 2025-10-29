using System;
using System.Collections.Generic;

namespace MVCWebApp_RazorPages.Models;

public partial class Student
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public DateOnly Dob { get; set; }

    public virtual ICollection<Course> Courses { get; set; } = new List<Course>();
}
