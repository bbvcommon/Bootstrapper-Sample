namespace bootstrapper.sample.Specs.Magic
{
    using System;
    using System.Collections.ObjectModel;
    using bbv.Common.Bootstrapper;

    public class DecoratedExtensionPoint : IExtensionPoint<ISensor>
    {
        private readonly IExtensionPoint<ISensor> extensionPoint;
        private readonly Func<ISensor, bool> selector;
        private readonly Collection<ISensor> sensors;

        public DecoratedExtensionPoint(IExtensionPoint<ISensor> extensionPoint, Func<ISensor, bool> selector, Collection<ISensor> sensors)
        {
            this.sensors = sensors;
            this.selector = selector;
            this.extensionPoint = extensionPoint;
        }

        public void AddExtension(ISensor extension)
        {
            if (this.selector(extension))
            {
                this.extensionPoint.AddExtension(extension);
                this.sensors.Add(extension);
            }
        }
    }
}