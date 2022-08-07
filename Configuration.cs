﻿using IdentityModel;
using IdentityServer4;
using IdentityServer4.Models;

namespace Notes.Identity
{
    public static class Configuration
    {
        public static IEnumerable<ApiScope> ApiScopes =>
            new List<ApiScope>()
            {
                new ApiScope("NotesWebAPI", "WebAPI")
            };

        public static IEnumerable<IdentityResource> IdentityResources =>
            new List<IdentityResource>()
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile()
            };

        public static IEnumerable<ApiResource> ApiResources =>
            new List<ApiResource>()
            {
                new ApiResource("NotesWebAPI", "WebAPI", new[] { JwtClaimTypes.Name })
                {
                    Scopes = {"NotesWebAPI"}
                }
            };

        public static IEnumerable<Client> Clients =>
            new List<Client>()
            {
                new Client()
                {
                    ClientId = "notes-web-api",
                    ClientName = "Notes WebAPI",
                    AllowedGrantTypes = GrantTypes.Code,
                    RequireClientSecret = false,
                    RedirectUris = {"http://.../signin-oidc"},
                    AllowedCorsOrigins = {"http://..."},
                    PostLogoutRedirectUris = {"http://.../signout-oidc"},
                    AllowedScopes = 
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        "NotesWebAPI"
                    },
                    AllowAccessTokensViaBrowser = true
                }
            };
    }
}
