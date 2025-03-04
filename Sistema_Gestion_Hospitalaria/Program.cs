
using CapaDatos;
using CapaNegocios;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<PacienteDAL>();  // Registrar PacienteDAL
builder.Services.AddScoped<PacienteBL>();   // Registrar PacienteBL
builder.Services.AddScoped<EspecialidadDAL>();  // Registrar PacienteDAL
builder.Services.AddScoped<EspecialidadBL>();
builder.Services.AddScoped<TratamientoDAL>();  
builder.Services.AddScoped<TratamientoBL>();
builder.Services.AddScoped<AdministradorDAL>();
builder.Services.AddScoped<AdministradorBL>(); 
builder.Services.AddScoped<CitaDAL>();
builder.Services.AddScoped<CitaBL>();
builder.Services.AddScoped<MedicoDAL>();
builder.Services.AddScoped<MedicoBL>();
builder.Services.AddScoped<FacturacionDAL>();
builder.Services.AddScoped<FacturacionBL>();

builder.Services.AddDbContext<HospitalDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("HospitalDB")));

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); // Tiempo de expiración de la sesión
    options.Cookie.HttpOnly = true; // La cookie solo es accesible desde el servidor
    options.Cookie.IsEssential = true; // La cookie es esencial para el funcionamiento de la aplicación
});

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

app.UseSession();

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Acceso}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
