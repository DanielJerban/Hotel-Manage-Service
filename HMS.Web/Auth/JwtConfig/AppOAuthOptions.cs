using System;
using HMS.Web.Auth.IocConfig;
using Microsoft.Owin;
using Microsoft.Owin.Security.Infrastructure;
using Microsoft.Owin.Security.OAuth;

namespace HMS.Web.Auth.JwtConfig
{
    public class AppOAuthOptions : OAuthAuthorizationServerOptions
    {
        public AppOAuthOptions(IAppJwtConfiguration configuration)
        {
            this.AllowInsecureHttp = true; // TODO: Buy an SSL certificate!
            this.TokenEndpointPath = new PathString(configuration.TokenPath);
            this.AccessTokenExpireTimeSpan = TimeSpan.FromMinutes(configuration.ExpirationMinutes);
            this.AccessTokenFormat = new AppJwtWriterFormat(this, configuration);
            this.Provider = SmObjectFactory.Container.GetInstance<IOAuthAuthorizationServerProvider>();
            this.RefreshTokenProvider = SmObjectFactory.Container.GetInstance<IAuthenticationTokenProvider>();
        }
    }
}