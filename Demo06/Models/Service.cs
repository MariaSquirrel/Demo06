using System;
using System.Collections.Generic;

namespace Demo06.Models;

public partial class Service
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public decimal? Cost { get; set; }

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
