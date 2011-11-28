namespace bootstrapper.sample.Specs
{
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using bbv.Common.Bootstrapper;
    using bbv.Common.Bootstrapper.Configuration;

    public class ExtensionConfigurationSectionBehaviorFactoryWithExchangedSectionLoading : DefaultExtensionConfigurationSectionBehaviorFactory, ILoadConfigurationSection
    {
        private Func<string, ExtensionConfigurationSection> sectionProvider = section => ExtensionConfigurationSectionHelper.CreateSection(new Dictionary<string, string>());

        public override ILoadConfigurationSection CreateLoadConfigurationSection(IExtension extension)
        {
            return this;
        }

        public void SetSection(Func<string, ExtensionConfigurationSection> sectionProvider)
        {
            this.sectionProvider = sectionProvider;
        }

        public ConfigurationSection GetSection(string sectionName)
        {
            return this.sectionProvider(sectionName);
        }
    }
}