using System;
using System.Collections.Generic;

namespace CollegeAdmissionPlatform.Models;

public partial class District
{
    public int DistrictId { get; set; }

    public string DistrictName { get; set; } = null!;

    public int StateId { get; set; }

    public virtual State State { get; set; } = null!;

    public virtual ICollection<Student> Students { get; set; } = new List<Student>();
}
