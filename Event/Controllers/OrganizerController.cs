using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

public class OrganizerController : Controller
{
    private readonly DB db;
    public OrganizerController(DB db)
    {
        this.db = db;
    }

    // GET: Events/Create
    [HttpGet]
    public IActionResult OCreate()
    {
        var model = new OcreateVM
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
    public IActionResult Ocreate(OcreateVM model)
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
            TotalTicket = model.TotalTicket,
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
}
