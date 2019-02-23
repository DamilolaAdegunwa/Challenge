using Microsoft.Extensions.Configuration;
using WAC.Admin.Domain.Url;
using FWK.Domain.bus;

namespace WAC.WebService.Admin.Shared
{
    public class AngularAppUrlService : AppUrlServiceBase
    {
        public override string PasswordResetRoute => "account/reset";

        public AngularAppUrlService(IWebUrlService webUrlService, IConfiguration configuration) : 
            base(webUrlService, configuration
            )
        {

        }
    }
 
}