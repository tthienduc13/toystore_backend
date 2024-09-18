using System;
using System.Collections.Generic;

namespace Repositories.Entities;

public partial class Address
{
    public Guid Id { get; set; }

    public string? Phonenumber { get; set; }

    public string? City { get; set; }

    public string? District { get; set; }

    public string? Ward { get; set; }

    public DateTime? Createdat { get; set; }

    public Guid? Userid { get; set; }

    public virtual User? User { get; set; }
}
