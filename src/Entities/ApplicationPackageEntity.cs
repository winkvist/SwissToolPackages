using Azure;
using Azure.Data.Tables;

namespace SwissToolPackages.Entities
{
    using System;

    public class ApplicationPackageEntity : ITableEntity
    {
        public string Changes { get; set; }
        public string DownloadFileName { get; set; }
        public long DownloadFileSize { get; set; }
        public string DownloadUrl { get; set; }
        public DateTime ReleaseDate { get; set; }
        public bool PreRelease { get; set; }
        public string PartitionKey { get; set; }
        public string RowKey { get; set; }
        public DateTimeOffset? Timestamp { get; set; }
        public ETag ETag { get; set; }
    }
}