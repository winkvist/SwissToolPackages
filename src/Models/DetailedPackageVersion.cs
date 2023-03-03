namespace SwissToolPackages.Models
{
    using System;
    using System.Collections.Generic;

    public class DetailedPackageVersion : PackageVersion
    {
        /// <summary>
        /// Gets or sets the name of the package.
        /// </summary>
        /// <value>The name of the package.</value>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the download URL.
        /// </summary>
        /// <value>The download URL.</value>
        public string DownloadUrl { get; set; }

        /// <summary>
        /// Gets or sets the download filename.
        /// </summary>
        /// <value>
        /// The download filename.
        /// </value>
        public string DownloadFilename { get; set; }

        /// <summary>
        /// Gets or sets the size of the download file.
        /// </summary>
        /// <value>The size of the download file.</value>
        public long DownloadFileSize { get; set; }

        /// <summary>
        /// Gets or sets the released date.
        /// </summary>
        /// <value>The released date.</value>
        public DateTime ReleasedDate { get; set; }

        /// <summary>
        /// Gets or sets the changes.
        /// </summary>
        /// <value>The changes.</value>
        public List<string> Changes { get; set; }
    }
}