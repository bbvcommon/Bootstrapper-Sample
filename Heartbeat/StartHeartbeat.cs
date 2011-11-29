namespace bootstrapper.sample.Heartbeat
{
    using System;
    using System.Collections.Generic;
    using bootstrapper.sample.Sirius;

    // Hint: Implement here
    public class StartHeartbeat : IStartHeartbeat
    {
        private readonly IVhptHeartbeatGenerator generator;

        public StartHeartbeat(IVhptHeartbeatGenerator generator)
        {
            this.generator = generator;
        }

        public void Behave(IEnumerable<ISensor> extensions)
        {
            throw new NotImplementedException();
        }

        public string Describe()
        {
            throw new NotImplementedException();
        }

        public string Name
        {
            get { throw new NotImplementedException(); }
        }
    }
}