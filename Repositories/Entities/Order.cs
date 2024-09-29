using System;
using System.Collections.Generic;

namespace Repositories.Entities;

public partial class Order
{
    public Guid Id { get; set; }

    public Guid? Userid { get; set; }

    public double? Totalprice { get; set; }

    public DateTime? Createdat { get; set; }

    public string? Address { get; set; }

    public virtual ICollection<Orderdetail> Orderdetails { get; set; } = new List<Orderdetail>();

    public virtual User? User { get; set; }
}
