using Cavex.Principal.Data;
using Cavex.Principal.Infraesctructure.Settings;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<ApiSettings>(
    builder.Configuration.GetSection("ApiSettings"));

//builder.Services.AddDbContext<CavexPrincipalContext>(options =>
// options.UseSqlServer(builder.Configuration.GetConnectionString("CavexPrincipalContext") ?? throw new InvalidOperationException("Connection string 'CavexPrincipalContext' not found.")));

// Add services to the container.
builder.Services.AddControllersWithViews()
    .AddRazorOptions(options =>
    {
        options.ViewLocationFormats.Add("/Views/Shared/UI/{0}.cshtml");
        options.ViewLocationFormats.Add("/Views/Shared/Cotizaciones/{0}.cshtml");
        options.ViewLocationFormats.Add("/Views/Shared/Sucursales/{0}.cshtml");
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Auth}/{action=Login}/{id?}")
    .WithStaticAssets();


app.Run();
