namespace bootstrapper.sample
{
    using Ninject.Modules;

    public class SensorModule : NinjectModule
    {
        public override void Load()
        {
            this.Bind<ISensor>().To<DoorSensor>();
            this.Bind<ISensor>().To<SerotoninSensor>();
            this.Bind<ISensor>().To<BlackHoleSensor>();
        }
    }
}