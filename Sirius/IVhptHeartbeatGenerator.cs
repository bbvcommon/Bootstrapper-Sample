namespace bootstrapper.sample.Sirius
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reactive.Concurrency;
    using System.Reactive.Linq;
    using System.Threading.Tasks;

    public interface IVhptHeartbeatGenerator
    {
        void Start();

        void MakeAwareOf(IVhptHeartbeatAware instance);

        void Stop();
    }

    public class VhptHeartbeatGenerator : IVhptHeartbeatGenerator
    {
        private IDisposable generator;

        private readonly List<AwareDecorator> decorated = new List<AwareDecorator>();
        private readonly object locker = new object();

        public void Start()
        {
            this.generator = Observable.Interval(TimeSpan.FromSeconds(5))
                .SubscribeOn(Scheduler.TaskPool)
                .Subscribe(intervall => this.Beat());
        }

        public void MakeAwareOf(IVhptHeartbeatAware instance)
        {
            lock (this.locker)
            {
                this.decorated.Add(new AwareDecorator(instance));
            }
        }

        public void Stop()
        {
            this.generator.Dispose();
        }

        private void Beat()
        {
            IVhptHeartbeatAware[] awares;
            lock (this.locker)
            {
                var notInterested = this.decorated.Where(d => !d.Interested).ToList();
                notInterested.ForEach(x => this.decorated.Remove(x));

                awares = this.decorated.ToArray();
            }

            Parallel.ForEach(awares, aware => aware.Bumm());
        }

        private class AwareDecorator : IVhptHeartbeatAware
        {
            private readonly WeakReference target;

            public AwareDecorator(IVhptHeartbeatAware decorated)
            {
                this.target = new WeakReference(decorated);
            }

            public void Bumm()
            {
                var decorated = (IVhptHeartbeatAware)this.target.Target;
                if (decorated != null)
                {
                    decorated.Bumm();
                }
            }

            public bool Interested
            {
                get { return this.target.IsAlive; }
            }

        }
    }

    public interface IVhptHeartbeatAware
    {
        void Bumm();
    }


}