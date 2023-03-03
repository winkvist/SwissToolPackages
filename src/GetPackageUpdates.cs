using System.Net;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;
using SwissToolPackages.Infrastructure;
using SwissToolPackages.Models;

namespace SwissToolPackages
{
    public class GetPackageUpdates
    {
        private readonly ILogger logger;

        public GetPackageUpdates(ILoggerFactory loggerFactory)
        {
            this.logger = loggerFactory.CreateLogger<GetPackageUpdates>();
        }

        [Function("GetPackageUpdates")]
        public async Task<HttpResponseData> Run([HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "fetch")] HttpRequestData request, bool? includePreReleases)
        {
            this.logger.LogInformation("GetPackageUpdates HTTP trigger function processed a request.");

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
            var detailedPackageVersions = await repository.GetApplicationUpdatesAsync(packageVersions, includePreReleases ?? false);

            var response = request.CreateResponse(HttpStatusCode.OK);
            await response.WriteAsJsonAsync(detailedPackageVersions);

            return response;
        }
    }
}
