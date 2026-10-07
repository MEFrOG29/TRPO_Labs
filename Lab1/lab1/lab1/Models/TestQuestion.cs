using System;
using System.Collections.Generic;

namespace lab1.Models;

public partial class TestQuestion
{
    public int Id { get; set; }

    public int TestId { get; set; }

    public string QuestionText { get; set; } = null!;

    public string? QuestionType { get; set; }

    public string AnswersJson { get; set; } = null!;

    public int? Points { get; set; }

    public virtual Test IdNavigation { get; set; } = null!;
}
