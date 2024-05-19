


global using AutoMapper.Internal;
using E_Commerece_PL.Settings;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;

namespace E_Commerece_PL
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();


            builder.Services.AddDbContext<MVCDataBase>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            }, ServiceLifetime.Scoped);
            /*       builder.Services.AddScoped<IDepartmentRepository, DepartmentRepository>();
                   builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();*/
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            builder.Services.AddAutoMapper(M => M.AddProfile(new EmployeeProfile()));
            builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<MVCDataBase>()
                .AddDefaultTokenProviders();
            builder.Services.AddAutoMapper(M => M.AddProfile(new UserProfile()));
            builder.Services.AddAutoMapper(M => M.AddProfile(new RoleProfile()));

            builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
            {
                options.Password.RequireNonAlphanumeric = true; // @#$%
                options.Password.RequireUppercase = true;
                options.Password.RequireUppercase = true;
                options.Password.RequireDigit = true;
                //options.Password.RequiredLength = 4;
            })

             .AddEntityFrameworkStores<MVCDataBase>()
             .AddDefaultTokenProviders();


            builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(p =>
                {
                    p.LoginPath = "account/login";
                    p.AccessDeniedPath = "kkf";
                }
                );
            builder.Services.Configure<MailSettings>(builder.Configuration.GetSection("MailSettings"));
            builder.Services.AddTransient<IEmailSettings, EmailSettings>();
            builder.Services.Configure<TwillioSettings>(builder.Configuration.GetSection("Twillio"));
            builder.Services.AddTransient<ISmsService, SmsService>();
            builder.Services.AddAuthentication(o =>
            {
                o.DefaultAuthenticateScheme = GoogleDefaults.AuthenticationScheme;
                o.DefaultChallengeScheme = GoogleDefaults.AuthenticationScheme;
            }).AddGoogle(o =>
            {
                IConfiguration GoogleAuthSection = builder.Configuration.GetSection("Authentcation:Google");
                o.ClientId = GoogleAuthSection["ClientId"];
                o.ClientSecret = GoogleAuthSection["ClientSecret"];
            });
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
            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
