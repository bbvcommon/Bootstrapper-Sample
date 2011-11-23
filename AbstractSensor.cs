namespace bootstrapper.sample
{
    using bootstrapper.sample.Sirius;

    public abstract class AbstractSensor : ISensor
    {
        public abstract string Name { get; }

        public abstract string Describe();

        public IVphtMessageBus MessageBus { get; private set; }

        public IVhptDataBus DataBus { get; private set; }

        public void MessageBusInitialized(IVphtMessageBus messageBus)
        {
            this.MessageBus = messageBus;
            this.MessageBusInitializedCore(this.MessageBus);
        }

        public void DataBusInitialized(IVhptDataBus dataBus)
        {
            this.DataBus = dataBus;
            this.DataBusInitializedCore(this.DataBus);
        }

        public void Initialize()
        {
            this.InitializeCore();
        }

        public void StartObservation()
        {
            this.StartObservationCore();
        }

        public void StopObservation()
        {
            this.StopObservationCore();
        }

        protected virtual void MessageBusInitializedCore(IVphtMessageBus messageBus)
        {
        }

        protected virtual void DataBusInitializedCore(IVhptDataBus dataBus)
        {
        }

        protected virtual void InitializeCore()
        {
        }

        protected virtual void StartObservationCore()
        {
        }

        protected virtual void StopObservationCore()
        {
        }
    }
}