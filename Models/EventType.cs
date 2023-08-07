using System;
using System.Collections.Generic;

namespace TMS.Models
{
    public partial class EventType
    {
        public EventType()
        {
            Events = new HashSet<Event>();
        }

        public int EventtypeId { get; set; }
        public string? Name { get; set; }

        public virtual ICollection<Event> Events { get; set; }
    }
}
