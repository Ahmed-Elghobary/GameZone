

using GameZone.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var ConnectionString = builder.Configuration.GetConnectionString(name: "DefaultConnection")
    ?? throw new InvalidOperationException(message: "No Connection String Was Found");

builder.Services.AddDbContext<GameDbContext>(options =>
options.UseSqlServer(connectionString: ConnectionString));

builder.Services.AddControllersWithViews();
builder.Services.AddScoped<ICategoriesServices, CategoriesServices>();
builder.Services.AddScoped<IDevicesServices, DevicesServices>();
builder.Services.AddScoped<IGameServices, GameServices>();

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
