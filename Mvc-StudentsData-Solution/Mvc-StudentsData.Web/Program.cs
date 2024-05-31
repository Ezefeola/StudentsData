using Microsoft.EntityFrameworkCore;
using Mvc_StudentsData.Infrastructure.Context;

var builder = WebApplication.CreateBuilder(args);

#region Services Area
// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<ApplicationDbContext>(
    options => options.UseSqlServer(connectionString)
);



builder.Services.AddControllersWithViews();


#endregion Services Area

var app = builder.Build();


#region Middlewares Area
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

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
#endregion Middlewares Area
app.Run();
