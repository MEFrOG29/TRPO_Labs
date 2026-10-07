using System;
using System.Collections.Generic;

namespace lab1.Models;

public partial class Assignment
{
    public int Id { get; set; }

    public int TopicsId { get; set; }

    public string Title { get; set; } = null!;

    public string Description { get; set; } = null!;

    public int? MaxPoints { get; set; }

    public string? Deadline { get; set; }

    public string? CreatedAt { get; set; }

    public virtual Topic IdNavigation { get; set; } = null!;
}
