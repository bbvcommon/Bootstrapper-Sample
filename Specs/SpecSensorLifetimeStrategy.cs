namespace bootstrapper.sample.Specs
{
    using System.Collections.ObjectModel;
    using bbv.Common.Bootstrapper;
    using Machine.Specifications.Runner.Impl;

    internal class SpecSensorLifetimeStrategy : SensorLifetimeStrategy
    {
        public SpecSensorLifetimeStrategy()
        {
            this.NeedKernels = new Collection<INeedKernel>();
        }

        public Collection<INeedKernel> NeedKernels { get; private set; }

        public override IExtensionResolver<ISensor> CreateExtensionResolver()
        {
            this.NeedKernels.ForEach(h => h.Need(this.standardKernel.Value));

            return base.CreateExtensionResolver();
        }
    }
}
