using System.Net;
using System.Text.Json;
using Azure.Core.Serialization;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using SwissToolPackages.Infrastructure;
using SwissToolPackages.Models;

namespace SwissToolPackages
{
    public class CheckPackageUpdates
    {
        private readonly ILogger logger;

        public CheckPackageUpdates(ILoggerFactory loggerFactory)
        {
            this.logger = loggerFactory.CreateLogger<CheckPackageUpdates>();
        }

        [Function("CheckPackageUpdates")]
        public async Task<HttpResponseData> Run([HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "check")] HttpRequestData request, bool? includePreReleases)
        {
            this.logger.LogInformation("CheckPackageUpdates HTTP trigger function processed a request.");
            
            var packageVersions = await request.ReadFromJsonAsync<List<PackageVersion>>();
            if (packageVersions == null)
            {
                throw new ArgumentNullException("Invalid API method call");
            }

            var storageConnectionString = Environment.GetEnvironmentVariable("CONNECTION_STRING");
            if (storageConnectionString == null)
            {
                throw new ArgumentNullException("Invalid connection string");
            }

            var repository = new ApplicationRepository(storageConnectionString);
            var detailedPackageVersions = await repository.CheckApplicationUpdatesAsync(packageVersions, includePreReleases ?? false);

            var response = request.CreateResponse(HttpStatusCode.OK);
            await response.WriteAsJsonAsync(detailedPackageVersions);

            return response;
        }
    }
}
