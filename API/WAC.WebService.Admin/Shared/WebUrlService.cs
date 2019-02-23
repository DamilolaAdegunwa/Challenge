using Microsoft.Extensions.Configuration;
using WAC.Admin.Domain.Url;
using System.Collections.Generic;
using System.Linq;
using FWK.Domain.bus;

namespace WAC.WebService.Admin.Shared
{
    public class WebUrlService : WebUrlServiceBase, IWebUrlService, ITransientDependency
    {
        public WebUrlService(
            IConfiguration configuration) :
            base(configuration)
        {
        }

        public override string WebSiteRootAddressFormatKey => "App:ClientRootAddress";

        public override string ServerRootAddressFormatKey => "App:ServerRootAddress";
    }

    public abstract class WebUrlServiceBase
    {
        public abstract string WebSiteRootAddressFormatKey { get; }

        public abstract string ServerRootAddressFormatKey { get; }

        public string WebSiteRootAddressFormat => _appConfiguration[WebSiteRootAddressFormatKey] ?? "http://localhost:4200/";

        public string ServerRootAddressFormat => _appConfiguration[ServerRootAddressFormatKey] ?? "http://localhost:44376/";
         

        readonly IConfiguration _appConfiguration;

        public WebUrlServiceBase(IConfiguration configuration)
        {
            _appConfiguration = configuration;
        }

        public string GetSiteRootAddress()
        {
            return WebSiteRootAddressFormat;
        }

        public string GetServerRootAddress()
        {
            return ServerRootAddressFormat;
        }

        public List<string> GetRedirectAllowedExternalWebSites()
        {
            var values = _appConfiguration["App:RedirectAllowedExternalWebSites"];
            return values?.Split(',').ToList() ?? new List<string>();
        }

         
    }

}