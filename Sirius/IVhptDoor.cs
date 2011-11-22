namespace bootstrapper.sample.Sirius
{
    using System;
    using System.Reactive.Linq;

    public interface IVhptDoor : IDisposable
    {
        event EventHandler Opened;

        event EventHandler Closed;
    }

    public class VhptDoor : IVhptDoor
    {
        private readonly IDisposable observer;

        public event EventHandler Opened = delegate { };

        public event EventHandler Closed = delegate { };

        public VhptDoor()
        {
            var doorIsOpen = from interval in Observable.Interval(TimeSpan.FromSeconds(1))
                             select Convert.ToBoolean(interval % 2);

            this.observer = doorIsOpen.Subscribe(value =>
            {
                if (value)
                {
                    this.Opened(this, EventArgs.Empty);
                }
                else
                {
                    this.Closed(this, EventArgs.Empty);
                }
            });
        }

        public void Dispose()
        {
            this.observer.Dispose();
        }
    }
}