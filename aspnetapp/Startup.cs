
using Microsoft.OpenApi.Models;

namespace aspnetapp
{
    /// <summary>
    /// 
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="configuration"></param>
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        /// <summary>
        /// 
        /// </summary>
        public IConfiguration Configuration { get; }
        /// <summary>
        ///  This method gets called by the runtime. Use this method to add services to the container.
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers((config) =>
            {
            })
            .AddJsonOptions(config =>
            {
                config.JsonSerializerOptions.PropertyNamingPolicy = null;//原样输出


            })
            .AddControllersAsServices();

            //swagger
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "aspnetapp", Version = "v1" });
                var file = Path.Combine(AppContext.BaseDirectory, "aspnetapp.xml");  // xml文档绝对路径
                var path = Path.Combine(AppContext.BaseDirectory, file); // xml文档绝对路径
                c.IncludeXmlComments(path, true); // true : 显示控制器层注释
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "token授权 直接在下框中输入Bearer {token}（注意两者之间是一个空格）",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference()
                            {
                                Id = "Bearer",
                                Type = ReferenceType.SecurityScheme
                            }
                        }, Array.Empty<string>()
                    }
                });
            });

        }
        /// <summary>
        /// This method gets called by the runtime. Use this method to configure the HTTP request pipeline
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "aspnetapp v1"));
            }

            app.UseStaticFiles();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
