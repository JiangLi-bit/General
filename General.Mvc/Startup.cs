using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using General.Entity;
using Microsoft.EntityFrameworkCore;
using General.Core;
using General.Core.Extensions;
using General.Framework;
using General.Framework.Security;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Memory;
using General.Framework.Menu;
using Autofac;
using System;
using Autofac.Extensions.DependencyInjection;
using Autofac.Configuration;

namespace General.Mvc
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        #region 程序依赖注入
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            //数据库连接
            services.AddDbContextPool<GeneralDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));


            //添加认证信息
            services.AddAuthentication(o =>
            {
                o.DefaultAuthenticateScheme = CookieAdminAuthInfo.AuthenticationScheme;
                o.DefaultChallengeScheme = CookieAdminAuthInfo.AuthenticationScheme;
            })
            .AddCookie(CookieAdminAuthInfo.AuthenticationScheme, o =>
            {
                o.Cookie.Name = CookieAdminAuthInfo.AuthenticationScheme;
                o.LoginPath = new PathString("/admin/login");
                o.LogoutPath = new PathString("/admin/login");
            });

            services.AddSession();

            //程序集依赖注入
            services.AddAssembly("General.IService");
            services.AddAssembly("General.Service");
            //泛型注入到DI里面
            services.AddScoped(typeof(IRepositoryBase<>), typeof(RepositoryBase<>));
            services.AddScoped<IWorkContext, WorkContext>();
            services.AddScoped<ILoginAuthService, LoginAuthService>();
            services.AddSingleton<IMemoryCache, MemoryCache>();
            services.AddSingleton<IRegisterApplicationService, RegisterApplicationService>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            EnginContext.Initialize(new GeneralEngine(services.BuildServiceProvider()));
        }
        #endregion

        #region Autofac注入

        //public Startup(IHostingEnvironment env)
        //{
        //    Configuration = new ConfigurationBuilder()
        //   .SetBasePath(env.ContentRootPath)
        //   .AddJsonFile("appsettings.json").Build();
        //}
        //public IContainer Container { get; private set; }

        //public IServiceProvider ConfigureServices(IServiceCollection services)
        //{
        //    services.AddMvc();

        //    //数据库连接
        //    services.AddDbContextPool<GeneralDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

        //    //添加认证信息
        //    services.AddAuthentication(o =>
        //    {
        //        o.DefaultAuthenticateScheme = CookieAdminAuthInfo.AuthenticationScheme;
        //        o.DefaultChallengeScheme = CookieAdminAuthInfo.AuthenticationScheme;
        //    })
        //    .AddCookie(CookieAdminAuthInfo.AuthenticationScheme, o =>
        //    {
        //        o.LoginPath = "/admin/login";
        //    });

        //    services.AddSession();

        //    //IOC Autofac注入
        //    var builder = new ContainerBuilder();
        //    builder.Populate(services);

        //    //注册应用服务
        //    var iservice = System.Reflection.Assembly.Load("General.IService");
        //    builder.RegisterAssemblyTypes(iservice).AsImplementedInterfaces();
        //    var service = System.Reflection.Assembly.Load("General.Service");
        //    builder.RegisterAssemblyTypes(service).AsImplementedInterfaces();

        //    builder.RegisterGeneric(typeof(RepositoryBase<>)).As(typeof(IRepositoryBase<>)).InstancePerLifetimeScope();
        //    builder.RegisterType<WorkContext>().As<IWorkContext>().InstancePerLifetimeScope();
        //    builder.RegisterType<LoginAuthService>().As<ILoginAuthService>().InstancePerLifetimeScope();
        //    builder.RegisterType<MemoryCache>().As<IMemoryCache>().SingleInstance();
        //    builder.RegisterType<RegisterApplicationService>().As<IRegisterApplicationService>().SingleInstance();
        //    builder.RegisterType<HttpContextAccessor>().As<IHttpContextAccessor>().SingleInstance();

        //    EnginContext.Initialize(new GeneralEngine(services.BuildServiceProvider()));

        //    this.Container = builder.Build();
        //    return new AutofacServiceProvider(this.Container);
        //}
        #endregion

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();
            //授权认证
            app.UseAuthentication();
            app.UseSession();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                  name: "areas",
                  template: "{area:exists}/{controller=admin}/{action=login}/{id?}"
                );
            });
            //初始化菜单
            EnginContext.Current.Resolve<IRegisterApplicationService>().initRegister();
        }
    }
}
