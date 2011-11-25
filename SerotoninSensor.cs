namespace bootstrapper.sample
{
    using System.Collections.Generic;
    using bootstrapper.sample.Sirius;

    public class SerotoninSensor : AbstractSensor, IVhptHeartbeatAware, IVphtHandleMessage<BlackHoleDetected>
    {
        private readonly Dictionary<int, string> knownSerotoninLevels =
            new Dictionary<int, string>
                {
                    { 60, "Mouse (orally)" },
                    { 81, "Mouse (intravenously)" },
                    { 30, "Rat (intravenously)" },
                    { 13, "guinea pig (intravenously)" },
                    { 5, "Cat (intravenously)" },
                    { 150, "Raver on ecstasy (intravenously)" },
                };

        private readonly IVphtSerotoninProbe probe;

        public SerotoninSensor(IVphtSerotoninProbe probe)
        {
            this.probe = probe;
        }

        public override string Name
        {
            get { return "Serotonin Sensor"; }
        }

        public bool InPanicMode { get; private set; }

        public bool PanicModeEnabled { get; private set; }

        public override string Describe()
        {
            return "Measures the serotinin level of the people transportet.";
        }

        protected override void MessageBusInitializedCore(IVphtMessageBus messageBus)
        {
            if (this.PanicModeEnabled)
            {
                messageBus.Subscribe(this);
            }
        }

        public void Bumm()
        {
            if (this.InPanicMode)
            {
                this.DataBus.SendAsync(Data.New(this.Name, "We are going to die anyways! No need to detect serotonin level!"));
                return;
            }

            int level = this.probe.Take();

            string message;
            if (!this.knownSerotoninLevels.TryGetValue(level, out message))
            {
                message = "You are not happy enought!";
            }

            this.DataBus.SendAsync(Data.New(this.Name, string.Format("Serotonin level of people transported is that of a {0}.", message)));
        }

        public void Handle(BlackHoleDetected message)
        {
            this.InPanicMode = true;
            this.DataBus.SendAsync(Data.New(this.Name, "We are going to die!"));
        }
    }
}