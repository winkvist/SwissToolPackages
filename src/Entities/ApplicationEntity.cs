using Azure;
using Azure.Data.Tables;

namespace SwissToolPackages.Entities
{
    using System;

    public class ApplicationEntity : ITableEntity
    {
        public string ApplicationType { get; set; }
        public Guid Identifier { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string PartitionKey { get; set; }
        public string RowKey { get; set; }
        public DateTimeOffset? Timestamp { get; set; }
        public ETag ETag { get; set; }
    }
}