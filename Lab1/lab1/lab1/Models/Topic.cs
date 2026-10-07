using System;
using System.Collections.Generic;

namespace lab1.Models;

public partial class Topic
{
    public int Id { get; set; }

    public string Title { get; set; } = null!;

    public string? Description { get; set; }

    public string? CreatedAt { get; set; }

    public virtual Assignment? Assignment { get; set; }

    public virtual LectureMaterial? LectureMaterial { get; set; }

    public virtual Test? Test { get; set; }
}
