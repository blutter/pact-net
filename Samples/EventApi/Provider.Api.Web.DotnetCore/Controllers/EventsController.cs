using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Provider.Api.Web.DotnetCore.Models;

namespace Provider.Api.Web.DotnetCore.Controllers
{
    public class EventsController : Controller
    {
        //[Authorize]
        //[HttpGet("events")]
        public IEnumerable<Event> Get()
        {
            return GetAllEventsFromRepo();
        }

        [HttpGet("events/{id}", Name = "GetEvent")]
        public Event GetById(Guid id)
        {
            return GetAllEventsFromRepo().First(x => x.EventId == id);
        }

        [HttpGet("events/{type?}")]
        [ProducesResponseType(typeof(IEnumerable<Event>), 200)]
        public IActionResult GetByType([FromQuery] string type)
        //public IEnumerable<Event> GetByType([FromQuery] string type)
        {
            if (type == null)
            {
                if (User.Identity.IsAuthenticated)
                {
                    return Ok(Get());
                } else
                {
                    return Unauthorized();
                }
            }
            return Ok(GetAllEventsFromRepo().Where(x => x.EventType.Equals(type, StringComparison.InvariantCultureIgnoreCase)));
        }

        [HttpPost("events")]
        public IActionResult Post([FromBody] Event @event)
        {
            if (@event == null)
            {
                return BadRequest();
            }

            return CreatedAtAction("GetById", new { id = @event.EventId }, @event);
        }

        private IEnumerable<Event> GetAllEventsFromRepo()
        {
            return new List<Event>
            {
                new Event
                {
                    EventId = Guid.Parse("45D80D13-D5A2-48D7-8353-CBB4C0EAABF5"),
                    Timestamp = DateTime.Parse("2014-06-30T01:37:41.0660548"),
                    EventType = "SearchView"
                },
                new Event
                {
                    EventId = Guid.Parse("83F9262F-28F1-4703-AB1A-8CFD9E8249C9"),
                    Timestamp = DateTime.Parse("2014-06-30T01:37:52.2618864"),
                    EventType = "DetailsView"
                },
                new Event
                {
                    EventId = Guid.Parse("3E83A96B-2A0C-49B1-9959-26DF23F83AEB"),
                    Timestamp = DateTime.Parse("2014-06-30T01:38:00.8518952"),
                    EventType = "SearchView"
                }
            };
        }
    }
}