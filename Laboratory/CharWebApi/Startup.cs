using CharDB;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Server.IISIntegration;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Linq;
using System.Text;

namespace Test.CharWebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddControllers();

            services.AddDbContext<TCDBCentext>(
                options => options.UseSqlServer(configuration.GetConnectionString("TCDBCentext")));
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IConfiguration config)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                //系统参数
                app.Run(async (context) =>
                {
                    var sb = new StringBuilder();
                    var nl = System.Environment.NewLine;
                    var rule = string.Concat(nl, new string('-', 40), nl);
                    var authSchemeProvider = app.ApplicationServices
                        .GetRequiredService<IAuthenticationSchemeProvider>();

                    sb.Append($"Request{rule}");
                    sb.Append($"{DateTimeOffset.Now}{nl}");
                    sb.Append($"{context.Request.Method} {context.Request.Path}{nl}");
                    sb.Append($"Scheme: {context.Request.Scheme}{nl}");
                    sb.Append($"Host: {context.Request.Headers["Host"]}{nl}");
                    sb.Append($"PathBase: {context.Request.PathBase.Value}{nl}");
                    sb.Append($"Path: {context.Request.Path.Value}{nl}");
                    sb.Append($"Query: {context.Request.QueryString.Value}{nl}{nl}");

                    sb.Append($"Connection{rule}");
                    sb.Append($"RemoteIp: {context.Connection.RemoteIpAddress}{nl}");
                    sb.Append($"RemotePort: {context.Connection.RemotePort}{nl}");
                    sb.Append($"LocalIp: {context.Connection.LocalIpAddress}{nl}");
                    sb.Append($"LocalPort: {context.Connection.LocalPort}{nl}");
                    sb.Append($"ClientCert: {context.Connection.ClientCertificate}{nl}{nl}");

                    sb.Append($"Identity{rule}");
                    sb.Append($"User: {context.User.Identity.Name}{nl}");
                    var scheme = await authSchemeProvider
                        .GetSchemeAsync(IISDefaults.AuthenticationScheme);
                    sb.Append($"DisplayName: {scheme?.DisplayName}{nl}{nl}");

                    sb.Append($"Headers{rule}");
                    foreach (var header in context.Request.Headers)
                    {
                        sb.Append($"{header.Key}: {header.Value}{nl}");
                    }
                    sb.Append(nl);

                    sb.Append($"Websockets{rule}");
                    if (context.Features.Get<IHttpUpgradeFeature>() != null)
                    {
                        sb.Append($"Status: Enabled{nl}{nl}");
                    }
                    else
                    {
                        sb.Append($"Status: Disabled{nl}{nl}");
                    }

                    sb.Append($"Configuration{rule}");
                    foreach (var pair in config.AsEnumerable())
                    {
                        sb.Append($"{pair.Key}: {pair.Value}{nl}");
                    }
                    sb.Append(nl);

                    sb.Append($"Environment Variables{rule}");
                    var vars = System.Environment.GetEnvironmentVariables();
                    foreach (var key in vars.Keys.Cast<string>().OrderBy(key => key,
                        StringComparer.OrdinalIgnoreCase))
                    {
                        var value = vars[key];
                        sb.Append($"{key}: {value}{nl}");
                    }

                    context.Response.ContentType = "text/plain";
                    await context.Response.WriteAsync(sb.ToString());
                });
            }

            app.UseHttpsRedirection();
            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
