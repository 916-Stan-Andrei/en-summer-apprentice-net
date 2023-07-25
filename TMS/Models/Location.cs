using System;
using System.Collections.Generic;

namespace TMS.Models;

public partial class Location
{
    public int LocationId { get; set; }

    public string? Name { get; set; }

    public string? Type { get; set; }

    public int Capacity { get; set; }

    public virtual ICollection<Event> Events { get; set; } = new List<Event>();
}
