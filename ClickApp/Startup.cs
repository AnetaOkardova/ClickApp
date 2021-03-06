using ClickApp.Data;
using ClickApp.Models;
using ClickApp.Repositories;
using ClickApp.Repositories.Interfaces;
using ClickApp.Services;
using ClickApp.Services.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClickApp
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("ClickDB")));
            services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<ApplicationDbContext>();
            services.AddControllersWithViews();
            services.AddRazorPages();



            services.ConfigureApplicationCookie(options =>
            {
                options.ExpireTimeSpan = TimeSpan.FromDays(int.Parse(Configuration["CookieExpirationPeriod"]));
                //options.LoginPath = "/admin/login";
            });

            services.AddTransient<IOffersService, OffersService>();
            services.AddTransient<IGeneralFieldsService, GeneralFieldsService>();
            services.AddTransient<ISkillsService, SkillsService>();
            services.AddTransient<IUserSkillsService, UserSkillsService>();
            services.AddTransient<IInterestsService, InterestsService>();
            services.AddTransient<IUserInterestsService, UserInterestsService>();
            services.AddTransient<IFriendshipRequestsService, FriendshipRequestsService>();
            services.AddTransient<IFriendshipService, FriendshipService>();
            services.AddTransient<ICarpoolOfferService, CarpoolOfferService>();
            services.AddTransient<ICarpoolPassengerAcceptancesService, CarpoolPassengerAcceptancesService>();
            services.AddTransient<ICarpoolPassengerRequestsService, CarpoolPassengerRequestsService>();
            services.AddTransient<IMessagesService, MessagesService>();
            services.AddTransient<IJournalEntriesService, JournalEntriesService>();
            services.AddTransient<INotesService, NotesService>();
            services.AddTransient<IJournalThemesService, JournalThemesService>();
            services.AddTransient<ISubscriptionsService, SubscriptionsService>();

            services.AddTransient<IOffersRepository, OffersRepository>();
            services.AddTransient<IGeneralFieldsRepository, GeneralFieldsRepository>();
            services.AddTransient<ISkillsRepository, SkillsRepository>();
            services.AddTransient<IUserSkillsRepository, UserSkillsRepository>();
            services.AddTransient<IInterestsRepository, InterestsRepository>();
            services.AddTransient<IUserInterestsRepository, UserInterestsRepository>();
            services.AddTransient<IFriendshipRequestsRepository, FriendshipRequestsRepository>();
            services.AddTransient<IFriendshipRepository, FriendshipRepository>();
            services.AddTransient<ICarpoolOffersRepository, CarpoolOffersRepository>();
            services.AddTransient<ICarpoolPassengerRequestsRepository, CarpoolPassengerRequestsRepository>();
            services.AddTransient<ICarpoolPassengerAcceptancesRepository, CarpoolPassengerAcceptancesRepository>();
            services.AddTransient<IMessagesRepository, MessagesRepository>();
            services.AddTransient<IJournalEntriesRepository, JournalEntriesRepository>();
            services.AddTransient<INotesRepository, NotesRepository>();
            services.AddTransient<IJournalThemesRepository, JournalThemesRepository>();
            services.AddTransient<ISubscriptionsRepository, SubscriptionsRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
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

            app.UseAuthentication();
            app.UseAuthorization();

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
