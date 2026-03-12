using System;
using System.Collections.Generic;

namespace Demo06.Models;

public partial class Order
{
    public int Id { get; set; }

    public DateOnly? Datecreate { get; set; }

    public int? ContainerId { get; set; }

    public int? MaterialId { get; set; }

    public int? UserId { get; set; }

    public virtual Container? Container { get; set; }

    public virtual Material? Material { get; set; }

    public virtual User? User { get; set; }

    public virtual ICollection<Client> Clients { get; set; } = new List<Client>();

    public virtual ICollection<Service> Services { get; set; } = new List<Service>();
}
