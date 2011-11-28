namespace bootstrapper.sample.Specs
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using bbv.Common.Bootstrapper.Configuration;
    using FluentAssertions;
    using Machine.Specifications;
    using Magic;

    public class when_black_hole_detected_and_panic_mode_disabled : BlackHoleSpecification
    {
        [Rebind]
        private static IExtensionConfigurationSectionBehaviorFactory FactoryWithExchangedSectionLoading;

        private static ISerotoninSensor SerotoninSensor;

        Establish context = () =>
            {
                var factory = new ExtensionConfigurationSectionBehaviorFactoryWithExchangedSectionLoading();
                factory.SetSection(name => ExtensionConfigurationSectionHelper.CreateSection(new KeyValuePair<string, string>("PanicModeEnabled", "false")));

                FactoryWithExchangedSectionLoading = factory;

                Bootstrapper.Scan<when_black_hole_detected_and_panic_mode_disabled>();
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
                SerotoninSensor.InPanicMode.Should().BeFalse();
            };

        It should_not_indicate_on_databus_panic_mode = () =>
            {
                DataBus.Sent.Should().NotContain(d => d.Originator == "Serotonin Sensor" && d.Content.EndsWith("We are going to die!"));
            };

        It should_indicate_disabled_panic_mode = () =>
            {
                SerotoninSensor.Should().NotBeNull();
                SerotoninSensor.PanicModeEnabled.Should().BeFalse();
            };
    }
}