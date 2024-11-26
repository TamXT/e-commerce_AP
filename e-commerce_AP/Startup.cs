using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration; // Sử dụng đúng namespace cho IConfiguration
using Microsoft.Extensions.DependencyInjection;
using e_commerce_AP.Data;
using e_commerce_AP.Models.Momo;
using e_commerce_AP.Services.Momo; // Namespace cho DbContext

namespace e_commerce_AP
{
    public class Startup
    {
        public IConfiguration Configuration { get; } // Thêm thuộc tính Configuration

        // Constructor nhận IConfiguration từ Dependency Injection
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // Cấu hình các dịch vụ
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<MomoOptionModel>(Configuration.GetSection("MomoAPI"));
            services.AddScoped<IMomoService, MomoService>();

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    options.LoginPath = "/LoginAdmin"; // Trang đăng nhập
                });

            

            services.AddControllersWithViews();

            services.AddDbContext<ECommerce_APDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            services.AddCors(options =>
            {
                options.AddPolicy("AllowAllOrigins",
                    builder => builder.AllowAnyOrigin()
                                      .AllowAnyMethod()
                                      .AllowAnyHeader());
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts(); // Chỉ giữ lại HSTS trong môi trường production
            }


            app.UseCors("AllowAllOrigins");

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication(); // Cấu hình middleware xác thực
            app.UseAuthorization();  // Cấu hình middleware ủy quyền

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");

                endpoints.MapAreaControllerRoute(
                    name: "admin",
                    areaName: "Admin",
                    pattern: "Admin/{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
