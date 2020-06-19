// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using IdentityModel;
using IdentityServer4.Models;
using System.Collections.Generic;
using System.Security.Claims;

namespace IdentityApi
{
    public static class Config
    {
        public static IEnumerable<IdentityResource> Ids =>
            new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
                new IdentityResource( "roles", new [] { "role" } )
            };


        public static IEnumerable<ApiResource> Apis =>
            new List<ApiResource>
            {
                new ApiResource("api1", "My API #1"),
                new ApiResource("api2", "My API #2"),
                //new ApiResource("roles", new[] {"role"}),
            };
        public static IEnumerable<Client> Clients =>
            new List<Client>
            {
                //client credentials flow client
                // new Client
                // {
                //     ClientId = "client",
                //     ClientName = "Client Credentials Client",

                //     AllowedGrantTypes = GrantTypes.ResourceOwnerPasswordAndClientCredentials,
                //     ClientSecrets = { new Secret("secret".Sha256()) },
                //     //ClientSecrets = { new Secret("secret".Sha256()) },
                //     AllowedScopes = { "api1" }
                // },
                // new Client {
                //     ClientId = "postman",
                //     ClientName = "Postman Client",
                //     AllowedGrantTypes = GrantTypes.ResourceOwnerPasswordAndClientCredentials,
                //     AllowAccessTokensViaBrowser = true,
                //     RequireConsent = false,
                //     RedirectUris = {"https://app.getpostman.com/oauth2/callback"},
                //     PostLogoutRedirectUris = {"https://getpostman.com"},
                //     AllowedCorsOrigins = {"https://getpostman.com"},
                //     EnableLocalLogin = true,
                //     ClientSecrets = { new Secret("secret".Sha256()) },
                //     AllowedScopes = {"api1"}

                // },

                // MVC client using code flow + pkce
                // new Client
                // {
                //     ClientId = "mvc",
                //     ClientName = "MVC Client",

                //     AllowedGrantTypes = GrantTypes.CodeAndClientCredentials,
                //     RequirePkce = true,
                //     ClientSecrets = { new Secret("49C1A7E1-0C79-4A89-A3D6-A37998FB86B0".Sha256()) },

                //     RedirectUris = { "http://localhost:5003/signin-oidc" },
                //     FrontChannelLogoutUri = "http://localhost:5003/signout-oidc",
                //     PostLogoutRedirectUris = { "http://localhost:5003/signout-callback-oidc" },

                //     AllowOfflineAccess = true,
                //     AllowedScopes = { "openid", "profile", "api1" }
                // },

                // SPA client using code flow + pkce
                // new Client
                // {
                //     ClientId = "spa",
                //     ClientName = "SPA Client",
                //     ClientUri = "https://getpostman.com",

                //     AllowedGrantTypes = GrantTypes.Code,
                //     RequireClientSecret = false,
                //     AllowAccessTokensViaBrowser = true,
                //     //AlwaysIncludeUserClaimsInIdToken = true,
                //     RedirectUris = {"https://getpostman.com/oauth2/callback"},
                //     PostLogoutRedirectUris = {"https://getpostman.com"},
                //     AllowedCorsOrigins = {"https://getpostman.com"},
                //     AllowedScopes = { "api1", "openid", "profile", "roles"},
                // },
                new Client
                {
                    ClientId = "spa",
                    ClientName = "SPA Client",
                    ClientUri = "http://localhost:8080",

                    AllowedGrantTypes = GrantTypes.Code,
                    RequireClientSecret = false,
                    AllowAccessTokensViaBrowser = true,
                    RequireConsent = false,
                    AllowOfflineAccess = true,
                    AccessTokenLifetime = 3600,
                    AbsoluteRefreshTokenLifetime = 0,
                    RefreshTokenUsage = TokenUsage.OneTimeOnly,
                    RefreshTokenExpiration = TokenExpiration.Sliding,
                    UpdateAccessTokenClaimsOnRefresh = true,
                    //AlwaysIncludeUserClaimsInIdToken = true,
                    RedirectUris = {
                        "http://localhost:8080/callback",
                        "http://localhost:8080/static/silent-renew.html"
                        },
                    PostLogoutRedirectUris = {"http://localhost:8080"},
                    AllowedCorsOrigins = {"http://localhost:8080"},
                    AllowedScopes = {"api2", "api1", "openid", "profile", "roles"},
                }
            };
    }
}