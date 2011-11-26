namespace bootstrapper.sample
{
    using bbv.Common.Bootstrapper;
    using Ninject.Modules;

    public class SensorModule : NinjectModule
    {
        public override void Load()
        {
            this.Bind<IExtensionResolver<ISensor>>().To<NinjectSensorResolver>().InSingletonScope();

            this.Bind<IDoorSensor>().To<DoorSensor>();
            this.Bind<ISerotoninSensor>().To<SerotoninSensor>();
            this.Bind<IBlackHoleSensor>().To<BlackHoleSensor>();
        }
    }
}