using e_commerce_AP.Data;
using e_commerce_AP.Models.Momo;
using e_commerce_AP.Services;
using e_commerce_AP.Services.Momo;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;




var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<MomoOptionModel>(builder.Configuration.GetSection("MomoAPI"));
builder.Services.AddScoped<IMomoService, MomoService>();



// Cấu hình Authentication với Cookie
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/LoginAdmin";  // Đường dẫn đến trang đăng nhập
        options.LogoutPath = "/LoginAdmin/Logout";  // Đường dẫn đến trang đăng xuất
    });



// Cấu hình kết nối đến cơ sở dữ liệu SQL Server
builder.Services.AddDbContext<ECommerce_APDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


// Thêm các dịch vụ cho EmailService và OtpService
builder.Services.AddTransient<EmailService>();
builder.Services.AddTransient<OtpService>();




// Cấu hình Identity
builder.Services.AddIdentity<IdentityUser, IdentityRole>(options =>
{
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireUppercase = true;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequiredLength = 6;
    options.Password.RequiredUniqueChars = 1;

    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
    options.Lockout.MaxFailedAccessAttempts = 5;
    options.Lockout.AllowedForNewUsers = true;

    options.User.RequireUniqueEmail = true;
    options.SignIn.RequireConfirmedAccount = false;
})
    .AddEntityFrameworkStores<ECommerce_APDbContext>()
    .AddDefaultTokenProviders();

// Thêm dịch vụ MVC và HttpContextAccessor
builder.Services.AddControllersWithViews();
builder.Services.AddHttpContextAccessor();

// Thêm dịch vụ Session
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
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

app.UseStaticFiles();
app.UseRouting();
app.UseSession(); // Kích hoạt Session middleware
app.UseAuthentication(); // Kích hoạt Authentication middleware
app.UseAuthorization(); // Kích hoạt Authorization middleware


// Middleware xử lý lỗi 404
app.UseStatusCodePagesWithRedirects("/Home/Error?statusCode=404");

app.UseEndpoints(endpoints =>
{
    // Route cho các Area
    endpoints.MapControllerRoute(
        name: "areas",
        pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

    // Route cho Shop
    endpoints.MapControllerRoute(
        name: "shop",
        pattern: "Shop",
        defaults: new { controller = "TbProducts", action = "Index" });

    // Route mặc định
    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");
});
app.Run();
