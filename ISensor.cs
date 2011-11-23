namespace bootstrapper.sample
{
    using bbv.Common.Bootstrapper;
    using bootstrapper.sample.Sirius;

    public interface ISensor : IExtension
    {
        void MessageBusInitialized(IVphtMessageBus messageBus);

        void DataBusInitialized(IVhptDataBus dataBus);

        void Initialize();

        void StartObservation();

        void StopObservation();
    }
}