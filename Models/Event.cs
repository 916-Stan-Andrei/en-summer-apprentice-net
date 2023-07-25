using System;
using System.Collections.Generic;

namespace TMS.Models;

public partial class Event
{
    public int EventId { get; set; }

    public int? LocationId { get; set; }

    public int? EventtypeId { get; set; }

    public string? Name { get; set; }

    public string? Description { get; set; }

    public DateTime? StartDate { get; set; }

    public DateTime? EndDate { get; set; }

    public virtual EventType? Eventtype { get; set; }

    public virtual Location? Location { get; set; }

    public virtual ICollection<TicketCategory> TicketCategories { get; set; } = new List<TicketCategory>();
}
