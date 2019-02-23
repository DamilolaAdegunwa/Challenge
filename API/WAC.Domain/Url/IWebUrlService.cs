using System;
using System.Collections.Generic;
using System.Text;

namespace WAC.Admin.Domain.Url
{
    public interface IWebUrlService
    {
        string WebSiteRootAddressFormat { get; }

        string ServerRootAddressFormat { get; }

        string GetSiteRootAddress();

        string GetServerRootAddress();

        List<string> GetRedirectAllowedExternalWebSites();
    }
}
