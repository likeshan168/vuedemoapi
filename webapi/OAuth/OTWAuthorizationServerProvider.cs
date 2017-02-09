using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.OAuth;

namespace webapi.OAuth
{
    public class OTWAuthorizationServerProvider : OAuthAuthorizationServerProvider
    {
        //1.验证客户
        public override Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {      //此处可以判断client和user　<br>
               //this.ClientId = clientId;
               //this.IsValidated = true;
               //this.HasError = false;
            context.Validated("自定义的clientId");
            return base.ValidateClientAuthentication(context);
        }
        //授权客户
        public override Task GrantClientCredentials(OAuthGrantClientCredentialsContext context)
        {
            var ticket = new AuthenticationTicket(new ClaimsIdentity(new[] { new Claim(ClaimTypes.Name, "Never、C") }, context.Options.AuthenticationType), null);
            //this.Ticket = ticket;
            //this.IsValidated = true;
            //this.HasError = false;
            context.Validated(ticket);
            return base.GrantClientCredentials(context);
        }
    }
}