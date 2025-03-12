using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using ProductCRUD.Data;
using ProductCRUD.Repository;

var builder = WebApplication.CreateBuilder(args);

//Register repository services
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

// Add services to the container.(for database connection)
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<ApplicatonDbContext> (options=> 
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnections")));

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

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Product}/{action=Index}/{id?}");

app.Run();
