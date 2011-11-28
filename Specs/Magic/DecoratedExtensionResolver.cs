namespace bootstrapper.sample.Specs.Magic
{
    using System;
    using System.Collections.ObjectModel;
    using bbv.Common.Bootstrapper;

    public class DecoratedExtensionResolver : IExtensionResolver<ISensor>
    {
        private readonly IExtensionResolver<ISensor> extensionResolver;
        private readonly Func<ISensor, bool> selector;
        private readonly Collection<ISensor> sensors;

        public DecoratedExtensionResolver(IExtensionResolver<ISensor> extensionResolver, Func<ISensor, bool> selector, Collection<ISensor> sensors)
        {
            this.sensors = sensors;
            this.selector = selector;
            this.extensionResolver = extensionResolver;
        }

        public void Resolve(IExtensionPoint<ISensor> extensionPoint)
        {
            this.extensionResolver.Resolve(new DecoratedExtensionPoint(extensionPoint, this.selector, sensors));
        }
    }
}