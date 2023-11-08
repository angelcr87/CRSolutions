using CRSolutions.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("CRSolutionsDBContext");
builder.Services.AddDbContext<CRSolutionsDBContext>(options =>
    options.UseSqlServer(connectionString));
//builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromSeconds(300);
    options.Cookie.HttpOnly = true;
});

builder.Services.AddControllersWithViews();
var app = builder.Build();

//using (var scope = app.Services.CreateScope())
//{
//    var CRSolutionsContext = scope.ServiceProvider.GetRequiredService<CRSolutionsContext>();
//    CRSolutionsContext.Database.Migrate();
//}

    // Configure the HTTP request pipeline.
    if (!app.Environment.IsProduction())
    {
        app.UseExceptionHandler("/Home/Error");
        // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
        app.UseHsts();
    }

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.UseSession();

app.MapControllerRoute(
    name: "default",
     pattern: "{controller=Users}/{action=Login}/{id?}");
//pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
