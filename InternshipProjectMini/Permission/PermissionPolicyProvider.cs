using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;
using System;
using System.Threading.Tasks;

namespace InternshipProjectMini.Permission
{
    internal class PermissionPolicyProvider: IAuthorizationPolicyProvider
    {
        public DefaultAuthorizationPolicyProvider FallbackpolicyProvider { get; }
        public PermissionPolicyProvider(IOptions<AuthorizationOptions> options)
        {
            FallbackpolicyProvider = new DefaultAuthorizationPolicyProvider(options);
        }
        public Task<AuthorizationPolicy> GetDefaultPolicyAsync()=>FallbackpolicyProvider.GetDefaultPolicyAsync();

        public Task<AuthorizationPolicy?> GetPolicyAsync(string policyName)
        {
           if(policyName.StartsWith("Permission",StringComparison.OrdinalIgnoreCase))
            {
                var policy = new AuthorizationPolicyBuilder();
                policy.AddRequirements(new PermissionRequirement(policyName));
                return Task.FromResult(policy.Build());
            }
           return FallbackpolicyProvider.GetPolicyAsync(policyName);

        }

        public Task<AuthorizationPolicy> GetFallbackPolicyAsync() => FallbackpolicyProvider.GetDefaultPolicyAsync();

        Task<AuthorizationPolicy?> IAuthorizationPolicyProvider.GetPolicyAsync(string policyName)
        {
            throw new System.NotImplementedException();
        }

        Task<AuthorizationPolicy> IAuthorizationPolicyProvider.GetDefaultPolicyAsync()
        {
            throw new System.NotImplementedException();
        }

        Task<AuthorizationPolicy?> IAuthorizationPolicyProvider.GetFallbackPolicyAsync()
        {
            throw new System.NotImplementedException();
        }
    }
}
