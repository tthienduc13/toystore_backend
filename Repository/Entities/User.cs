using System;
using System.Collections.Generic;

namespace Repositories.Entities;

public partial class User
{
    public Guid Id { get; set; }

    public string? Fullname { get; set; }

    public string? Password { get; set; }

    public string? Email { get; set; }

    public string? Username { get; set; }

    public string? Img { get; set; }

    public DateTime? Createdat { get; set; }

    public DateTime? Updatedat { get; set; }

    public bool? Isdelete { get; set; }

    public string? Address { get; set; }

    public int? Role { get; set; }

    public virtual ICollection<Address> Addresses { get; set; } = new List<Address>();

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
