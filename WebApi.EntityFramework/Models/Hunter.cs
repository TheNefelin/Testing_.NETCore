using System;
using System.Collections.Generic;

namespace WebApi.EntityFramework.Models;

public partial class Hunter
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public int Age { get; set; }

    public virtual ICollection<Nen> Nens { get; set; } = new List<Nen>();
}
