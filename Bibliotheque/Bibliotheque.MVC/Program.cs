using Bibliotheque.MVC.Data;
using Bibliotheque.MVC.Interfaces;
using Bibliotheque.MVC.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
//builder.Services.Configure<RequestLocalizationOptions>(options =>
//{
//    options.DefaultRequestCulture = new RequestCulture("en-US");
//});

builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<BibliothequeContext>(options =>
  options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddHttpClient<ILivresService, 
    LivresServiceProxy>(client => client.BaseAddress = new Uri(builder.Configuration.GetValue<string>("UrlAPI")));

builder.Services.AddHttpClient<IUsagersService,
    UsagersServiceProxy>(client => client.BaseAddress = new Uri(builder.Configuration.GetValue<string>("UrlAPI")));

builder.Services.AddHttpClient<IEmpruntsService, 
    EmpruntsServiceProxy>(client => client.BaseAddress = new Uri(builder.Configuration.GetValue<string>("UrlAPI")));

//builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddScoped<IGenerateurCode, GenerateurCode>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    var context = services.GetRequiredService<BibliothequeContext>();
    context.Database.EnsureCreated();
    DbInitializer.Initialize(context);
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();