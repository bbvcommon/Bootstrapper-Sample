namespace bootstrapper.sample.Specs
{
    using System.Linq;
    using bbv.Common.Bootstrapper.Configuration;
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
                // HINT : Fake away configuration section with SetSection and ExtensionConfigurationSectionHelper

                FactoryWithExchangedSectionLoading = factory;

                // HINT : Scan
            };

        Because of = () =>
            {
                Bootstrapper.Using(() =>
                {
                    SerotoninSensor = Bootstrapper.OfType<ISerotoninSensor>().Single();

                    // HINT : Indicate black hole detected
                });
            };

        It should_not_enter_panic_mode_ = () =>
            {
                // HINT : Fill in here
            };

        It should_not_indicate_on_databus_panic_mode = () =>
            {
                // HINT : Fill in here
            };

        It should_indicate_disabled_panic_mode = () =>
            {
                // HINT : Fill in here
            };
    }
}