namespace bootstrapper.sample
{
    using bbv.Common.Bootstrapper;

    public interface ISensor : IExtension
    {
        void StartObservation();

        void StopObservation();
    }
}