namespace bootstrapper.sample.Specs
{
    using bbv.Common.Bootstrapper;
    using Machine.Specifications;
    using Magic;

    public class InitializedBootstrapperSpecification
    {
        protected static DecoratedBootstrapper Bootstrapper;

        private static SensorLifetimeStrategy Strategy;

        Establish context = () =>
        {
            Bootstrapper = new DecoratedBootstrapper(new DefaultBootstrapper<ISensor>());
            Strategy = new SpecSensorLifetimeStrategy();

            Bootstrapper.Initialize(Strategy);
        };
    }
}