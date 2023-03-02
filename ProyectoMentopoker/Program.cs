using Microsoft.EntityFrameworkCore;
using ProyectoMentopoker.Data;
using ProyectoMentopoker.Repositories;

var builder = WebApplication.CreateBuilder(args);
string connectionString =
    builder.Configuration.GetConnectionString("SqlMentopokerClase");


builder.Services.AddTransient<RepositoryEstadisticas>();



builder.Services.AddDbContext<MentopokerContext>
    (options => options.UseSqlServer(connectionString));

//builder.Services.AddDbContext<MentopokerContext>
//    (options => options.UseSqlServer(@"Data Source=DESKTOP-E38C8U3;Initial Catalog=PROYECTOMENTOPOKER;User ID=sa;Password=MCSD2022";));



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

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
