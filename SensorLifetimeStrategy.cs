using Ninject.Syntax;

namespace bootstrapper.sample
{
    using System;
    using System.Reflection;
    using bbv.Common.Bootstrapper;
    using bbv.Common.Bootstrapper.Syntax;
    using bootstrapper.sample.Sirius;
    using Ninject;

    // HINT : Extension resolver / Override
    // HINT : DefineRunSyntax
    // HINT : DefineShutdownSyntax
    public class SensorLifetimeStrategy : AbstractStrategy<ISensor>
    {
        private readonly Lazy<IKernel> standardKernel;

        public SensorLifetimeStrategy()
        {
            this.standardKernel =
                new Lazy<IKernel>(() =>
                    {
                        var kernel = new StandardKernel();
                        kernel.Load(Assembly.GetExecutingAssembly());
                        kernel.Bind<IResolutionRoot>().ToConstant(kernel);
                        return kernel;
                    });
        }

        protected IKernel Kernel
        {
            get { return this.standardKernel.Value; }
        }

        protected override void DefineRunSyntax(ISyntaxBuilder<ISensor> builder)
        {
            // HINT : Fill in here
        }

        protected override void DefineShutdownSyntax(ISyntaxBuilder<ISensor> builder)
        {
            // HINT : Fill in here
        }

        protected override void Dispose(bool disposing)
        {
            if (this.standardKernel.IsValueCreated && !this.Kernel.IsDisposed)
            {
                this.Kernel.Dispose();
            }

            base.Dispose(disposing);
        }

        private IVphtMessageBus GetVphtMessageBus()
        {
            return this.Kernel.Get<IVphtMessageBus>();
        }

        private IVhptDataBus GetVphtDataBus()
        {
            return this.Kernel.Get<IVhptDataBus>();
        }
    }
}