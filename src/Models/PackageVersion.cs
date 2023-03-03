namespace SwissToolPackages.Models
{
    using System;

    /// <summary>
    /// The package version.
    /// </summary>
    public class PackageVersion
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public Guid Identifier { get; set; }

        /// <summary>
        /// Gets or sets the version.
        /// </summary>
        /// <value>
        /// The version.
        /// </value>
        public Version Version { get; set; }
    }
}