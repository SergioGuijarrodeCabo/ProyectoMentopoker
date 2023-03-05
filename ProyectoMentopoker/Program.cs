using Microsoft.EntityFrameworkCore;
using ProyectoMentopoker.Data;
using ProyectoMentopoker.Repositories;

var builder = WebApplication.CreateBuilder(args);
string connectionString =
    builder.Configuration.GetConnectionString("SqlMentopokerCasa");


builder.Services.AddTransient<RepositoryEstadisticas>();

builder.Services.AddTransient<RepositoryLogin>();





builder.Services.AddDbContext<MentopokerContext>
    (options => options.UseSqlServer(connectionString));

//builder.Services.AddDbContext<MentopokerContext>
//    (options => options.UseSqlServer(@"Data Source=DESKTOP-E38C8U3;Initial Catalog=PROYECTOMENTOPOKER;User ID=sa;Password=MCSD2022";));

builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
});

builder.Services.AddAntiforgery();

// Add services to the container.
builder.Services.AddControllersWithViews();

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
app.UseSession();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Login}/{action=Index}/{id?}");

app.Run();
