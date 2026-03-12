using System;
using System.Collections.Generic;

namespace Demo06.Models;

public partial class Client
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? Family { get; set; }

    public string? Patronomic { get; set; }

    public string? Email { get; set; }

    public string? Phone { get; set; }

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
