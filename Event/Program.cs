global using Event;
global using Event.Models;
using Microsoft;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();
builder.Services.AddSqlServer<DB>($@"
    Data Source=(localDB)\MSSQLLocalDB;
    AttachDbFilename={builder.Environment.ContentRootPath}\EventDB.mdf;
    Initial Catalog=EventDB_AttachTest;
    Integrated Security=True;
");

builder.Services.AddAuthentication().AddCookie();
builder.Services.AddHttpContextAccessor();
builder.Services.AddSession();

var app = builder.Build();
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRequestLocalization("en-MY");
app.UseSession();

app.MapDefaultControllerRoute();
app.Run();
