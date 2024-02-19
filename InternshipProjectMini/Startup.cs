using InternshipProjectMini.Constants;
using InternshipProjectMini.Context;
using InternshipProjectMini.Permission;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace InternshipProjectMini
{
    public class Startup
    {
        // Inject IConfiguration
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IAuthorizationPolicyProvider, PermissionPolicyProvider>();
            services.AddScoped<IAuthorizationHandler, PermissionAuthorizationHandler>();

            // Use Configuration property to access configuration
            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentity<IdentityUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultUI()
                .AddDefaultTokenProviders();

            services.AddAuthorization(options =>
            {
                options.AddPolicy("RequireSuperAdmin", policy => policy.RequireRole("SuperAdmin"));

                options.AddPolicy("EmployeePermissions", policy =>
                {
                    policy.RequireClaim(Permissions.Employee.Create);
                    policy.RequireClaim(Permissions.Employee.Edit);
                    policy.RequireClaim(Permissions.Employee.Details);
                    policy.RequireClaim(Permissions.Employee.Delete);
                });

                options.AddPolicy("DepartmentPermissions", policy =>
                {
                    policy.RequireClaim(Permissions.Department.Create);
                    policy.RequireClaim(Permissions.Department.Edit);
                    policy.RequireClaim(Permissions.Department.Details);
                    policy.RequireClaim(Permissions.Department.Delete);
                });
                options.AddPolicy("LocationPermissions", policy =>
                {
                    policy.RequireClaim(Permissions.Location.Create);
                    policy.RequireClaim(Permissions.Location.Edit);
                    policy.RequireClaim(Permissions.Location.Details);
                    policy.RequireClaim(Permissions.Location.Delete);
                });
                options.AddPolicy("MachinePermissions", policy =>
                {
                    policy.RequireClaim(Permissions.Machine.Create);
                    policy.RequireClaim(Permissions.Machine.Edit);
                    policy.RequireClaim(Permissions.Machine.Details);
                    policy.RequireClaim(Permissions.Machine.Delete);
                });

                
            });

            services.ConfigureApplicationCookie(options =>
            {
                options.AccessDeniedPath = "/Account/AccessDenied";
            });

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = IdentityConstants.ApplicationScheme;
                options.DefaultChallengeScheme = IdentityConstants.ApplicationScheme;
                options.DefaultSignInScheme = IdentityConstants.ExternalScheme;
            })
            .AddIdentityCookies(options => { });

            services.AddControllersWithViews();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
