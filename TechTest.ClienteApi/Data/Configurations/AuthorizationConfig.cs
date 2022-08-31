using System;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using ClienteApi.Authorization;

namespace ClienteApi
{
    public static class AuthorizationConfig
    {
        public static void LoadAuthentication(this IServiceCollection services)
        {
            services.AddAuthentication(auth =>
                {
                    auth.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    auth.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(token =>
                {
                    token.RequireHttpsMetadata = false;
                    token.SaveToken = true;
                    token.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey =
                            new SymmetricSecurityKey(
                                System.Text.Encoding.UTF8.GetBytes("ZmVkYWY3ZDg4NjNiNDhlMTk3YjkyODdkNDkyYjcwOGU=")),
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        ClockSkew = TimeSpan.Zero
                    };
                });

            services.AddAuthorization(option =>
            {
                option.AddPolicy("IdadeMinima", policy =>
                {
                    policy.Requirements.Add(new IdadeMinimaRequirement(10));
                });
            });
        }
    }
}