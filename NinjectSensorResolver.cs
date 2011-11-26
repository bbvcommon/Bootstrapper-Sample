namespace bootstrapper.sample
{
    using bbv.Common.Bootstrapper;
    using Ninject;
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
            extensionPoint.AddExtension(this.resolutionRoot.Get<IDoorSensor>());
            extensionPoint.AddExtension(this.resolutionRoot.Get<ISerotoninSensor>());
            extensionPoint.AddExtension(this.resolutionRoot.Get<IBlackHoleSensor>());
        }
    }
}