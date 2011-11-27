namespace bootstrapper.sample.Specs.Magic
{
    using System;
    using bbv.Common.Bootstrapper;

    public class DecoratedExtensionPoint : IExtensionPoint<ISensor>
    {
        private readonly IExtensionPoint<ISensor> extensionPoint;
        private readonly Func<ISensor, bool> selector;

        public DecoratedExtensionPoint(IExtensionPoint<ISensor> extensionPoint, Func<ISensor, bool> selector)
        {
            this.selector = selector;
            this.extensionPoint = extensionPoint;
        }

        public void AddExtension(ISensor extension)
        {
            if (this.selector(extension))
            {
                this.extensionPoint.AddExtension(extension);
            }
        }
    }
}