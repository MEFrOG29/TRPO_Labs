using System;
using System.Collections.Generic;

namespace lab1.Models;

public partial class Role
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Description { get; set; } = null!;

    public virtual User? User { get; set; }
}
