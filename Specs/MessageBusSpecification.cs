namespace bootstrapper.sample.Specs
{
    using Machine.Specifications;
    using Sirius;

    public class MessageBusSpecification : InitializedBootstrapperSpecification
    {
        [Rebind]
        private static IVphtMessageBus MessageBusMock;

        Establish context = () =>
            {
                MessageBusMock = new FakeVhptMessageBus();

                Bootstrapper.Scan<MessageBusSpecification>();
            };
    }
}