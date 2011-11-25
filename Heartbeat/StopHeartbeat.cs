namespace bootstrapper.sample.Heartbeat
{
    using System;
    using System.Collections.Generic;
    using bootstrapper.sample.Sirius;

    public class StopHeartbeat : IStopHeartbeat
    {
        private readonly IVhptHeartbeatGenerator generator;

        public StopHeartbeat(IVhptHeartbeatGenerator generator)
        {
            this.generator = generator;
        }

        public string Name
        {
            get { return "Stop heartbeat behavior"; }
        }

        public void Behave(IEnumerable<ISensor> extensions)
        {
            Console.WriteLine("Stopping heartbeat...");

            this.generator.Stop();

            Console.WriteLine("Heartbeat stopped.");
        }

        public string Describe()
        {
            return "Stops the heartbeat";
        }
    }
}