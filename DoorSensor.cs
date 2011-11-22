namespace bootstrapper.sample
{
    using System;

    using bootstrapper.sample.Sirius;

    public class DoorSensor : AbstractSensor
    {
        private readonly IVhptDoor door;

        public DoorSensor(IVhptDoor door)
        {
            this.door = door;
        }

        public override string Name
        {
            get { return "Door Sensor"; }
        }

        protected override void StartObservationCore()
        {
            this.door.Opened += this.HandleDoorOpened;
            this.door.Closed += this.HandleDoorClosed;
        }

        protected override void StopObservationCore()
        {
            this.door.Opened -= this.HandleDoorOpened;
            this.door.Closed -= this.HandleDoorClosed;
        }

        public override string Describe()
        {
            return "The door sensor detects opening and closing of doors";
        }

        private void HandleDoorOpened(object sender, EventArgs e)
        {
            Console.WriteLine("{0}: Door is open!", this.Name);
        }

        private void HandleDoorClosed(object sender, EventArgs e)
        {
            Console.WriteLine("{0}: Door is closed!", this.Name);
        }
    }
}