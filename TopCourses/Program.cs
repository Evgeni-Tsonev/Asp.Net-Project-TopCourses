using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TopCourses.Infrastructure.Data;
using TopCourses.Infrastructure.Data.Identity;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddApplicationDbContexts(builder.Configuration);

builder.Services.AddDefaultIdentity<ApplicationUser>(options =>
{
    options.SignIn.RequireConfirmedAccount = false;
    options.Password.RequireNonAlphanumeric = true;
    options.Password.RequiredLength = 6;
    options.User.RequireUniqueEmail = true;
})
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<TopCoursesDbContext>();

//builder.Services.AddAuthentication()
//    .AddFacebook(options =>
//    {
//        options.AppId = "505133611164058";
//        options.AppSecret = "ca327d57819f6267ec8baac1aec48663";
//    });

//builder.Services.AddAuthentication()
//    .AddFacebook(options =>
//    {
//        options.AppId = builder.Configuration.GetValue<string>("Facebook:AppId");
//        options.AppSecret = builder.Configuration.GetValue<string>("Facebook:AppSecret");
//    });

builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Account/Login";
});

builder.Services.AddControllersWithViews(options =>
{
    options.Filters.Add(new AutoValidateAntiforgeryTokenAttribute());
});

builder.Services.AddControllersWithViews();

builder.Services.AddApplicationServices();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseMigrationsEndPoint();
}
else
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
  name: "areas",
  pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapRazorPages();

app.Run();
