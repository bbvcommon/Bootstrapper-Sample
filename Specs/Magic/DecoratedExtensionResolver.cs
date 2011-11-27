namespace bootstrapper.sample.Specs.Magic
{
    using System;
    using bbv.Common.Bootstrapper;

    public class DecoratedExtensionResolver : IExtensionResolver<ISensor>
    {
        private readonly IExtensionResolver<ISensor> extensionResolver;
        private readonly Func<ISensor, bool> selector;

        public DecoratedExtensionResolver(IExtensionResolver<ISensor> extensionResolver, Func<ISensor, bool> selector)
        {
            this.selector = selector;
            this.extensionResolver = extensionResolver;
        }

        public void Resolve(IExtensionPoint<ISensor> extensionPoint)
        {
            this.extensionResolver.Resolve(new DecoratedExtensionPoint(extensionPoint, this.selector));
        }
    }
}