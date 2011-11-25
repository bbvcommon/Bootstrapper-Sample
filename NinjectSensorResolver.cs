namespace bootstrapper.sample
{
    using System.Collections.Generic;
    using bbv.Common.Bootstrapper;

    public class NinjectSensorResolver : IExtensionResolver<ISensor>
    {
        private readonly IEnumerable<ISensor> sensors;

        public NinjectSensorResolver(IEnumerable<ISensor> sensors)
        {
            this.sensors = sensors;
        }

        public void Resolve(IExtensionPoint<ISensor> extensionPoint)
        {
            foreach (ISensor sensor in this.sensors)
            {
                extensionPoint.AddExtension(sensor);
            }
        }
    }
}