var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Add HTTP client for API calls
builder.Services.AddHttpClient();

// Configure cookie-based authentication
builder.Services.AddAuthentication("Cookies")
    .AddCookie("Cookies", options =>
    {
        options.LoginPath = "/Auth/Index"; // Ensure this matches your login route
        options.LogoutPath = "/Auth/Logout"; // Logout route
        options.ExpireTimeSpan = TimeSpan.FromMinutes(30); // Optional: Set cookie expiration
        options.SlidingExpiration = true; // Optional: Refresh expiration on activity
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles(); // Ensure this is present for serving static files

app.UseRouting();

// Add authentication and authorization middlewares
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();