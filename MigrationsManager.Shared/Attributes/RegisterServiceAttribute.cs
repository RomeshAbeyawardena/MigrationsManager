using Microsoft.Extensions.DependencyInjection;
using System;

namespace MigrationsManager.Shared.Attributes
{
    [System.AttributeUsage(AttributeTargets.All, Inherited = false, AllowMultiple = true)]
    public sealed class RegisterServiceAttribute : Attribute
    {

        public RegisterServiceAttribute(ServiceLifetime serviceLifetime = ServiceLifetime.Singleton)
        {
            ServiceLifetime = serviceLifetime;
        }

        public ServiceLifetime ServiceLifetime { get; }
    }

}
