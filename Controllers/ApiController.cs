using CodeFirstApi.Data;
using CodeFirstApi.Model;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CodeFirstApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApiController : ControllerBase
    {

        private readonly ApplicationDbContext db;
        public ApiController(ApplicationDbContext _db)
        {

            db = _db;
        }

        [HttpGet]
        public IActionResult GetEvents()
        {
            var Events = db.Events.Include(q => q.EventType);
            return Ok(Events);
        }

        [HttpGet("EventTypes")]

        public IActionResult GetEventsTypes()
        {
            var events = db.EventType;
            return Ok(events);
        }

        [HttpPost]

        public IActionResult AddEvents(EventDTO ev)
        {
            if (ev != null)
            {
                Event newEvent = new Event()
                {
                    NoOfGuests = ev.NoOfGuests,
                    CustomerName = ev.CustomerName,
                    Data = ev.Data,
                    EventTypeId = ev.EventTypeId

                };

                var addedEvent = db.Events.Add(newEvent);
                db.SaveChanges();
                return Ok(addedEvent.Entity);

            }
            else
            {
                return BadRequest("INVALID DATA");
            }


        }
    }
}
