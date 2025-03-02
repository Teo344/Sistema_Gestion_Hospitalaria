
using CapaDatos;
using CapaNegocios;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<PacienteDAL>();  // Registrar PacienteDAL
builder.Services.AddScoped<PacienteBL>();   // Registrar PacienteBL
builder.Services.AddScoped<EspecialidadDAL>();  // Registrar PacienteDAL
builder.Services.AddScoped<EspecialidadBL>();
builder.Services.AddScoped<MedicoDAL>();
builder.Services.AddScoped<MedicoBL>();

builder.Services.AddDbContext<HospitalDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("HospitalDB")));

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
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
