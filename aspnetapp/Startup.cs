
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

            //            //注入freesql
            //            services.AddSingleton(p =>
            //            {
            //                var freesql = new FreeSqlBuilder()
            //                .UseConnectionString(DataType.MySql, Configuration.GetSection("DbConnectionString").Value)
            //                .Build();

            //#if DEBUG ||DEBUG1
            //                freesql.Aop.CurdAfter += (sender, e) =>
            //                {
            //                    Debug.WriteLine(e.Sql);
            //                };
            //#endif
            //                return freesql;
            //            });
            //            services.AddScoped<UnitOfWorkManager>();
            //services.AddFreeRepository(null, typeof(BaseService).Assembly);

            //注入缓存

#if DEBUG
            //services.AddSingleton<ICache,NormalCache>();
#else
            services.AddSingleton<ICache>(new RedisCache(Configuration.GetSection("RedisConnectionString").Value));
#endif

            services.AddControllers((config) =>
            {
                //config.Filters.Add(typeof(ActionFilter));
                //config.Filters.Add(typeof(ExceptionFilter));
            })
            .AddJsonOptions(config =>
            {
                config.JsonSerializerOptions.PropertyNamingPolicy = null;//原样输出

                //config.JsonSerializerOptions.Converters.Add(new DateTimeJsonConverter());

            })
            .AddControllersAsServices();

            //swagger
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "FineExTech.TMS.WebApi", Version = "v1" });
                var commentFiles = new FileInfo(typeof(Startup).Assembly.Location)
                   .Directory.GetFiles("*.xml");
                foreach (var file in commentFiles)
                {
                    c.IncludeXmlComments(file.FullName, true);
                }
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
            }

            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "FineExTech.TMS.WebApi v1"));
            app.UseStaticFiles();
            //string filePath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Image");
            //if (!System.IO.Directory.Exists(filePath))
            //{
            //    System.IO.Directory.CreateDirectory(filePath);
            //}
            //app.UseStaticFiles(new StaticFileOptions
            //{
            //    FileProvider = new Microsoft.Extensions.FileProviders.PhysicalFileProvider(filePath),
            //    RequestPath = "/Image"
            //});

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
