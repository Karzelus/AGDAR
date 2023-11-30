using AGDAR;
using AGDAR.Models;
using AGDAR.Models.DTO;
using AGDAR.Models.Status;
using AGDAR.Models.Validators;
using AGDAR.Repositories;
using AGDAR.Seeder;
using AGDAR.Services;
using AGDAR.Services.Interfaces;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);


//Authentication

var authenticationSettings = new AuthenticationSettings();
builder.Configuration.GetSection("Authentication").Bind(authenticationSettings);

builder.Services.AddSingleton(authenticationSettings);
builder.Services.AddAuthentication(option =>
{
    option.DefaultAuthenticateScheme = "Bearer";
    option.DefaultScheme = "Bearer";
    option.DefaultChallengeScheme = "Bearer";
}).AddJwtBearer(cfg =>
{
    cfg.RequireHttpsMetadata = false;
    cfg.SaveToken = true;
    cfg.TokenValidationParameters = new TokenValidationParameters
    {
        ValidIssuer = authenticationSettings.jwtIssuer,
        ValidAudience = authenticationSettings.jwtIssuer,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(authenticationSettings.JwtKey)),
    };
});
//Add service SESSION
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(20);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});
// Add services to the container.
builder.Services.AddControllersWithViews().AddFluentValidation();
//Services
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IClientService, ClientService>();
builder.Services.AddScoped<IWorkerService, WorkerService>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IPartService, PartService>();
builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddScoped<IRoleService, RoleService>();
builder.Services.AddScoped<IStateService, StateService>();
builder.Services.AddScoped<IOrderHistoryService, OrderHistoryService>();
builder.Services.AddScoped<IServiceProductService, ServiceProductService>();
builder.Services.AddScoped<IValidator<WorkerDto>, RegisterWorkerDtoValidator>();
builder.Services.AddScoped<IPasswordHasher<Worker>, PasswordHasher<Worker>>();
builder.Services.AddScoped<IPasswordHasher<Client>, PasswordHasher<Client>>();

builder.Services.AddSingleton<Status>();
builder.Services.AddSingleton<PartType>();
builder.Services.AddSingleton<ProductType>();
builder.Services.AddSingleton<ProductState>();


//Repositories
builder.Services.AddScoped<RoleRepository>();
builder.Services.AddScoped<StateRepository>();
builder.Services.AddScoped<CategoryRepository>();
builder.Services.AddScoped<ProductCategoryRepository>();
builder.Services.AddScoped<PartProductRepository>();
builder.Services.AddScoped<OrderProductRepository>();
builder.Services.AddScoped<ClientRepository>();
builder.Services.AddScoped<OrderRepository>();
builder.Services.AddScoped<ProductRepository>();
builder.Services.AddScoped<WorkerRepository>();
builder.Services.AddScoped<OrderHistoryRepository>();
builder.Services.AddScoped<ServiceProductRepository>();
builder.Services.AddScoped<PartRepository>();

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

app.UseAuthentication();
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();
app.UseSession();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Products}/{action=Index}/{id?}");

app.Run();