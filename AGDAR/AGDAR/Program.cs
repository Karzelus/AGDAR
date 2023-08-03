using AGDAR.Models;
using AGDAR.Repositories;
using AGDAR.Seeder;
using AGDAR.Services;
using AGDAR.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
//Services
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IClientService, ClientService>();
builder.Services.AddScoped<IWorkerService, WorkerService>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IPartService, PartService>();
builder.Services.AddScoped<IRoleService, RoleService>();
builder.Services.AddScoped<IStateService, StateService>();

//Repositories
builder.Services.AddScoped<RoleRepository>();
builder.Services.AddScoped<StateRepository>();
builder.Services.AddScoped<CategoryRepository>();
builder.Services.AddScoped<ClientRepository>();
builder.Services.AddScoped<OrderRepository>();
builder.Services.AddScoped<ProductRepository>();
builder.Services.AddScoped<WorkerRepository>();

// Mapper
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
// Seeder
builder.Services.AddScoped<AGDARSeeder>();
// DbContext
builder.Services.AddDbContext<AGDARDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
var scope = app.Services.CreateScope();
var seeder = scope.ServiceProvider.GetRequiredService<AGDARSeeder>();

seeder.Seed();

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();