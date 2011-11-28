namespace bootstrapper.sample
{
    public interface ISerotoninSensor : ISensor
    {
        bool InPanicMode { get; }

        bool PanicModeEnabled { get; }
    }
}