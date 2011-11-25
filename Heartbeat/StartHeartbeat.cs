namespace bootstrapper.sample.Heartbeat
{
    using System;
    using System.Collections.Generic;
    using bootstrapper.sample.Sirius;

    public class StartHeartbeat : IStartHeartbeat
    {
        private readonly IVhptHeartbeatGenerator generator;

        public StartHeartbeat(IVhptHeartbeatGenerator generator)
        {
            this.generator = generator;
        }

        public string Name
        {
            get { return "Start heartbeat behavior"; }
        }

        public void Behave(IEnumerable<ISensor> extensions)
        {
            Console.WriteLine("Starting heartbeat...");

            this.generator.Start();

            Console.WriteLine("Heartbeat started.");
        }

        public string Describe()
        {
            return "Starts the heartbeat";
        }
    }
}