namespace bootstrapper.sample
{
    using Ninject.Modules;

    public class SensorModule : NinjectModule
    {
        public override void Load()
        {
            this.Bind<ISensor>().To<DoorSensor>().InSingletonScope();
        }
    }
}