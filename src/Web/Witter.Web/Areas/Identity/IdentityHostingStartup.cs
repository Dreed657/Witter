using Microsoft.AspNetCore.Hosting;

[assembly: HostingStartup(typeof(Witter.Web.Areas.Identity.IdentityHostingStartup))]

namespace Witter.Web.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) =>
            {
            });
        }
    }
}