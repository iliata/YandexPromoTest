using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;
using YaDemo.Dal;
using YaDemo.Dal.Interface;
using YaDemo.Model;

namespace YaDemo
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
            services.AddMvc();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "Promo API", Version = "v1" });
            });

            services.Configure<MongoSettings>(opt =>
            {
                opt.ConnectionString = Configuration.GetSection("MongoConnection:ConnectionString").Value;
                opt.Database = Configuration.GetSection("MongoConnection:Database").Value;
            });

            services.Configure<RedisSettings>(opt =>
            {
                opt.Host = Configuration.GetSection("RedisConnection:Host").Value;
            });

            services.AddTransient<IPromoCodeRepository, PromoCodeRepository>();
            services.AddTransient<IRequestCounterStore, RequestCounterStore>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
            }
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Promo API V1");
            });
            app.UseMvc();
        }
    }
}
