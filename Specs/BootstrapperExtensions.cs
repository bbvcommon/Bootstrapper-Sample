namespace bootstrapper.sample.Specs
{
    using System;

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
    }
}