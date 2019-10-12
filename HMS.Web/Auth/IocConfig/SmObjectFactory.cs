using System;
using StructureMap;
using System.Threading;
using Microsoft.Owin.Security.Infrastructure;
using Microsoft.Owin.Security.OAuth;
using StructureMap.Web;
using HMS.Service.Auth.Interfaces;
using HMS.Service.Auth.Implements;
using HMS.Web.Auth.JwtConfig;

namespace HMS.Web.Auth.IocConfig
{
    public static class SmObjectFactory
    {
        private static readonly Lazy<Container> _containerBuilder =
            new Lazy<Container>(defaultContainer, LazyThreadSafetyMode.ExecutionAndPublication);

        public static IContainer Container { get; } = _containerBuilder.Value;

        private static Container defaultContainer()
        {
            return new Container(ioc =>
            {
                ioc.For<IAppJwtConfiguration>().Singleton().Use(() => AppJwtConfiguration.Config);
                ioc.For<IUsersService>().HybridHttpOrThreadLocalScoped().Use<UsersService>();
                ioc.For<ITokenStoreService>().HybridHttpOrThreadLocalScoped().Use<TokenStoreService>();
                ioc.For<ISecurityService>().HybridHttpOrThreadLocalScoped().Use<SecurityService>();

                ioc.Policies.SetAllProperties(setterConvention =>
                {
                    // For WebAPI ActionFilter Dependency Injection
                    setterConvention.OfType<Func<IUsersService>>();
                    setterConvention.OfType<Func<ITokenStoreService>>();
                });

                // we only need one instance of this provider
                ioc.For<IOAuthAuthorizationServerProvider>().Singleton().Use<AppOAuthProvider>();
                ioc.For<IAuthenticationTokenProvider>().Singleton().Use<RefreshTokenProvider>();
            });
        }
    }
}