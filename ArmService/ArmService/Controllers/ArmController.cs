using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using ArmService.Models;
using Microsoft.Azure.Management.ResourceManager.Models;
using Swashbuckle.Swagger.Annotations;

namespace ArmService.Controllers
{
    [BasicAuthentication]
    public class ArmController : ApiController
    {
        // GET api/values
        [SwaggerOperation("GetSubscriptions")]
        public async Task<IEnumerable<Subscription>> GetSubscriptions(string clientId, string clientSecret, string tenantId)
        {
            ArmClient client = new ArmClient(clientId, clientSecret, tenantId);
            return await client.ListSubscriptions();
        }

        [SwaggerOperation("GetResourceGroups")]
        public async Task<IEnumerable<ResourceGroup>> GetResourceGroups(string clientId, string clientSecret, string tenantId, string subscriptionId)
        {
            ArmClient client = new ArmClient(clientId, clientSecret, tenantId);
            return await client.ListResourceGroups(subscriptionId);
        }
    }
}
