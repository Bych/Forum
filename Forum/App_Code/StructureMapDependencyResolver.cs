using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using StructureMap;
using System.Collections;

namespace Forum.App_Code
{
    public class StructureMapDependencyResolver : IDependencyResolver
    {
        readonly IContainer _container;

        public StructureMapDependencyResolver(IContainer container)
        {
            this._container = container;
        }

        public object GetService(Type serviceType)
        {
            if (serviceType.IsClass)
            {
                return GetConcreteService(serviceType);
            }
            else
            {
                return GetInterfaceService(serviceType);
            }
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return _container.GetAllInstances(serviceType).Cast<object>();
        }

        private object GetConcreteService(Type serviceType)
        {
            try
            {
                // Can't use TryGetInstance here because it won’t create concrete types
                return _container.GetInstance(serviceType);
            }
            catch (StructureMapException)
            {
                return null;
            }
        }

        private object GetInterfaceService(Type serviceType)
        {
            return _container.TryGetInstance(serviceType);
        }
    }
        
}