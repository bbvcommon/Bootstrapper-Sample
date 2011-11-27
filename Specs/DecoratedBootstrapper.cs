namespace bootstrapper.sample.Specs
{
    using bbv.Common.Bootstrapper;

    public class DecoratedBootstrapper : IBootstrapper<ISensor>
    {
        private readonly IBootstrapper<ISensor> decoratedBootstrapper;
        private SpecSensorLifetimeStrategy strategy;

        public DecoratedBootstrapper(IBootstrapper<ISensor> decoratedBootstrapper)
        {
            this.decoratedBootstrapper = decoratedBootstrapper;
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
    }
}