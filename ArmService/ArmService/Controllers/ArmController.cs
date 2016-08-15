using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using ArmService.Models;
using Microsoft.Azure.Management.ResourceManager.Models;
using Swashbuckle.Swagger.Annotations;

namespace ArmService.Controllers
{
    public class ArmController : ApiController
    {
        // GET api/values
        [SwaggerOperation("GetSubscriptions")]
        public async Task<IEnumerable<Subscription>> GetSubscriptions(string clientId, string clientSecret, string tenantId)
        {
            ArmClient client = new ArmClient(clientId, clientSecret, tenantId);
            return await client.ListSubscriptions();
        }
    }
}
