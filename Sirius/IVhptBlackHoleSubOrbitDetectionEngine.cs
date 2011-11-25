namespace bootstrapper.sample.Sirius
{
    using System;
    using System.Reactive.Linq;

    public interface IVhptBlackHoleSubOrbitDetectionEngine
    {
        event EventHandler BlackHoleDetected;
    }

    public class VhptBlackHoleSubOrbitDetectionEngine : IVhptBlackHoleSubOrbitDetectionEngine, IDisposable
    {
        private readonly IDisposable engine;
        private bool isDisposed;

        public VhptBlackHoleSubOrbitDetectionEngine()
        {
            this.engine =
                Observable.Start(() => Observable.Interval(TimeSpan.FromSeconds(10)).First()).Subscribe(
                    interval => this.OnBlackHoleDetected(EventArgs.Empty));
        }

        public event EventHandler BlackHoleDetected;

        private void OnBlackHoleDetected(EventArgs e)
        {
            EventHandler handler = this.BlackHoleDetected;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (!this.isDisposed && disposing)
            {
                this.engine.Dispose();

                this.isDisposed = true;
            }
        }
    }
}