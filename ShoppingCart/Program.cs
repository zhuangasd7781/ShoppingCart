using Microsoft.AspNetCore.Authentication.Cookies;
using DataBases;

var builder = WebApplication.CreateBuilder(args);
builder.Configuration.AddJsonFile("appsettings.json");
builder.Configuration.AddJsonFile("secrets.json", optional: true);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<IDB, MySqlDB>(x =>
{
    var connectionString = $"{builder.Configuration.GetConnectionString("dekConnStr")}" +
        $"Data Source={builder.Configuration["db_Param:IP"]};" +
        $"port={builder.Configuration["db_Param:Port"]};" +
        $"Initial Catalog={builder.Configuration["db_Param:DbName"]};" +
        $"User Id={builder.Configuration["db_Param:UserId"]};" +
        $"Password={builder.Configuration["db_Param:Password"]};";
    return new MySqlDB(connectionString);
});
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Login";
        options.ExpireTimeSpan = new System.TimeSpan(0, 60, 0);
        options.SlidingExpiration = true;
        options.Cookie = new CookieBuilder
        {
            SameSite = SameSiteMode.Strict,
            SecurePolicy = CookieSecurePolicy.Always,
            IsEssential = true,
            HttpOnly = true,
            Name = "Authentication"
        };
    });
builder.Services.AddAuthorization();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
