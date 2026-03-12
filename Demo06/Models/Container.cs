using System;
using System.Collections.Generic;

namespace Demo06.Models;

public partial class Container
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
