// Type: System.Activator
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Configuration.Assemblies;
using System.Globalization;
using System.Reflection;
using System.Runtime;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Remoting;
using System.Security;
using System.Security.Policy;

namespace System
{
    [ComVisible(true)]
    [ClassInterface(ClassInterfaceType.None)]
    [ComDefaultInterface(typeof (_Activator))]
    public sealed class Activator : _Activator
    {
        #region _Activator Members

        void _Activator.GetTypeInfoCount(out uint pcTInfo);
        void _Activator.GetTypeInfo(uint iTInfo, uint lcid, IntPtr ppTInfo);
        void _Activator.GetIDsOfNames([In] ref Guid riid, IntPtr rgszNames, uint cNames, uint lcid, IntPtr rgDispId);
        void _Activator.Invoke(uint dispIdMember, [In] ref Guid riid, uint lcid, short wFlags, IntPtr pDispParams, IntPtr pVarResult, IntPtr pExcepInfo, IntPtr puArgErr);

        #endregion

        [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
        public static object CreateInstance(Type type, BindingFlags bindingAttr, Binder binder, object[] args, CultureInfo culture);

        [SecuritySafeCritical]
        public static object CreateInstance(Type type, BindingFlags bindingAttr, Binder binder, object[] args, CultureInfo culture, object[] activationAttributes);

        [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
        public static object CreateInstance(Type type, params object[] args);

        [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
        public static object CreateInstance(Type type, object[] args, object[] activationAttributes);

        [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
        public static object CreateInstance(Type type);

        [SecuritySafeCritical]
        [MethodImpl(MethodImplOptions.NoInlining)]
        public static ObjectHandle CreateInstance(string assemblyName, string typeName);

        [SecuritySafeCritical]
        [MethodImpl(MethodImplOptions.NoInlining)]
        public static ObjectHandle CreateInstance(string assemblyName, string typeName, object[] activationAttributes);

        public static object CreateInstance(Type type, bool nonPublic);
        public static T CreateInstance<T>();

        [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
        public static ObjectHandle CreateInstanceFrom(string assemblyFile, string typeName);

        [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
        public static ObjectHandle CreateInstanceFrom(string assemblyFile, string typeName, object[] activationAttributes);

        [Obsolete("Methods which use evidence to sandbox are obsolete and will be removed in a future release of the .NET Framework. Please use an overload of CreateInstance which does not take an Evidence parameter. See http://go.microsoft.com/fwlink/?LinkID=155570 for more information.")]
        [SecuritySafeCritical]
        [MethodImpl(MethodImplOptions.NoInlining)]
        public static ObjectHandle CreateInstance(string assemblyName, string typeName, bool ignoreCase, BindingFlags bindingAttr, Binder binder, object[] args, CultureInfo culture, object[] activationAttributes, Evidence securityInfo);

        [SecuritySafeCritical]
        [MethodImpl(MethodImplOptions.NoInlining)]
        public static ObjectHandle CreateInstance(string assemblyName, string typeName, bool ignoreCase, BindingFlags bindingAttr, Binder binder, object[] args, CultureInfo culture, object[] activationAttributes);

        [Obsolete("Methods which use evidence to sandbox are obsolete and will be removed in a future release of the .NET Framework. Please use an overload of CreateInstanceFrom which does not take an Evidence parameter. See http://go.microsoft.com/fwlink/?LinkID=155570 for more information.")]
        public static ObjectHandle CreateInstanceFrom(string assemblyFile, string typeName, bool ignoreCase, BindingFlags bindingAttr, Binder binder, object[] args, CultureInfo culture, object[] activationAttributes, Evidence securityInfo);

        [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
        public static ObjectHandle CreateInstanceFrom(string assemblyFile, string typeName, bool ignoreCase, BindingFlags bindingAttr, Binder binder, object[] args, CultureInfo culture, object[] activationAttributes);

        [SecurityCritical]
        public static ObjectHandle CreateInstance(AppDomain domain, string assemblyName, string typeName);

        [SecurityCritical]
        [Obsolete("Methods which use evidence to sandbox are obsolete and will be removed in a future release of the .NET Framework. Please use an overload of CreateInstance which does not take an Evidence parameter. See http://go.microsoft.com/fwlink/?LinkID=155570 for more information.")]
        public static ObjectHandle CreateInstance(AppDomain domain, string assemblyName, string typeName, bool ignoreCase, BindingFlags bindingAttr, Binder binder, object[] args, CultureInfo culture, object[] activationAttributes, Evidence securityAttributes);

        [SecurityCritical]
        public static ObjectHandle CreateInstance(AppDomain domain, string assemblyName, string typeName, bool ignoreCase, BindingFlags bindingAttr, Binder binder, object[] args, CultureInfo culture, object[] activationAttributes);

        [SecurityCritical]
        public static ObjectHandle CreateInstanceFrom(AppDomain domain, string assemblyFile, string typeName);

        [SecurityCritical]
        [Obsolete("Methods which use Evidence to sandbox are obsolete and will be removed in a future release of the .NET Framework. Please use an overload of CreateInstanceFrom which does not take an Evidence parameter. See http://go.microsoft.com/fwlink/?LinkID=155570 for more information.")]
        public static ObjectHandle CreateInstanceFrom(AppDomain domain, string assemblyFile, string typeName, bool ignoreCase, BindingFlags bindingAttr, Binder binder, object[] args, CultureInfo culture, object[] activationAttributes, Evidence securityAttributes);

        [SecurityCritical]
        public static ObjectHandle CreateInstanceFrom(AppDomain domain, string assemblyFile, string typeName, bool ignoreCase, BindingFlags bindingAttr, Binder binder, object[] args, CultureInfo culture, object[] activationAttributes);

        [SecuritySafeCritical]
        public static ObjectHandle CreateInstance(ActivationContext activationContext);

        [SecuritySafeCritical]
        public static ObjectHandle CreateInstance(ActivationContext activationContext, string[] activationCustomData);

        [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
        public static ObjectHandle CreateComInstanceFrom(string assemblyName, string typeName);

        public static ObjectHandle CreateComInstanceFrom(string assemblyName, string typeName, byte[] hashValue, AssemblyHashAlgorithm hashAlgorithm);

        [SecurityCritical]
        [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
        public static object GetObject(Type type, string url);

        [SecurityCritical]
        public static object GetObject(Type type, string url, object state);
    }
}
