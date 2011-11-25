namespace bootstrapper.sample.Heartbeat
{
    using Ninject.Modules;

    public class HeartbeatModule : NinjectModule
    {
        public override void Load()
        {
            this.Bind<IStartHeartbeat>().To<StartHeartbeat>();
            this.Bind<IStopHeartbeat>().To<StopHeartbeat>();
            this.Bind<IMakeAwareOfHeartbeat>().To<MakeAwareOfHeartbeat>();
        }
    }
}