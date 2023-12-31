using Mango.Web.IServices;
using Mango.Web.Services;
using Mango.Web.Utility;

namespace Mango.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();


            //configure service
            SD.CouponAPIBase = builder.Configuration["ServiceUrls:CouponAPI"];
            SD.AuthAPIBase = builder.Configuration["ServiceUrls:AuthAPI"];
            builder.Services.AddHttpContextAccessor(); //to register http client factory
            builder.Services.AddHttpClient<ICouponService, CouponService>();
            builder.Services.AddHttpClient<IAuthService, AuthService>();


            builder.Services.AddScoped<ICouponService, CouponService>();
            builder.Services.AddScoped<IAuthService, AuthService>();
            builder.Services.AddScoped<IBaseService, BaseService>();


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
        }
    }
}