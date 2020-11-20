using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Azure.WebJobs.Extensions.SignalRService;

namespace Functions
{
    public static class GetInfo
    {
        [FunctionName("GetInfo")]
        public static SignalRConnectionInfo Run(
            [HttpTrigger(AuthorizationLevel.Anonymous)] HttpRequest req,
            [SignalRConnectionInfo(HubName = "live")] SignalRConnectionInfo connectionInfo)
        {
            return connectionInfo;
        }
    }
}
