using System;
using System.Collections.Generic;

namespace Repositories.Entities;

public partial class Product
{
    public Guid Id { get; set; }

    public string? Name { get; set; }

    public string? Description { get; set; }

    public string? Img { get; set; }

    public double? Price { get; set; }

    public string? Brand { get; set; }

    public Guid? Categoryid { get; set; }

    public Guid? Createdby { get; set; }

    public DateTime? Createdat { get; set; }

    public bool? Isdeleted { get; set; }

    public virtual Category? Category { get; set; }

    public virtual User? CreatedbyNavigation { get; set; }

    public virtual ICollection<Orderdetail> Orderdetails { get; set; } = new List<Orderdetail>();
}
