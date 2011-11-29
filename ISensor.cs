namespace bootstrapper.sample
{
    using bbv.Common.Bootstrapper;
    using bootstrapper.sample.Sirius;

    // HINT : The order of the methods is the order of execution.
    public interface ISensor : IExtension
    {
        void MessageBusInitialized(IVphtMessageBus messageBus);

        void DataBusInitialized(IVhptDataBus dataBus);

        void Initialize();

        void StartObservation();

        void StopObservation();
    }
}