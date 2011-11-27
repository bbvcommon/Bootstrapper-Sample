namespace bootstrapper.sample.Specs
{
    using Machine.Specifications;
    using Magic;
    using Moq;
    using Sirius;

    public class BlackHoleSpecification : DataBusSpecification
    {
        [Rebind]
        protected static Mock<IVhptBlackHoleSubOrbitDetectionEngine> BlackHoleEngine;

        Establish context = () =>
        {
            BlackHoleEngine = new Mock<IVhptBlackHoleSubOrbitDetectionEngine>();

            Bootstrapper.Ignore(typeof(IDoorSensor));
            Bootstrapper.Scan<BlackHoleSpecification>();
        };
    }
}