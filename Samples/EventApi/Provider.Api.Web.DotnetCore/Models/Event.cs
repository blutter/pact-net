using System;

namespace Provider.Api.Web.DotnetCore.Models
{
    public class Event
    {
        public Guid EventId { get; set; }
        public DateTime Timestamp { get; set; }
        public string EventType { get; set; }
    }
}
