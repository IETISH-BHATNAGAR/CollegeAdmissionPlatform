using System;
using System.Collections.Generic;

namespace CollegeAdmissionPlatform.Models;

public partial class State
{
    public int StateId { get; set; }

    public string StateName { get; set; } = null!;

    public DateTime CreatedDate { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public virtual ICollection<District> Districts { get; set; } = new List<District>();

    public virtual ICollection<Student> Students { get; set; } = new List<Student>();
}
