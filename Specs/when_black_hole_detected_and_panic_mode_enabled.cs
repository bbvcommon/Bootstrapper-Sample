namespace bootstrapper.sample.Specs
{
    using System;
    using FluentAssertions;
    using Machine.Specifications;

    public class when_black_hole_detected_and_panic_mode_enabled : BlackHoleSpecification
    {
        Establish context = () =>
        {
        };

        Because of = () =>
        {
            Bootstrapper.Using(() =>
            {
                BlackHoleEngine.Raise(x => x.BlackHoleDetected += null, EventArgs.Empty);
            });
        };

        It should_enter_panic_mode_ = () =>
        {
            DataBus.Sent.Should().Contain(d => d.Originator == "Serotonin Sensor" && d.Content.EndsWith("We are going to die!"));
        };
    }
}