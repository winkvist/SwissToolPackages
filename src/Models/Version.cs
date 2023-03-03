namespace SwissToolPackages.Models
{
    using System;

    /// <summary>
    /// The version.
    /// </summary>
    /// <seealso cref="System.IComparable{SwissApps.Web.Models.Version}" />
    public class Version : IComparable<Version>
    {
        /// <summary>
        /// Gets or sets the major.
        /// </summary>
        /// <value>
        /// The major.
        /// </value>
        public int Major { get; set; }

        /// <summary>
        /// Gets or sets the minor.
        /// </summary>
        /// <value>
        /// The minor.
        /// </value>
        public int Minor { get; set; }

        /// <summary>
        /// Gets or sets the build.
        /// </summary>
        /// <value>
        /// The build.
        /// </value>
        public int Build { get; set; }

        /// <summary>
        /// Gets or sets the revision.
        /// </summary>
        /// <value>
        /// The revision.
        /// </value>
        public int Revision { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Version"/> class.
        /// </summary>
        public Version()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Version"/> class.
        /// </summary>
        /// <param name="version">The version.</param>
        public Version(string version)
        {
            if (string.IsNullOrEmpty(version))
            {
                return;
            }

            var parts = version.Split('.');

            this.Major = int.Parse(parts[0].Trim());
            this.Minor = int.Parse(parts[1].Trim());
            this.Build = int.Parse(parts[2].Trim());
            this.Revision = int.Parse(parts[3].Trim());
        }

        /// <summary>
        /// Compares the current instance with another object of the same type and returns an integer that indicates whether the current instance precedes, follows, or occurs in the same position in the sort order as the other object.
        /// </summary>
        /// <param name="other">An object to compare with this instance.</param>
        /// <returns>
        /// A value that indicates the relative order of the objects being compared. The return value has these meanings: Value Meaning Less than zero This instance precedes <paramref name="other" /> in the sort order.  Zero This instance occurs in the same position in the sort order as <paramref name="other" />. Greater than zero This instance follows <paramref name="other" /> in the sort order.
        /// </returns>
        public int CompareTo(Version other)
        {
            var thisVer = new System.Version(this.Major, this.Minor, this.Build, this.Revision);
            var compareVer = new System.Version(other.Major, other.Minor, other.Build, other.Revision);

            return thisVer.CompareTo(compareVer);
        }

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return $"{this.Major}.{this.Minor}.{this.Build}.{this.Revision}";
        }

        /// <summary>
        /// Implements the operator &lt;.
        /// </summary>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        public static bool operator <(Version x, Version y)
        {
            return new System.Version(x.ToString()) < new System.Version(y.ToString());
        }

        /// <summary>
        /// Implements the operator &gt;.
        /// </summary>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        public static bool operator >(Version x, Version y)
        {
            return new System.Version(x.ToString()) > new System.Version(y.ToString());
        }
    }
}