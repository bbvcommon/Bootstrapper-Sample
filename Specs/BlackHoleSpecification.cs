namespace bootstrapper.sample.Specs
{
    using Machine.Specifications;
    using Moq;
    using Sirius;

    public class BlackHoleSpecification : DataBusSpecification
    {
        [Rebind]
        protected static Mock<IVhptBlackHoleSubOrbitDetectionEngine> BlackHoleEngine;

        Establish context = () =>
        {
            BlackHoleEngine = new Mock<IVhptBlackHoleSubOrbitDetectionEngine>();

            Bootstrapper.Scan<BlackHoleSpecification>();
        };
    }
}