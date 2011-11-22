namespace bootstrapper.sample
{
    using System;

    using bbv.Common.Bootstrapper;

    using bootstrapper.sample.Sirius;

    public static class Program
    {
        public static void Main(string[] args)
        {
            using (var bootstrapper = new DefaultBootstrapper<ISensor>())
            {
                bootstrapper.Initialize(new SensorLifetimeStrategy());
                bootstrapper.AddExtension(new DoorSensor(new VhptDoor()));

                PrintHeader();

                bootstrapper.Run();

                PrintBody();

                bootstrapper.Shutdown();

                PrintFooter();
            }
        }

        private static void PrintHeader()
        {
            Console.WriteLine("Sirius Cybernetics Sensor Management v1.0");
            Console.WriteLine("Techday 2011, bbv Software Services Ag");
            Console.WriteLine("=========================================================");
            Console.WriteLine("=========================================================");
            Console.WriteLine("Press any key to start sensors");
            Console.WriteLine("---------------------------------------------------------");
            Console.ReadLine();
        }

        private static void PrintBody()
        {
            Console.WriteLine("---------------------------------------------------------");
            Console.WriteLine("Press any key to stop sensors");
            Console.ReadLine();
        }

        private static void PrintFooter()
        {
            Console.WriteLine("---------------------------------------------------------");
            Console.WriteLine("Press any key to exit the sensor management.");
            Console.ReadLine();
        }
    }
}
