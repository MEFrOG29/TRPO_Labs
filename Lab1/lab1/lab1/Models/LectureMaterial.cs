using System;
using System.Collections.Generic;

namespace lab1.Models;

public partial class LectureMaterial
{
    public int Id { get; set; }

    public int TopicsId { get; set; }

    public string Title { get; set; } = null!;

    public string MaterialType { get; set; } = null!;

    public string? Content { get; set; }

    public string? FileUrl { get; set; }

    public string? CreatedAt { get; set; }

    public virtual Topic IdNavigation { get; set; } = null!;
}
