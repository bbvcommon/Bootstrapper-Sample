namespace bootstrapper.sample
{
    using bbv.Common.Bootstrapper;
    using bbv.Common.Bootstrapper.Syntax;

    public class SensorLifetimeStrategy : AbstractStrategy<ISensor>
    {
        protected override void DefineRunSyntax(ISyntaxBuilder<ISensor> builder)
        {
            builder.Execute(s => s.StartObservation());
        }

        protected override void DefineShutdownSyntax(ISyntaxBuilder<ISensor> builder)
        {
            builder.Execute(s => s.StopObservation());
        }
    }
}