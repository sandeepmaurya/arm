using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Azure.Management.ResourceManager;
using Microsoft.Azure.Management.ResourceManager.Models;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using Microsoft.Rest;

namespace ArmService.Models
{
    public class ArmClient
    {
        private string clientId;
        private string clientSecret;
        private string tenantId;

        public ArmClient(string clientId, string clientSecret, string tenantId)
        {
            this.clientId = clientId;
            this.clientSecret = clientSecret;
            this.tenantId = tenantId;
        }

        public async Task<IEnumerable<Subscription>> ListSubscriptions()
        {
            var authToken = await GetAuthorizationToken();
            TokenCredentials tokenCreds = new TokenCredentials(authToken);
            SubscriptionClient subscriptionClient = new SubscriptionClient(tokenCreds);
            return await subscriptionClient.Subscriptions.ListAsync();
        }

        private async Task<string> GetAuthorizationToken()
        {
            ClientCredential cc = new ClientCredential(this.clientId, this.clientSecret);
            var context = new AuthenticationContext("https://login.windows.net/" + this.tenantId);
            var result = await context.AcquireTokenAsync("https://management.azure.com/", cc);
            if (result == null)
            {
                throw new InvalidOperationException("Failed to obtain the JWT token");
            }

            return result.AccessToken;
        }
    }
}