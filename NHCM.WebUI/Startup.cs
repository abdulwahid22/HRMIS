using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using FluentValidation.AspNetCore;
using MediatR;
using MediatR.Pipeline;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Options; 
using NHCM.Application.Infrastructure;
using NHCM.Application.Recruitment.Commands;
using NHCM.Application.Recruitment.Validators;
using NHCM.Persistence;
using NHCM.Persistence.Identity.Infrastructure;
using NHCM.Persistence.Infrastructure.Identity;
using NHCM.WebUI.Resources;
using NHCM.Persistence.Infrastructure.Identity.Policies;
using NHCM.Persistence.Infrastructure.Services;
using NHCM.WebUI.Types;
using NHCM.WebUI.Utilities;

namespace NHCM.WebUI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        public Assembly SearchQueryValidator { get; private set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            // Registering custom services
            services.AddScoped<ICurrentUser, CurrentUser>();

            // 1 Antiforgery
            services.AddAntiforgery(options => options.HeaderName = "XSRF-TOKEN");

            // 2 Add DbContext
            services.AddDbContext<HCMContext>();
            services.AddSession(options => { options.Cookie.IsEssential = true; });

            // 3 Identity
            services.AddDbContext<HCMIdentityDbContext>();
            services.AddIdentity<HCMUser, HCMRole>(options => { options.User.RequireUniqueEmail = true; })
                .AddRoles<HCMRole>()
                .AddErrorDescriber<IdentityLocalizedErrorDescribers>()
                .AddEntityFrameworkStores<HCMIdentityDbContext>()
                .AddDefaultTokenProviders();

            services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequiredLength = 5;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireDigit = false;
                options.User.RequireUniqueEmail = true;

            });

            // 4 Add MediatR
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestPreProcessorBehavior<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestValidationBehavior<,>));
            services.AddMediatR(typeof(CreatePersonCommandHandler).GetTypeInfo().Assembly);

            // Add authorization Policy
            services.AddAuthorization(options =>
            {
                options.AddPolicy("ProfilerPolicy", policy => { policy.RequireRole("Profiler"); });
                options.AddPolicy("OrganizationTahskil", policy => { policy.RequireRole("OrganizationTashkil"); });
                options.AddPolicy("UserRegistrar",policy => { policy.RequireRole("UserRegistrar"); });
                options.AddPolicy("AuthenticatedPolicy", policy => { policy.RequireAuthenticatedUser(); });
                options.AddPolicy("SuperAdminPolicy", policy => { policy.RequireRole("SuperAdmin"); });
                options.AddPolicy("OrganizationAdminPolicy", policy => { policy.RequireRole("OrganizationAdmin"); });
                options.AddPolicy("FreshUserPolicy", policy => { policy.Requirements.Add(new NewlyRegisteredUsers(true)); });
                options.AddPolicy("SuperAdminPolicy", policy => { policy.Requirements.Add(new SuperAdminOnly()); });
            });
            services.AddScoped<IAuthorizationHandler, NewlyRegisteredUsersHandler>();



           
            services.AddSingleton<CultureLocalizer>(); 
            
            services.ConfigureRequestLocalization();
            // Add MVC, Fluent Validation, Localization, Routes
            services
                .AddMvc()
                .AddViewLocalization(o => o.ResourcesPath = "Resources")
                .AddModelBindingMessagesLocalizer(services) 
                .AddRazorPagesOptions(o => { o.Conventions.Add(new CultureTemplateRouteModelConvention()); })
                .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<CreatePersonCommandValidator>())
                .AddRazorPagesOptions
                (
                    options =>
                    {
                        // Configuring default pages routes for folders
                        options.Conventions.AddPageRoute("/Recruitment/Person", "/Recruitment");
                        options.Conventions.AddPageRoute("/Organogram/Plan", "/Organogram");
                        options.Conventions.AddPageRoute("/Employment/Selection", "/Employment");

                        
                        options.Conventions.AuthorizeFolder("/Security");
                        options.Conventions.AuthorizeFolder("/Recruitment", "ProfilerPolicy");
                        options.Conventions.AuthorizeFolder("/Shared");
                        options.Conventions.AuthorizeFolder("/Document");
                        options.Conventions.AuthorizePage("/index"); 
                        // Comment it in production
                         options.Conventions.AllowAnonymousToPage("/Security/Register");

                        options.AllowMappingHeadRequestsToGetHandler = true;
                    }
                )

                .SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.ConfigureApplicationCookie
              (
                  options =>
                  {
                      options.LoginPath = "/Security/Login";
                      options.AccessDeniedPath = "/Security/AccessDenied";
                  }
              );
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public async void Configure(IApplicationBuilder app, IHostingEnvironment env, IServiceProvider serviceProvider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else 
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
             
            // User type extension method used for providing configuration from config file in static methods.
            serviceProvider.SetConfigurationProvider(Configuration);

           
            app.UseRequestLocalization();
            app.UseHttpsRedirection();
            app.UseStaticFiles(); 
            app.UseAuthentication();
            app.UseCookiePolicy();
            app.UseSession();
            app.UseMvc();
        }

    }
}
