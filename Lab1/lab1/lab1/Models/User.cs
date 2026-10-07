using System;
using System.Collections.Generic;

namespace lab1.Models;

public partial class User
{
    public int Id { get; set; }

    public int RoleId { get; set; }

    public string Email { get; set; } = null!;

    public string PasswordHash { get; set; } = null!;

    public string FirstName { get; set; } = null!;

    public string? LastName { get; set; }

    public string? CreatedAt { get; set; }

    public virtual Role IdNavigation { get; set; } = null!;
}
