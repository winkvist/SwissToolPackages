namespace SwissToolPackages.Models
{
    using System.Collections.Generic;

    /// <summary>
    /// The version changes.
    /// </summary>
    public class VersionChanges
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="VersionChanges"/> class.
        /// </summary>
        public VersionChanges()
        {
            this.Notes = new List<string>();
        }

        /// <summary>
        /// Gets or sets the version.
        /// </summary>
        /// <value>
        /// The version.
        /// </value>
        public Version Version { get; set; }

        /// <summary>
        /// Gets or sets the notes.
        /// </summary>
        /// <value>
        /// The notes.
        /// </value>
        public List<string> Notes { get; set; }
    }
}