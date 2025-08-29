using ModConfigMenu;
using ModConfigMenu.Objects;
using System.Collections.Generic;

namespace QM_SortToTabs.Mcm
{
    internal class McmConfiguration : McmConfigurationBase
    {
        public McmConfiguration(ModConfig config, Logger logger) : base (config, logger) { }

        public override void Configure()
        {
            ModConfigMenuAPI.RegisterModConfig("Sort To Tabs", new List<ConfigValue>()
            {
                CreateConfigProperty(nameof(ModConfig.DebugLogMatches),"Logs all matching info to assist debugging."),
                CreateReadOnly(nameof(ModConfig.SortToTabsKey)),
                CreateReadOnly(nameof(ModConfig.TabSortKey)),
            }, OnSave);
        }
    }
}
