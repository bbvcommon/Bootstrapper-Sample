namespace bootstrapper.sample
{
    using bbv.Common.Bootstrapper;
    using Ninject.Syntax;

    public class NinjectSensorResolver : IExtensionResolver<ISensor>
    {
        private readonly IResolutionRoot resolutionRoot;

        public NinjectSensorResolver(IResolutionRoot resolutionRoot)
        {
            this.resolutionRoot = resolutionRoot;
        }

        public void Resolve(IExtensionPoint<ISensor> extensionPoint)
        {
            // Hint : Resolve extensions with IResolutionRoot
        }
    }
}