using System;
using System.Collections.Generic;

namespace MVCWebApp_CRUD.Models;

public partial class Subject
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public int NumberOfSlot { get; set; }

    public virtual ICollection<Course> Courses { get; set; } = new List<Course>();
}
