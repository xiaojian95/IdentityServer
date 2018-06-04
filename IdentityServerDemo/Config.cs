using IdentityServer4.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityServerDemo
{
    public class Config
    {
        public static IEnumerable<ApiResource> GetapiResources()
        {
            return new List<ApiResource>()
            {
                new ApiResource("api","My Api")
            };
        }
        public static IEnumerable<Client> GetClients()
        {
            return new List<Client>
            {
                new Client()
                {
                     ClientId="clientId",
                     //使用clientid/secret进行身份验证
                     AllowedGrantTypes=GrantTypes.ClientCredentials,
                     //进行身份验证
                     ClientSecrets =
                    {
                        new Secret("secret".Sha256())
                    },
                     //客户端可以访问的范围
                    AllowedScopes={"api" }
                }
            };
        }
    }
}
