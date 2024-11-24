using System;
using System.Collections.Generic;

namespace WebApi.EntityFramework.Models;

public partial class Nen
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Description { get; set; } = null!;

    public virtual ICollection<Hunter> Hunters { get; set; } = new List<Hunter>();
}
