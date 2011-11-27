namespace bootstrapper.sample.Specs
{
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using Sirius;

    public class FakeVhptDataBus : IVhptDataBus
    {
        private readonly ConcurrentQueue<Data> dataQueue;

        public FakeVhptDataBus()
        {
            this.dataQueue = new ConcurrentQueue<Data>();
        }

        public IEnumerable<Data> Sent
        {
            get { return this.dataQueue; }
        }

        public void SendAsync(Data data)
        {
            this.dataQueue.Enqueue(data);
        }

        public void Dispose()
        {
        }
    }
}