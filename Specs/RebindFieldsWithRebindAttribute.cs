namespace bootstrapper.sample.Specs
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using Moq;

    public class RebindFieldsWithRebindAttribute<TSpecification> : FieldsWithAttributeCaching<TSpecification, RebindAttribute>
        where TSpecification : InitializedBootstrapperSpecification
    {
        protected override void PrepareFields(IEnumerable<FieldInfo> fields)
        {
            foreach (FieldInfo inject in fields)
            {
                var genericArguments = inject.FieldType.GetGenericArguments();
                if (genericArguments.Any() && genericArguments.Count() == 1)
                {
                    Type genericTypeArgument = genericArguments.Single();
                    var mockType = typeof(Mock<>).MakeGenericType(genericTypeArgument);

                    if (inject.FieldType.IsAssignableFrom(mockType))
                    {
                        var mock = (Mock)inject.GetValue(null);
                        this.Kernel.Rebind(genericTypeArgument).ToConstant(mock.Object);
                    }

                    continue;
                }

                this.Kernel.Rebind(inject.FieldType).ToConstant(inject.GetValue(null));
            }
        }
    }
}