using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http.Controllers;
using System.Web.Http.Routing;
using Umbraco.RestApi.Controllers;

namespace Umbraco.RestApi
{
    /// <summary>
    /// This is used to lookup our CustomRouteAttribute instead of the normal RouteAttribute so that 
    /// we can use the CustomRouteAttribute instead of the RouteAttribute on our controlles so the normal
    /// MapHttpAttributeRoutes method doesn't try to route our controllers - since the point of this is
    /// to be able to map our controller routes with attribute routing explicitly without interfering
    /// with normal developers usages.
    /// </summary>
    internal class CustomRouteAttributeDirectRouteProvider : DefaultDirectRouteProvider
    {
        private readonly bool _inherit;

        public CustomRouteAttributeDirectRouteProvider(bool inherit = false)
        {
            _inherit = inherit;
        }

        protected override IReadOnlyList<IDirectRouteFactory> GetActionRouteFactories(HttpActionDescriptor actionDescriptor)
        {
            //return actionDescriptor.GetCustomAttributes<IDirectRouteFactory>(inherit: _inherit);

            var customRoutes = actionDescriptor.GetCustomAttributes<CustomRouteAttribute>(inherit: _inherit);

            // inherit route attributes decorated on base class controller's actions
            return customRoutes.Select(x => x.InnerAttribute).ToList();
        }
    }
}