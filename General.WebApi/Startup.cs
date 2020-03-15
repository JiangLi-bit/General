using System.Text;
using General.Core;
using General.Core.Extensions;
using General.Entity;
using General.IService;
using General.Service;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using General.DTO;
using Microsoft.IdentityModel.Tokens;

namespace General.WebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            //添加响应压缩
            services.AddResponseCompression();
            //数据库连接
            services.AddDbContextPool<GeneralDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddSession();
            services.AddCors();

            var token = Configuration.GetSection("tokenConfig").Get<TokenDTO>();
            //添加 JWT 认证信息
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                //Token Validation Parameters
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    //获取或设置要使用的Microsoft.IdentityModel.Tokens.SecurityKey用于签名验证。
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(token.Secret)),

                    //获取或设置一个System.String，它表示将使用的有效发行者检查代币的发行者。
                    ValidateIssuer = false,
                    ValidIssuer = token.Issuer,

                    //获取或设置一个字符串，该字符串表示将用于检查的有效受众反对令牌的观众。
                    ValidateAudience = false,
                    ValidAudience = token.Audience,
                };
            });
            services.AddScoped<IAuthenticateService, AuthenticateService>();

            // 程序集依赖注入
            services.AddAssembly("General.IService");
            services.AddAssembly("General.Service");
            //泛型注入到DI里面
            services.AddScoped(typeof(IRepositoryBase<>), typeof(RepositoryBase<>));
            //services.AddScoped<IWorkContext, WorkContext>();
            //services.AddScoped<ILoginAuthService, LoginAuthService>();
            services.AddSingleton<IMemoryCache, MemoryCache>();
            //services.AddSingleton<IRegisterApplicationService, RegisterApplicationService>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();
            //授权认证
            app.UseAuthentication();
            app.UseSession();

            app.UseMvc();

            //app.UseMvc(routes =>
            //{
            //    routes.MapRoute(
            //      name: "areas",
            //      template: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
            //    );
            //});
        }
    }
}
