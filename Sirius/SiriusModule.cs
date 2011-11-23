namespace bootstrapper.sample.Sirius
{
    using Ninject.Modules;

    public class SiriusModule : NinjectModule
    {
        public override void Load()
        {
            this.Bind<IVhptDataBus>().To<VhptDataBus>().InSingletonScope();
            this.Bind<IVphtMessageBus>().To<VhptMessageBus>().InSingletonScope();
            this.Bind<IVhptDoor>().To<VhptDoor>().InSingletonScope();
        }
    }
}