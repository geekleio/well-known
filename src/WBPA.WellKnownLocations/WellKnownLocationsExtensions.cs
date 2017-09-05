using System;
using System.IO;
using Cuemon;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;

namespace WBPA.WellKnownLocations
{
    /// <summary>
    /// Extension methods for the <see cref="IApplicationBuilder"/> interface.
    /// </summary>
    public static class WellKnownLocationsExtensions
    {
        /// <summary>
        /// Enables static file serving from a .well-known location with the given options.
        /// </summary>
        /// <param name="builder">The <see cref="IApplicationBuilder"/> instance.</param>
        /// <param name="setup">The <see cref="WellKnownLocationsOptions"/> which need to be configured.</param>
        /// <returns>The <see cref="IApplicationBuilder"/> instance.</returns>
        public static IApplicationBuilder UseWellKnownLocations(this IApplicationBuilder builder, Action<WellKnownLocationsOptions> setup = null)
        {
            var options = setup.ConfigureOptions();
            var he = builder.ApplicationServices.GetService<IHostingEnvironment>();
            var root = he?.ContentRootPath ?? Directory.GetCurrentDirectory();
            var wellKnowLocationPath = Path.Combine(root, WellKnownLocationsOptions.DirectoryName);
            options.DirectoryWriter?.Invoke(wellKnowLocationPath);
            return builder.UseStaticFiles(new StaticFileOptions()
            {
                ServeUnknownFileTypes = true,
                FileProvider = options.FileProvider ?? new PhysicalFileProvider(wellKnowLocationPath),
                RequestPath = options.RequestPath
            });
        }
    }
}