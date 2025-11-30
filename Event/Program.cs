using Event;
using Event.Models;
using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<Helper>();
//Add Session
builder.Services.AddSession(options => { 
    options.IdleTimeout = TimeSpan.FromMinutes(20);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});
//=====================================//
//Firebase Realtime Database//
FirebaseApp.Create(new AppOptions
{
    Credential = GoogleCredential.FromFile("eventaspnet-firebase-adminsdk-fbsvc-d000e98266.json")
});

var client = new Firebase.Database.FirebaseClient(
    "https://eventaspnet-default-rtdb.firebaseio.com/",
    new Firebase.Database.FirebaseOptions
    {
        AuthTokenAsyncFactory = async () =>
            await GoogleCredential.FromFile("eventaspnet-firebase-adminsdk-fbsvc-421c0d011e.json")
                .UnderlyingCredential
                .GetAccessTokenForRequestAsync()
    });
//=====================================//
builder.Services.AddAuthentication().AddCookie();
builder.Services.AddHttpContextAccessor();
builder.Services.AddSession();
builder.Services.AddDbContext<DB>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();
app.UseSession();
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRequestLocalization("en-MY");
app.UseSession();

app.MapDefaultControllerRoute();
app.Run();
