namespace bootstrapper.sample
{
    using System;
    using bootstrapper.sample.Sirius;

    public class BlackHoleSensor : AbstractSensor, IDisposable
    {
        private readonly IVhptBlackHoleSubOrbitDetectionEngine detectionEngine;
        private bool isDisposed;

        public BlackHoleSensor(IVhptBlackHoleSubOrbitDetectionEngine detectionEngine)
        {
            this.detectionEngine = detectionEngine;
        }

        public override string Name
        {
            get { return "Black Hole Sensor"; }
        }

        protected override void StartObservationCore()
        {
            this.detectionEngine.BlackHoleDetected += this.HandleBlackHoleDetected;
        }

        protected override void StopObservationCore()
        {
            this.detectionEngine.BlackHoleDetected -= this.HandleBlackHoleDetected;
        }

        public override string Describe()
        {
            return "Detects black holes";
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
                this.DataBus.SendAsync(Data.New(this.Name, "Black hole sensor disposed!"));

                this.isDisposed = true;
            }
        }

        private void HandleBlackHoleDetected(object sender, EventArgs e)
        {
            this.MessageBus.Publish(new BlackHoleDetected());
        }
    }

    public class BlackHoleDetected
    {
    }
}