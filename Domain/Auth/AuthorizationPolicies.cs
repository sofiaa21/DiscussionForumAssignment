﻿namespace Domain.Auth;

using Microsoft.Extensions.DependencyInjection;

public class AuthorizationPolicies
{
    public static void AddPolicies(IServiceCollection services)
    {
        services.AddAuthorizationCore();
    }
}