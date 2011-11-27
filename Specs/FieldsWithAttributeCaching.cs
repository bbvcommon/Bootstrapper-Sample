namespace bootstrapper.sample.Specs
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using Ninject;

    public abstract class FieldsWithAttributeCaching<TSpecification, TAttribute> : INeedKernel
        where TSpecification : InitializedBootstrapperSpecification
        where TAttribute : Attribute
    {
        protected const BindingFlags BindingFlagsForStaticFields = BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.DeclaredOnly;

        protected static readonly Dictionary<Type, IEnumerable<FieldInfo>> InjectFieldsCache = new Dictionary<Type, IEnumerable<FieldInfo>>();

        protected IKernel Kernel { get; private set; }

        public void Need(IKernel kernel)
        {
            this.Kernel = kernel;

            Type specificationType = typeof(TSpecification);

            if (!InjectFieldsCache.ContainsKey(specificationType))
            {
                IEnumerable<FieldInfo> fields = specificationType.GetFields(BindingFlagsForStaticFields)
                    .Where(f => f.GetCustomAttributes(typeof(TAttribute), false).Count() == 1);

                InjectFieldsCache.Add(specificationType, fields.ToList());
            }

            this.PrepareFields(InjectFieldsCache[typeof(TSpecification)]);
        }

        protected abstract void PrepareFields(IEnumerable<FieldInfo> fields);
    }
}