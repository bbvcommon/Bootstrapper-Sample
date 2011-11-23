namespace bootstrapper.sample
{
    using System;
    using bbv.Common.Bootstrapper;
    using bbv.Common.Bootstrapper.Syntax;
    using bootstrapper.sample.Sirius;
    using Ninject;

    public class SensorLifetimeStrategy : AbstractStrategy<ISensor>
    {
        private readonly Lazy<IKernel> standardKernel;

        public SensorLifetimeStrategy()
        {
            this.standardKernel = new Lazy<IKernel>(() => new StandardKernel(new SiriusModule(), new SensorModule()));
        }

        public override IExtensionResolver<ISensor> CreateExtensionResolver()
        {
            return new NinjectExtensionResolver(this.standardKernel.Value);
        }

        protected override void DefineRunSyntax(ISyntaxBuilder<ISensor> builder)
        {
            builder
                .Execute(() => GetVphtMessageBus(), (sensor, messagebus) => sensor.MessageBusInitialized(messagebus))
                .Execute(() => GetVphtDataBus(), (sensor, databus) => sensor.DataBusInitialized(databus))
                .Execute(sensor => sensor.Initialize())
                .Execute(sensor => sensor.StartObservation());
        }

        private IVphtMessageBus GetVphtMessageBus()
        {
            return this.standardKernel.Value.Get<IVphtMessageBus>();
        }

        private IVhptDataBus GetVphtDataBus()
        {
            return this.standardKernel.Value.Get<IVhptDataBus>();
        }

        protected override void DefineShutdownSyntax(ISyntaxBuilder<ISensor> builder)
        {
            builder.Execute(sensor => sensor.StopObservation());
        }

        protected override void Dispose(bool disposing)
        {
            if (this.standardKernel.IsValueCreated && !this.standardKernel.Value.IsDisposed)
            {
                this.standardKernel.Value.Dispose();
            }

            base.Dispose(disposing);
        }
    }
}