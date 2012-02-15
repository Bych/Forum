// Type: StructureMap.Container
// Assembly: StructureMap, Version=2.6.1.0, Culture=neutral, PublicKeyToken=e60ad81abae3c223
// Assembly location: E:\Programming\ASP_NET_MVC\Forum\Forum\packages\StructureMap\StructureMap.dll

using StructureMap.Configuration.DSL;
using StructureMap.Graph;
using StructureMap.Pipeline;
using StructureMap.Query;
using System;
using System.Collections;
using System.Collections.Generic;

namespace StructureMap
{
    public class Container : IContainer, IDisposable
    {
        public Container(Action<ConfigurationExpression> action);
        public Container(Registry registry);
        public Container();
        public Container(PluginGraph pluginGraph);
        protected MissingFactoryFunction onMissingFactory { set; }
        public PluginGraph PluginGraph { get; }

        #region IContainer Members

        public T GetInstance<T>(string instanceKey);
        public T GetInstance<T>(Instance instance);
        public PLUGINTYPE GetInstance<PLUGINTYPE>(ExplicitArguments args);
        public T GetInstance<T>(ExplicitArguments args, string name);
        public object GetInstance(Type pluginType, ExplicitArguments args);
        public IList GetAllInstances(Type type, ExplicitArguments args);
        public IList<T> GetAllInstances<T>(ExplicitArguments args);
        public T GetInstance<T>();

        [Obsolete("Please use GetInstance<T>() instead.")]
        public T FillDependencies<T>();

        public IList<T> GetAllInstances<T>();
        public void SetDefaultsToProfile(string profile);
        public object GetInstance(Type pluginType, string instanceKey);
        public object TryGetInstance(Type pluginType, string instanceKey);
        public object TryGetInstance(Type pluginType);
        public T TryGetInstance<T>();
        public void BuildUp(object target);
        public T TryGetInstance<T>(string instanceKey);
        public object GetInstance(Type pluginType);
        public object GetInstance(Type pluginType, Instance instance);
        public void SetDefault(Type pluginType, Instance instance);

        [Obsolete("Please use GetInstance(Type) instead")]
        public object FillDependencies(Type type);

        public IList GetAllInstances(Type pluginType);
        public void Configure(Action<ConfigurationExpression> configure);
        public string WhatDoIHave();
        public ExplicitArgsExpression With<T>(T arg);
        public ExplicitArgsExpression With(Type pluginType, object arg);
        public IExplicitProperty With(string argName);
        public void AssertConfigurationIsValid();
        public void EjectAllInstancesOf<T>();
        public Container.OpenGenericTypeExpression ForGenericType(Type templateType);
        public CloseGenericTypeExpression ForObject(object subject);
        public IContainer GetNestedContainer();
        public IContainer GetNestedContainer(string profileName);
        public void Dispose();
        public void Inject<PLUGINTYPE>(PLUGINTYPE instance);
        public void Inject<PLUGINTYPE>(string name, PLUGINTYPE value);
        public void Inject(Type pluginType, object @object);
        public IModel Model { get; }

        #endregion

        public ExplicitArgsExpression With(Action<ExplicitArgsExpression> action);
        public void Inject(Type pluginType, Instance instance);

        #region Nested type: GetInstanceAsExpression

        public interface GetInstanceAsExpression
        {
            T GetInstanceAs<T>();
        }

        #endregion

        #region Nested type: OpenGenericTypeExpression

        public class OpenGenericTypeExpression : Container.GetInstanceAsExpression
        {
            public OpenGenericTypeExpression(Type templateType, Container container);

            #region GetInstanceAsExpression Members

            public T GetInstanceAs<T>();

            #endregion

            public Container.GetInstanceAsExpression WithParameters(params Type[] parameterTypes);
        }

        #endregion
    }
}
