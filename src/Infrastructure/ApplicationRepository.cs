namespace SwissToolPackages.Infrastructure
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Xml.Linq;

    using Azure;
    using Azure.Data.Tables;

    using SwissToolPackages.Entities;
    using SwissToolPackages.Models;

    public class ApplicationRepository
    {
        private readonly TableServiceClient tableServiceClient;
        private readonly TableClient applicationPackageTableClient;
        private readonly TableClient applicationTableClient;

        public ApplicationRepository(string storageConnectionString)
        {
            tableServiceClient = new TableServiceClient(storageConnectionString);

            applicationPackageTableClient = tableServiceClient.GetTableClient("ApplicationPackage");
            applicationTableClient = tableServiceClient.GetTableClient("Application");
        }

        public async Task<bool> CheckApplicationUpdatesAsync(List<PackageVersion> packageVersions, bool includePreReleases = false)
        {
            foreach (var packageVersion in packageVersions)
            {
                var identifier = packageVersion.Identifier.ToString();
                var version = packageVersion.Version.ToString();

                var packageUpdates = applicationPackageTableClient.QueryAsync<ApplicationPackageEntity>(ap =>
                    ap.PartitionKey == identifier &&
                    string.Compare(ap.RowKey, version, StringComparison.Ordinal) > 0
                    && (includePreReleases || !includePreReleases && !ap.PreRelease));

                await foreach (var _ in packageUpdates)
                {
                    return true;
                }
            }

            return false;
        }

        public async Task<List<DetailedPackageVersion>> GetApplicationUpdatesAsync(List<PackageVersion> packageVersions, bool includePreReleases = false)
        {
            var applicationNameDictionary = new Dictionary<string, string>();

            var applications = applicationTableClient.QueryAsync<ApplicationEntity>();

            await foreach (var application in applications)
            {
                var identifier = application.PartitionKey;
                var name = application.Name;

                applicationNameDictionary.Add(identifier, name);
            }

            var detailedPackageVersions = new List<DetailedPackageVersion>();

            foreach (var packageVersion in packageVersions)
            {
                var identifier = packageVersion.Identifier.ToString();
                var version = packageVersion.Version.ToString();

                var applicationPackages = applicationPackageTableClient.QueryAsync<ApplicationPackageEntity>(ap =>
                    ap.PartitionKey == identifier &&
                    string.Compare(ap.RowKey, version, StringComparison.Ordinal) > 0
                    && (includePreReleases || !includePreReleases && !ap.PreRelease));

                await foreach (var applicationPackage in applicationPackages)
                {
                    var updateIdentifier = applicationPackage.PartitionKey;

                    var changes = XDocument.Parse(applicationPackage.Changes)
                        .Descendants("change")
                        .Select(change => change.Value)
                        .ToList();

                    var detailedPackageVersion = new DetailedPackageVersion
                    {
                        Version = new Models.Version(applicationPackage.RowKey),
                        Identifier = Guid.Parse(updateIdentifier),
                        Changes = changes,
                        DownloadFileSize = applicationPackage.DownloadFileSize,
                        DownloadFilename = applicationPackage.DownloadFileName,
                        DownloadUrl = applicationPackage.DownloadUrl,
                        Name = applicationNameDictionary[updateIdentifier],
                        ReleasedDate = applicationPackage.ReleaseDate
                    };

                    detailedPackageVersions.Add(detailedPackageVersion);
                }
            }

            return detailedPackageVersions.OrderBy(p => p.Identifier).ThenBy(p => p.Version).ToList();
        }
    }
}
