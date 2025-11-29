using Accounting_Managment_System_Frontend.Services;

namespace Accounting_Managment_System_Frontend
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            // register ApiService with a base address pointing to your backend API
            builder.Services.AddHttpClient<ApiService>(client =>
            {
                client.BaseAddress = new Uri(builder.Configuration["ApiBaseUrl"] ?? "http://localhost:5175/"); // configure in appsettings if you like
            });
            // Add services to the container.
            builder.Services.AddControllersWithViews();

            builder.Services.AddSession();
            builder.Services.AddDistributedMemoryCache();

            builder.Services.AddHttpContextAccessor();

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
            app.UseSession();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
