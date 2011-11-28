namespace bootstrapper.sample.Specs
{
    using System;
    using System.Linq;
    using FluentAssertions;
    using Machine.Specifications;

    public class when_black_hole_detected_and_panic_mode_enabled : BlackHoleSpecification
    {
        private static ISerotoninSensor SerotoninSensor;

        Establish context = () =>
        {
        };

        Because of = () =>
        {
            Bootstrapper.Using(() =>
            {
                SerotoninSensor = Bootstrapper.OfType<ISerotoninSensor>().Single();

                BlackHoleEngine.Raise(x => x.BlackHoleDetected += null, EventArgs.Empty);
            });
        };

        It should_not_enter_panic_mode_ = () =>
        {
            SerotoninSensor.InPanicMode.Should().BeTrue();
        };

        It should_not_indicate_on_databus_panic_mode = () =>
        {
            DataBus.Sent.Should().Contain(d => d.Originator == "Serotonin Sensor" && d.Content.EndsWith("We are going to die!"));
        };

        It should_indicate_disabled_panic_mode = () =>
        {
            SerotoninSensor.Should().NotBeNull();
            SerotoninSensor.PanicModeEnabled.Should().BeTrue();
        };
    }
}