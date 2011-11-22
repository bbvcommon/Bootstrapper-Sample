namespace bootstrapper.sample
{
    public abstract class AbstractSensor : ISensor
    {
        public abstract string Name { get; }

        public abstract string Describe();

        public void StartObservation()
        {
            this.StartObservationCore();
        }

        public void StopObservation()
        {
            this.StopObservationCore();
        }

        protected virtual void StartObservationCore()
        {
        }

        protected virtual void StopObservationCore()
        {
        }
    }
}