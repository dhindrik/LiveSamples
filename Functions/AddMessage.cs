using Newtonsoft.Json;
using System.Threading.Tasks;
using Messages;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Extensions.SignalRService;

namespace Functions
{
    public static class AddMessage
    {
        [FunctionName("AddMessage")]
        public async static Task Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post")] object message,
            [SignalR(HubName = "live")] IAsyncCollector<SignalRMessage> signalRMessages)
        {
            var json = message.ToString();
            var msg = JsonConvert.DeserializeObject<Message>(json);

            await signalRMessages.AddAsync(
                new SignalRMessage
                {
                    Target = msg.TypeInfo,
                    Arguments = new[] { message }
                });
        }
    }
}
