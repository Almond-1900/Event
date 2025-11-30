using Event.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
namespace Event.Controllers
{

    public class OrganizerController : Controller
    {
        private readonly DB db;
        private readonly Helper hp;
        private readonly FirebaseDbService firebase;
        public OrganizerController(DB db, Helper hp)
        {
            this.db = db;
            this.hp = hp;
            firebase = new FirebaseDbService();
        }

        // GET: Events/Create
        [HttpGet]
        public IActionResult OCreate()
        {
            var model = new EventVM
            {
                VenueList = db.Venues
                                    .Select(v => new SelectListItem
                                    {
                                        Value = v.VenueId.ToString(),
                                        Text = v.VenueName
                                    }).ToList()
            };
            return View(model);
        }

        // POST: Events/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Ocreate(EventVM model)
        {
            if (!ModelState.IsValid)
            {
                model.VenueList = db.Venues
                                          .Select(v => new SelectListItem
                                          {
                                              Value = v.VenueId.ToString(),
                                              Text = v.VenueName
                                          }).ToList();
                return View(model);
            }

            var newEvent = new Events
            {
                EventTitle = model.EventTitle,
                description = model.description,
                StartT = model.StartT,
                EndT = model.EndT,
                CreatedT = DateTime.Now,
                OrganizerId = model.OrganizerId,
                VenueId = model.VenueId
            };

            db.Events.Add(newEvent);
            db.SaveChanges();

            TempData["SuccessMessage"] = "Event created successfully!";
            return RedirectToAction("Index", "Events");
        }

        //=============================================================//
        //======================== Event Edit =========================//
        //=============================================================//
        [HttpGet]
        public IActionResult Oedit(int id)
        {
            var existingEvent = db.Events.Find(id);
            if (existingEvent == null)
            {
                return NotFound();
            }
            var model = new EventVM
            {
                EventId = existingEvent.EventId,
                EventTitle = existingEvent.EventTitle,
                description = existingEvent.description,
                StartT = existingEvent.StartT,
                EndT = existingEvent.EndT,
                OrganizerId = existingEvent.OrganizerId,
                VenueId = existingEvent.VenueId,
                VenueList = db.Venues
                                    .Select(v => new SelectListItem
                                    {
                                        Value = v.VenueId.ToString(),
                                        Text = v.VenueName
                                    }).ToList()
            };
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Oedit(EventVM model)
        {
            if (!ModelState.IsValid)
            {
                model.VenueList = db.Venues
                                          .Select(v => new SelectListItem
                                          {
                                              Value = v.VenueId.ToString(),
                                              Text = v.VenueName
                                          }).ToList();
                return View(model);
            }
            var existingEvent = db.Events.Find(model.EventId);
            if (existingEvent == null)
            {
                return NotFound();
            }
            existingEvent.EventTitle = model.EventTitle;
            existingEvent.description = model.description;
            existingEvent.StartT = model.StartT;
            existingEvent.EndT = model.EndT;
            existingEvent.OrganizerId = model.OrganizerId;
            existingEvent.VenueId = model.VenueId;
            db.SaveChanges();
            TempData["SuccessMessage"] = "Event updated successfully!";
            return RedirectToAction("Index", "Events");
        }

        //=============================================================//
        //======================== Event List =========================//
        //=============================================================//
        public ActionResult EventList()
        {
            var eventList = db.Events
                .Select(e => new EventVM
                {
                    EventId = e.EventId,
                    EventTitle = e.EventTitle,
                    description = e.description,
                    StartT = e.StartT,
                    EndT = e.EndT,
                    CreatedT = e.CreatedT
                })
                .ToList();

            return View(eventList);
        }
















        //=============================================================//
        //======================== Venue CRUD =========================//
        //=============================================================//
    }
}
