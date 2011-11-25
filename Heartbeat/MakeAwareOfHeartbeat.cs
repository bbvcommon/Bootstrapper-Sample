namespace bootstrapper.sample.Heartbeat
{
    using System.Collections.Generic;
    using System.Linq;
    using bbv.Common.Bootstrapper;
    using bootstrapper.sample.Sirius;

    public interface IMakeAwareOfHeartbeat : IBehavior<ISensor>
    {
    }

    public class MakeAwareOfHeartbeat : IMakeAwareOfHeartbeat
    {
        private readonly IVhptHeartbeatGenerator generator;

        public MakeAwareOfHeartbeat(IVhptHeartbeatGenerator generator)
        {
            this.generator = generator;
        }

        public void Behave(IEnumerable<ISensor> extensions)
        {
            foreach (IVhptHeartbeatAware aware in extensions.OfType<IVhptHeartbeatAware>())
            {
                this.generator.MakeAwareOf(aware);
            }
        }

        public string Describe()
        {
            return "Makes all sensors which require the heartbeat aware of it.";
        }

        public string Name
        {
            get { return "Make aware of heartbeat behavior"; }
        }
    }
}