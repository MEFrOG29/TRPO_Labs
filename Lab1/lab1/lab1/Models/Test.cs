using System;
using System.Collections.Generic;

namespace lab1.Models;

public partial class Test
{
    public int Id { get; set; }

    public int TopicId { get; set; }

    public string Title { get; set; } = null!;

    public string? Description { get; set; }

    public int? LimitMinutes { get; set; }

    public string? CreatedAt { get; set; }

    public virtual Topic IdNavigation { get; set; } = null!;

    public virtual TestQuestion? TestQuestion { get; set; }
}
