using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using AttrOleo.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using AttrOleo.Models;
using AttrOleo.Repository;
using Microsoft.AspNetCore.Mvc;
using AttrOleo.Entities;
using AttrOleo.Services;
using AttrOleo.ModelsAreaACC;
using AttrOleo.ModelsAreaGHI;
using AttrOleo.ModelsAreaLAM;
using AttrOleo.ModelsAreaPGT;

namespace AttrOleo
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        private void ImportOldDB(IServiceProvider serviceProvider)
        {

            var ruoli = new List<String>();

            ruoli.Add("PGT");
            var contextAREAPGT = new AttrezzatureOleodinamicheAREAPGTContext();
            var areeAREAPGT = contextAREAPGT.UtImpianti.ToList();
            areeAREAPGT.ForEach(area => ruoli.Add(area.Nome.ToUpper().Trim()));
            var tecniciAREAPGT = contextAREAPGT.UtTecnici.ToList();
            tecniciAREAPGT.ForEach(q => {
                Console.WriteLine(">>>>>>>>>>>>>>>>>>>>>>nome<<<<<<< " + q.CognomeNome.ToUpper().Trim());
                var impiantoAREAPGT = contextAREAPGT.UtImpianti.FirstOrDefault(imp => imp.Id == q.ImpiantoAppartenenza);
                Console.WriteLine(">>>>>>>>>>>>>>>>>>>>>>impianto "+q.ImpiantoAppartenenza+"<<<<<<< " + impiantoAREAPGT.Nome);
            });

            ruoli.Add("ACC");
            var contextAREAACC = new AttrezzatureOleodinamicheAREAACCContext();
            var impiantiAREAACC = contextAREAACC.UtImpianti.ToList();
            impiantiAREAACC.ForEach(area => ruoli.Add(area.Nome.ToUpper().Trim()));
            var areeAREAACC = contextAREAACC.UtAree.ToList();
            areeAREAACC.ForEach(area => ruoli.Add(area.Area.ToUpper().Trim()));

            ruoli.Add("GHI");
            var contextAREAGHI = new AttrezzatureOleodinamicheAREAGHIContext();
            var impiantiAREAGHI = contextAREAGHI.UtImpianti.ToList();
            impiantiAREAGHI.ForEach(area => ruoli.Add(area.Nome.ToUpper().Trim()));
            var areeAREAGHI = contextAREAGHI.UtAree.ToList();
            areeAREAGHI.ForEach(area => ruoli.Add(area.Area.ToUpper().Trim()));

            ruoli.Add("LAM");
            var contextAREALAM = new AttrezzatureOleodinamicheAREALAMContext();
            var impiantiAREALAM = contextAREALAM.UtImpianti.ToList();
            impiantiAREALAM.ForEach(area => ruoli.Add(area.Nome.ToUpper().Trim()));
            var areeAREALAM = contextAREALAM.UtAree.ToList();
            areeAREALAM.ForEach(area => ruoli.Add(area.Area.ToUpper().Trim()));

            var RoleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            
            ruoli.ForEach(async area => 
            {
                //var roleExist = await RoleManager.RoleExistsAsync(area);
                var roleExist = RoleManager.FindByNameAsync(area).GetAwaiter().GetResult();
                
                if (roleExist == null) 
                {
                    //Console.WriteLine(">>>>>>>><<<" + roleExist.Id);
                    Console.WriteLine("inserisco ruolo:"+area);
                    //create the roles and seed them to the database: Question 1
                    RoleManager.CreateAsync(new IdentityRole(area)).GetAwaiter().GetResult();
                } else
                {
                    Console.WriteLine("già esiste ruolo:" + area);
                }
            });



        }
        private async Task CreateRoles(IServiceProvider serviceProvider)
        {
            try
            {

                //initializing custom roles 
                var RoleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
                var UserManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
                string[] roleNames = { "Admin", "Manager", "RW" };
                
                IdentityResult roleResult;

                foreach (var roleName in roleNames)
                {
                    var roleExist = await RoleManager.RoleExistsAsync(roleName);
                    if (!roleExist)
                    {
                        //create the roles and seed them to the database: Question 1
                        roleResult = await RoleManager.CreateAsync(new IdentityRole(roleName));
                    }
                }


                //Ensure you have these values in your appsettings.json file
                string userPWD = Configuration.GetValue<string>("UserPassword");
                string user = Configuration.GetValue<string>("AdminUserEmail");
                //Console.WriteLine(">>>>>>>>>>>>>>>>>>>>>>>User:"+ user);
                //Console.WriteLine(">>>>>>>>>>>>>>>>>>>>>>>Pwd:" + userPWD);
                //Here you could create a super user who will maintain the web app
                var poweruser = new ApplicationUser
                {

                    UserName = user,
                    Email = user,
                    EmailConfirmed = true,
                    Name = user,
                    DOB = DateTime.Now
                };
                var _user = await UserManager.FindByEmailAsync(user);

                if (_user == null)
                {
                    var createPowerUser = await UserManager.CreateAsync(poweruser, userPWD);
                    if (createPowerUser.Succeeded)
                    {
                        //here we tie the new user to the role
                        await UserManager.AddToRoleAsync(poweruser, "Admin");

                    }
                }
                else
                {
                    await UserManager.AddToRoleAsync(_user, "Admin");
                }
            }
            catch
            {
                Console.WriteLine(">>>>>>>>>>>>>>>>>>>>>>>errore connessione database");
            }
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));

            
            services.AddOptions();
            services.Configure<EmailSettings>(Configuration.GetSection("EmailSettings"));
            services.AddDefaultIdentity<ApplicationUser>(options =>
            {
                options.SignIn.RequireConfirmedAccount = true;
                options.SignIn.RequireConfirmedEmail = true;
            })
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>();
            services.AddSingleton<IEmailSender, EmailSender>();



            services.ConfigureApplicationCookie(options =>
            {
                // Cookie settings
                options.Cookie.HttpOnly = true;
                options.ExpireTimeSpan = TimeSpan.FromDays(5);

                options.LoginPath = "/Identity/Account/Login";
                options.AccessDeniedPath = "/Identity/Account/AccessDenied";
                options.SlidingExpiration = true;
            });
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                //options.MinimumSameSitePolicy = 0;
            });
            services.AddControllersWithViews();
            services.AddRazorPages();
            services.AddControllers(config =>
            {
                // using Microsoft.AspNetCore.Mvc.Authorization;
                // using Microsoft.AspNetCore.Authorization;
                var policy = new AuthorizationPolicyBuilder()
                                 .RequireAuthenticatedUser()
                                 .Build();
                config.Filters.Add(new AuthorizeFilter(policy));
            });

            services.AddHttpContextAccessor();
            services.AddScoped<IFileRepository, FileRepository>();
            services.AddSingleton<IAppVersionService, AppVersionService>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IServiceProvider serviceProvider)
        {
            //app.UsePathBase("/AttrOleo");
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseCookiePolicy();
            app.UseAuthentication();
            app.UseAuthorization();
            CreateRoles(serviceProvider).Wait();
            ImportOldDB(serviceProvider);
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });
        }
    }
}
