using Event.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
namespace Event;

public class HomeController : Controller
{
    private readonly DB db;
    private readonly FirebaseDbService firebase;
    public HomeController(DB db)
    {
        this.db = db;
        firebase = new FirebaseDbService();
    }

    public IActionResult Index()
    {
        return View();
    }

    // GET: Home/Both
    [Authorize]
    public IActionResult Both()
    {
        return View();
    }

    // GET: Home/Student
    [Authorize(Roles = "Student")]
    public IActionResult Student()
    {
        return View();
    }

    // GET: Home/Faculty
    [Authorize(Roles = "Central")]
    public IActionResult Central()
    {
        return View();
    }

    // GET: Home/Teacher
    [Authorize(Roles = "Teacher")]
    public IActionResult Teacher()

    {
        return View();
    }

}

