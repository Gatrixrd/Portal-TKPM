using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Net.Http;
using System.Threading.Tasks;
using System.Threading;

namespace MVC5_full_version
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            //BundleMobileConfig.RegisterBundles(BundleTable.Bundles);
            //GlobalConfiguration.Configuration.MessageHandlers.Add(new AntiCSRFHandler());
        }
    }

    //public class AntiCSRFHandler : DelegatingHandler
    //{
    //    protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    //    {

    //        var referrer = request.Headers.Referrer;
    //        var uri = request.RequestUri;

    //        if (referrer != null && uri != null && referrer.Port == uri.Port && referrer.Host == uri.Host)
    //        {

    //            return base.SendAsync(request, cancellationToken);
    //        }
    //        var response = request.CreateResponse(System.Net.HttpStatusCode.InternalServerError);
    //        response.ReasonPhrase = "Invalid referrer";
    //        return Task.FromResult<HttpResponseMessage>(response);

    //    }
    //}
}
