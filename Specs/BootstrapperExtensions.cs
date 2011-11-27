namespace bootstrapper.sample.Specs
{
    using System;
    using System.Linq;
    using Magic;

    public static class BootstrapperExtensions
    {
        public static void Using(this DecoratedBootstrapper bootstrapper, Action action)
        {
            bootstrapper.Run();

            try
            {
                action();
            }
            finally
            {
                bootstrapper.Shutdown();
                bootstrapper.Dispose();
            }
        }

        public static void Scan<TSpecification>(this DecoratedBootstrapper bootstrapper)
            where TSpecification : InitializedBootstrapperSpecification
        {
            bootstrapper.AddExtension(new RebindFieldsWithRebindAttribute<TSpecification>());
            bootstrapper.AddExtension(new InjectFieldsWithInjectAttribute<TSpecification>());
        }

        public static void Ignore(this DecoratedBootstrapper bootstrapper, params Type[] sensorType)
        {
            bootstrapper.SetSelector(sensor =>
                                         {
                                             Type sensorInterface = sensor.GetType().GetInterfaces().Where(
                                                 @interface =>
                                                 @interface != typeof(ISensor) &&
                                                 typeof(ISensor).IsAssignableFrom(@interface)).SingleOrDefault();

                                             return sensorInterface == null || !sensorType.Contains(sensorInterface);
                                         });
        }
    }
}