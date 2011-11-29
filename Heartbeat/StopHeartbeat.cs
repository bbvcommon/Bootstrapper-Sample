namespace bootstrapper.sample.Heartbeat
{
    using System;
    using System.Collections.Generic;
    using bootstrapper.sample.Sirius;

    // HINT : Implement here
    public class StopHeartbeat : IStopHeartbeat
    {
        private readonly IVhptHeartbeatGenerator generator;

        public StopHeartbeat(IVhptHeartbeatGenerator generator)
        {
            this.generator = generator;
        }

        public string Name
        {
            get { throw new NotImplementedException(); }
        }

        public void Behave(IEnumerable<ISensor> extensions)
        {
            throw new NotImplementedException();
        }

        public string Describe()
        {
            throw new NotImplementedException();
        }
    }
}