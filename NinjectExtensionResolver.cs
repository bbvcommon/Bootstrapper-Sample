namespace bootstrapper.sample
{
    using bbv.Common.Bootstrapper;
    using Ninject;
    using Ninject.Syntax;

    public class NinjectExtensionResolver : IExtensionResolver<ISensor>
    {
        private readonly IResolutionRoot resolutionRoot;

        public NinjectExtensionResolver(IResolutionRoot resolutionRoot)
        {
            this.resolutionRoot = resolutionRoot;
        }

        public void Resolve(IExtensionPoint<ISensor> extensionPoint)
        {
            var sensors = this.resolutionRoot.GetAll<ISensor>();

            foreach (ISensor sensor in sensors)
            {
                extensionPoint.AddExtension(sensor);
            }
        }
    }
}