using System;
using System.IO;
using Cuemon;
using Microsoft.Extensions.FileProviders;

namespace WBPA.WellKnownLocations
{
    /// <summary>
    /// Options for serving static files from the .well-known directory.
    /// </summary>
    public class WellKnownLocationsOptions
    {
        /// <summary>
        /// The name of the directory of the well-known location as described in RFC 5785.
        /// </summary>
        public const string DirectoryName = ".well-known";

        /// <summary>
        /// Initializes a new instance of the <see cref="WellKnownLocationsOptions"/> class.
        /// </summary>
        /// <remarks>
        /// The following table shows the initial property values for an instance of <see cref="WellKnownLocationsOptions"/>.
        /// <list type="table">
        ///     <listheader>
        ///         <term>Property</term>
        ///         <description>Initial Value</description>
        ///     </listheader>
        ///     <item>
        ///         <term><see cref="FileProvider"/></term>
        ///         <description><c>null</c></description>
        ///     </item>
        ///     <item>
        ///         <term><see cref="DirectoryWriter"/></term>
        ///         <description><c>path =&gt;
        ///             {
        ///                 if (!Directory.Exists(path)) { Directory.CreateDirectory(path); }
        ///             }</c></description>
        ///     </item>
        /// </list>
        /// </remarks>
        public WellKnownLocationsOptions()
        {
            RequestPath = "/{0}".FormatWith(DirectoryName);
            DirectoryWriter = path =>
            {
                if (!Directory.Exists(path)) { Directory.CreateDirectory(path); }
            };
        }

        /// <summary>
        /// Gets the request path that maps to the .well-known directory.
        /// </summary>
        /// <value>The request path that maps to the .well-known directory.</value>
        public string RequestPath { get; }

        /// <summary>
        /// Gets or sets the file provider used to locate the .well-known resource.
        /// </summary>
        /// <value>The file provider used to locate the .well-known resource.</value>
        public IFileProvider FileProvider { get; set; }

        /// <summary>
        /// Gets or sets the delegate that writes a .well-known directory should this not exists.
        /// </summary>
        /// <value>The delegate that writes a .well-known directory should this not exists.</value>
        public Action<string> DirectoryWriter { get; set; }
    }
}