namespace bootstrapper.sample.Specs.Magic
{
    using System.Collections.Generic;
    using System.Reflection;
    using Ninject;

    public class InjectFieldsWithInjectAttribute<TSpecification> : FieldsWithAttributeCaching<TSpecification, InjectAttribute>
        where TSpecification : InitializedBootstrapperSpecification
    {
        protected override void PrepareFields(IEnumerable<FieldInfo> fields)
        {
            foreach (FieldInfo inject in fields)
            {
                inject.SetValue(null, this.Kernel.Get(inject.FieldType));
            }
        }
    }
}