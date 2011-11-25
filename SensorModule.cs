namespace bootstrapper.sample
{
    using bbv.Common.Bootstrapper;
    using Ninject.Modules;

    public class SensorModule : NinjectModule
    {
        public override void Load()
        {
            this.Bind<IExtensionResolver<ISensor>>().To<NinjectSensorResolver>().InSingletonScope();

            this.Bind<ISensor>().To<DoorSensor>();
            this.Bind<ISensor>().To<SerotoninSensor>();
            this.Bind<ISensor>().To<BlackHoleSensor>();
        }
    }
}