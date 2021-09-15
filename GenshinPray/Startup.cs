using GenshinPray.Attribute;
using GenshinPray.Common;
using GenshinPray.Dao;
using GenshinPray.Service;
using GenshinPray.Util;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using SqlSugar.IOC;
using System;
using System.IO;
using System.Reflection;
using System.Text;

namespace GenshinPray
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            loadSiteConfig();

            LogHelper.ConfigureLog();//log4net
            LogHelper.Info($"读取配置文件...");
            LogHelper.Info($"初始化数据库...");
            services.AddSqlSugar(new IocConfig()//注入Sqlsuger
            {
                DbType = IocDbType.MySql,
                ConnectionString = SiteConfig.ConnectionString,
                IsAutoCloseConnection = true//自动释放
            });
            new DBClient().CreateDB();
            LogHelper.Info($"数据库初始化完毕...");

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "接口文档", Description = "api 文档", Version = "v1" });
                c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, $"{Assembly.GetExecutingAssembly().GetName().Name}.xml"));// 为 Swagger 设置xml文档注释路径
            });

            //jwt验证
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,//是否验证Issuer
                    ValidateAudience = true,//是否验证Audience
                    ValidateLifetime = true,//是否验证失效时间
                    ClockSkew = TimeSpan.FromSeconds(30),
                    ValidateIssuerSigningKey = true,//是否验证SecurityKey
                    ValidIssuer = SiteConfig.JWTIssuer,//Issuer，这两项和前面签发jwt的设置一致
                    ValidAudience = SiteConfig.JWTAudience,//Audience
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SiteConfig.JWTSecurityKey))//拿到SecurityKey
                };
            });

            //services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Latest);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();
            app.UseSwaggerUI(c => {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "GenshinPray v1");
                c.RoutePrefix = string.Empty;//设置根节点访问
            });

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseAuthentication();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            //加载默认蛋池数据到内存
            new GoodsService().LoadYSPrayItem(); 
        }

        //将配置文件中的信息加载到内存
        private void loadSiteConfig()
        {
            SiteConfig.ConnectionString = Configuration.GetSection("ConnectionString").Value;
            SiteConfig.PrayImgSavePath = Configuration.GetSection("PrayImgSavePath").Value;
            SiteConfig.PrayMaterialSavePath = Configuration.GetSection("PrayMaterialSavePath").Value;
        }

    }
}
