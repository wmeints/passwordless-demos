using Duende.IdentityServer.Models;

namespace IdentityServerWithOneTimePassword;

public class Config
{
    public static IEnumerable<IdentityResource> GetIdentityResources()
    {
        yield return new IdentityResources.OpenId();
        yield return new IdentityResources.Profile();
    }

    public static IEnumerable<ApiResource> GetApiResources()
    {
        yield return new ApiResource("customers", "Customer Data");
    }

    internal static IEnumerable<Client> GetClients()
    {
        yield return new Client
        {
            ClientId = "",
            ClientSecrets = new[] {
                new Secret("".Sha256())
            },
            AllowedGrantTypes = GrantTypes.Code,
            RedirectUris = {"https://localhost:6001/signin-callback-oidc"},
            PostLogoutRedirectUris = {"https://localhost:6001/signout-callback-oidc"},
            AllowAccessTokensViaBrowser = true,
            RequirePkce = false,
            AllowedScopes = {"customers", "openid", "profile"}
        };
    }
}