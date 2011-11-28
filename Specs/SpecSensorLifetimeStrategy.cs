namespace bootstrapper.sample.Specs
{
    using System;
    using System.Collections.ObjectModel;
    using bbv.Common.Bootstrapper;
    using Machine.Specifications.Runner.Impl;
    using Magic;

    internal class SpecSensorLifetimeStrategy : SensorLifetimeStrategy
    {
        private Func<ISensor, bool> selector;

        public SpecSensorLifetimeStrategy()
        {
            this.NeedKernels = new Collection<INeedKernel>();
            this.Sensors = new Collection<ISensor>();
            this.SetSelector(sensor => true);
        }

        public Collection<INeedKernel> NeedKernels { get; private set; }

        public Collection<ISensor> Sensors { get; private set; }

        public void SetSelector(Func<ISensor, bool> value)
        {
            this.selector = value;
        }

        public override IExtensionResolver<ISensor> CreateExtensionResolver()
        {
            this.NeedKernels.ForEach(h => h.Need(this.Kernel));

            return new DecoratedExtensionResolver(base.CreateExtensionResolver(), this.selector, this.Sensors);
        }
    }
}
