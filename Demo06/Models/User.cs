using System;
using System.Collections.Generic;

namespace Demo06.Models;

public partial class User
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? Family { get; set; }

    public string? Patronomic { get; set; }

    public string? Login { get; set; }

    public string? Password { get; set; }

    public int? RoleId { get; set; }

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

    public virtual Role? Role { get; set; }
}
