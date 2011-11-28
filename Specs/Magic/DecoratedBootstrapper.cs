namespace bootstrapper.sample.Specs.Magic
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using bbv.Common;
    using bbv.Common.Bootstrapper;

    public class DecoratedBootstrapper : IBootstrapper<ISensor>, IEnumerable<ISensor>
    {
        private readonly IBootstrapper<ISensor> decoratedBootstrapper;
        private SpecSensorLifetimeStrategy strategy;

        public DecoratedBootstrapper(IBootstrapper<ISensor> decoratedBootstrapper)
        {
            this.decoratedBootstrapper = decoratedBootstrapper;
        }

        public void SetSelector(Func<ISensor, bool> selector)
        {
            this.strategy.SetSelector(selector);
        }

        public void AddExtension(INeedKernel needKernel)
        {
            this.strategy.NeedKernels.Add(needKernel);
        }

        public void AddExtension(ISensor extension)
        {
            this.decoratedBootstrapper.AddExtension(extension);
        }

        public void Dispose()
        {
            this.decoratedBootstrapper.Dispose();
        }

        public void Initialize(IStrategy<ISensor> strategy)
        {
            Ensure.ArgumentAssignableFrom(typeof(SpecSensorLifetimeStrategy), strategy, "strategy");

            this.strategy = (SpecSensorLifetimeStrategy)strategy;
            this.decoratedBootstrapper.Initialize(strategy);
        }

        public void Run()
        {
            this.decoratedBootstrapper.Run();
        }

        public void Shutdown()
        {
            this.decoratedBootstrapper.Shutdown();
        }

        public IEnumerator<ISensor> GetEnumerator()
        {
            return this.strategy.Sensors.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}