using System;
using System.Collections.Generic;

namespace Repositories.Entities;

public partial class Category
{
    public Guid Id { get; set; }

    public string? Categoryname { get; set; }

    public DateTime? Createdat { get; set; }

    public bool? Isdelete { get; set; }

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
