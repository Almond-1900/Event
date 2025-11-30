using Event.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
namespace Event;

public class AccountController : Controller
{
    private readonly DB db;
    private readonly Helper hp;
    private readonly FirebaseDbService firebase;
    public AccountController(DB db, Helper hp)
    {
        this.db = db;
        this.hp = hp;
        firebase = new FirebaseDbService();
    }

    // GET: Account/Login
    public IActionResult Login()
    { 
        return View();
    }

    // POST: Account/Login
    [HttpPost]
    public IActionResult Login(LoginVM vm, string? returnURL)
    {
        var u = db.Users.FirstOrDefault(u => u.Email == vm.Email);
        if (u == null || !hp.VerifyPassword(u.Password, vm.Password))
        {
            ModelState.AddModelError("", "Login credentials not matched.");
        }

        if (ModelState.IsValid)
        {
            TempData["Info"] = "Login successfully.";

            hp.SignIn(u!.Email, u.Role, vm.RememberMe);

            if (string.IsNullOrEmpty(returnURL))
            {
                return RedirectToAction("Index", "Home");
            }
        }

        return View(vm);
    }

    // GET: Account/Logout
    public IActionResult Logout(string? returnURL)
    {
        TempData["Info"] = "Logout successfully.";

        // Sign out
        hp.SignOut();

        return RedirectToAction("Index", "Home");
    }

    // GET: Account/AccessDenied
    public IActionResult AccessDenied(string? returnURL)
    {
        return View();
    }



    // ------------------------------------------------------------------------
    // Others
    // ------------------------------------------------------------------------

    public bool CheckEmail(string email)
    {
        return !db.Users.Any(u => u.Email == email);
    }

    // GET: Account/Register
    public IActionResult Register()
    {
        return View();
    }

    // POST: Account/Register
    [HttpPost]
    public IActionResult Register(RegisterVM vm)
    {
        // Check email duplicated
        if (db.Users.Any(u => u.Email == vm.Email))
        {
            ModelState.AddModelError("Email", "Email already exists.");
        }

        var user = new Users
        {
            Email = vm.Email,
            Password = hp.HashPassword(vm.Password),
            Name = vm.Name,
            Role = "Guest" 
        };

        db.Users.Add(user);
        db.SaveChanges();

        TempData["Info"] = "Register successfully. Please login.";
        return RedirectToAction("Login");
    }




    //===============================================================
    //Profile
    //===============================================================
    // GET: Account/UpdatePassword
    [Authorize]
    public IActionResult UpdatePassword()
    {
        return View();
    }

    //=================================================================
    //UpdatePassword
    //=================================================================
    [Authorize]
    [HttpPost]
    public IActionResult UpdatePassword(UpdatePasswordVM vm)
    {
        var u = db.Users.FirstOrDefault(u => u.Email == User.Identity!.Name);
        if (u == null) return RedirectToAction("Index", "Home");

        // If current password not matched
        if (!hp.VerifyPassword(u.Password, vm.Current))
        {
            ModelState.AddModelError("Current", "Current Password not matched.");
        }

        if (ModelState.IsValid)
        {
            // Update user password (Password)
            u.Password = hp.HashPassword(vm.New);
            db.SaveChanges();

            TempData["Info"] = "Password updated.";
            return RedirectToAction();
        }

        return View();
    }

    // GET: Account/UpdateProfile

    [Authorize(Roles = "Guest")]
    public IActionResult UpdateProfile()
    {
        var m = db.Users.Find(User.Identity!.Name);
        if (m == null) return RedirectToAction("Index", "Home");

        var vm = new UpdateProfileVM
        {
            UserId = m.UserId,
            Name = m.Name,
        };

        return View(vm);
    }

    // POST: Account/UpdateProfile

    [Authorize(Roles = "Guest")]
    [HttpPost]
    public IActionResult UpdateProfile(UpdateProfileVM vm)
    {
        var m = db.Users.Find(User.Identity!.Name);
        if (m == null) return RedirectToAction("Index", "Home");

        if (ModelState.IsValid)
        {
            m.Name = vm.Name;
            db.SaveChanges();

            TempData["Info"] = " ";
            return RedirectToAction();
        }
        vm.UserId = m.UserId;
        return View(vm);
    }

    // GET: Account/ResetPassword
    public IActionResult ResetPassword()
    {
        return View();
    }

    // POST: Account/ResetPassword
    [HttpPost]
    public IActionResult ResetPassword(ResetPasswordVM vm)
    {
        var u = db.Users.FirstOrDefault(u => u.Email == vm.Email);

        if (u == null)
        {
            ModelState.AddModelError("UserId", "UserId not found.");
        }

        if (ModelState.IsValid)
        {
            // Generate random password
            string password = hp.RandomPassword();

            // Update user (Teacher or Guest) record
            u!.Password = hp.HashPassword(password);
            db.SaveChanges();

            //Send reset password UserId

            TempData["Info"] = $"Password reset to <b>{password}</b>.";
            return RedirectToAction();
        }
        return View();
    }
}
