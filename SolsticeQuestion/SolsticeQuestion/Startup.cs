using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore;
using SolsticeQuestion.Models;

namespace SolsticeQuestion
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
            //string Connection = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=ContactsDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            
            ////Azure has been configured with the following COnnection
            string Connection = @"Data Source=tcp:solsticequestion20190609121233dbserver.database.windows.net,1433;Initial Catalog=SolsticeQuestion20190609121233_db;User Id=sqladmin@solsticequestion20190609121233dbserver;Password=Adi47123";
            services.AddDbContext<ContactsDBContext>(opt =>opt.UseSqlServer(Connection));
            
            //// To Test Locally
            //services.AddDbContext<ContactsDBContext>(opt =>opt.UseInMemoryDatabase("ContactsDB"));
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMvc(
                routes =>
                {
                    routes.MapRoute("default", "{controller=Values}/{action=ActionResult}/{id?}");
                }
                );
        }
    }
}
