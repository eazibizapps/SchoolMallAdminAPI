using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using WebApiJwt.Entities;
using Microsoft.AspNetCore.Identity;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using AutoMapper;
using WebApiJwt.Models;
using System.Linq.Expressions;
using WebApiJwt.ViewModels;

namespace WebApiJwt
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Mapper.Initialize(cfg => {
                cfg.CreateMap<UserUpdateModel, ApplicationUser>();
                cfg.CreateMap<RegisterDto, ApplicationUser>();
                cfg.CreateMap<ResetUserPasswordViewModel, ApplicationUser>();
                cfg.CreateMap<SchoolContectUpdate, SchoolContacts>();
                cfg.CreateMap<SchoolsViewModel, Schools>();
                cfg.CreateMap<SchoolsViewModel, SchoolsStatus>();
                cfg.CreateMap<SupplierProductsViewModel, SupplierProducts>()
                  .ForMember(dest => dest.BrandCode, opt => opt.Condition(src => src.BrandCode != null))
                  .ForMember(dest => dest.CatalogueCode, opt => opt.Condition(src => src.CatalogueCode != null))
                  .ForMember(dest => dest.CategoryCode, opt => opt.Condition(src => src.CategoryCode != null))
                  .ForMember(dest => dest.Description, opt => opt.Condition(src => src.Description != null))
                  .ForMember(dest => dest.Image, opt => opt.Condition(src => src.Image != null))
                  .ForMember(dest => dest.ProductCode, opt => opt.Condition(src => src.ProductCode != null))
                  .ForMember(dest => dest.RetailPrice, opt => opt.Condition(src => src.RetailPrice != null))
                  .ForMember(dest => dest.SupplierPrice, opt => opt.Condition(src => src.SupplierPrice != null))
                  .ForMember(dest => dest.SuppliersId, opt => opt.Condition(src => src.SuppliersId != null))
                  .ForMember(dest => dest.UOMCode, opt => opt.Condition(src => src.UOMCode != null))
                   .ForMember(dest => dest.ProductId, opt => opt.Condition(src => src.ProductId != null))
                  .ForMember(dest => dest.UserID, opt => opt.Condition(src => src.UserID != null));

                cfg.CreateMap<SchoolProductsViewModel, SchoolProducts>();
                cfg.CreateMap<SupportTasksViewModel, SupportTasks>()
                .ForMember(dest => dest.Active, opt => opt.Condition(src => src.Active != null))
                .ForMember(dest => dest.TaskName, opt => opt.Condition(src => src.TaskName != null))
                .ForMember(dest => dest.Comments, opt => opt.Condition(src => src.Comments != null));
                    
                cfg.CreateMap<SchoolProductsLinkViewModel, SchoolProductsLink>();
                cfg.CreateMap<ScoolGradesListViewModel, ScoolGradesList>();
                cfg.CreateMap<SuppliersViewModel, Suppliers>();
                cfg.CreateMap<SchoolPressKitViewModel, SchoolPressKit>();
                



            });


            Configuration = configuration;
        }

    


        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // ===== Add our DbContext ========

            services.AddDbContext<ApplicationDbContext>();
            // ===== Add Identity ========
            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            //Mapper
           // services.AddScoped<IDataService, DataService>();
            services.AddTransient<IPeriods, Periods>();
            services.AddTransient<IPrintLanguages, PrintLanguages>();

            // ===== Add Jwt Authentication ========
            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear(); // => remove default claims
            services
                .AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

                })
                .AddJwtBearer(cfg =>
                {
                    cfg.RequireHttpsMetadata = false;
                    cfg.SaveToken = true;
                    cfg.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidIssuer = Configuration["JwtIssuer"],
                        ValidAudience = Configuration["JwtIssuer"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["JwtKey"])),
                        ClockSkew = TimeSpan.Zero // remove delay of token when expire
                    };
                });

            //services.AddCors(opt => opt.AddPolicy("CorsPolicy", al => al.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin()));

            services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", p =>
                {
                    p.AllowAnyOrigin()
                    .AllowAnyHeader()
                    .AllowAnyMethod();
                });
            });

            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ApplicationDbContext dbContext)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

        


            // ===== Use Authentication ======
            app.UseAuthentication();
            //app.UseCors("CorsPolicy");
            app.UseCors("AllowAll");

            app.UseMvc();

            dbContext.Database.EnsureCreated();

        }
    }
}
