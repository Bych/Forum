// Type: System.Web.Mvc.IControllerActivator
// Assembly: System.Web.Mvc, Version=3.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35
// Assembly location: C:\Program Files (x86)\Microsoft ASP.NET\ASP.NET MVC 3\Assemblies\System.Web.Mvc.dll

using System;
using System.Web.Routing;

namespace System.Web.Mvc
{
    public interface IControllerActivator
    {
        IController Create(RequestContext requestContext, Type controllerType);
    }
}
