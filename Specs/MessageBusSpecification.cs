namespace bootstrapper.sample.Specs
{
    using Fakes;
    using Machine.Specifications;
    using Magic;
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