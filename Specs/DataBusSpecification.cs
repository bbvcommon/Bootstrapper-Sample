namespace bootstrapper.sample.Specs
{
    using Machine.Specifications;
    using Sirius;

    public class DataBusSpecification : MessageBusSpecification
    {
        [Rebind]
        private static IVhptDataBus DataBusMock;

        protected static FakeVhptDataBus DataBus;

        Establish context = () =>
        {
            DataBus = new FakeVhptDataBus();
            DataBusMock = DataBus;

            Bootstrapper.Scan<DataBusSpecification>();
        };
    }
}