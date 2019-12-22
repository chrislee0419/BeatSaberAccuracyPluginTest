using CustomUI.GameplaySettings;

namespace TestBeatSaberPlugin
{
    class BasicUI
    {
        public static void CreateGameplayOptionsUI()
        {
            var pluginSubmenu = GameplaySettingsUI.CreateSubmenuOption(GameplaySettingsPanels.ModifiersLeft, "Accuracy Averages", "MainMenu", "accAverage");

            var enabled = GameplaySettingsUI.CreateToggleOption(GameplaySettingsPanels.ModifiersLeft, "Enabled", "accAverage", "Toggles the plugin");
            enabled.GetValue = PluginConfig.enabled;
            enabled.OnToggle += (value) => { PluginConfig.enabled = value; };

            var accDisplayBoth = GameplaySettingsUI.CreateToggleOption(GameplaySettingsPanels.ModifiersLeft, "Accuracy - Display Both", "accAverage", "Show the combined average accuracy for both sabers");
            accDisplayBoth.GetValue = PluginConfig.accDisplayBoth;
            accDisplayBoth.OnToggle += (value) => { PluginConfig.accDisplayBoth = value; };

            var accDisplaySeparate = GameplaySettingsUI.CreateToggleOption(GameplaySettingsPanels.ModifiersLeft, "Accuracy - Display Separate", "accAverage", "Show the average accuracy for each saber");
            accDisplaySeparate.GetValue = PluginConfig.accDisplaySeparate;
            accDisplaySeparate.OnToggle += (value) => { PluginConfig.accDisplaySeparate = value; };

            var prehitAngleDisplayBoth = GameplaySettingsUI.CreateToggleOption(GameplaySettingsPanels.ModifiersLeft, "Pre-hit - Display Both", "accAverage", "Show the combined average pre-hit angle score");
            prehitAngleDisplayBoth.GetValue = PluginConfig.prehitAngleDisplayBoth;
            prehitAngleDisplayBoth.OnToggle += (value) => { PluginConfig.prehitAngleDisplayBoth = value; };

            var prehitAngleDisplaySeparate = GameplaySettingsUI.CreateToggleOption(GameplaySettingsPanels.ModifiersLeft, "Pre-hit - Display Separate", "accAverage", "Show the average pre-hit angle score for each saber");
            prehitAngleDisplaySeparate.GetValue = PluginConfig.prehitAngleDisplaySeparate;
            prehitAngleDisplaySeparate.OnToggle += (value) => { PluginConfig.prehitAngleDisplaySeparate = value; };

            var posthitAngleDisplayBoth = GameplaySettingsUI.CreateToggleOption(GameplaySettingsPanels.ModifiersLeft, "Followthrough - Display Both", "accAverage", "Show the combined average followthrough angle score");
            posthitAngleDisplayBoth.GetValue = PluginConfig.posthitAngleDisplayBoth;
            posthitAngleDisplayBoth.OnToggle += (value) => { PluginConfig.posthitAngleDisplayBoth = value; };

            var posthitAngleDisplaySeparate = GameplaySettingsUI.CreateToggleOption(GameplaySettingsPanels.ModifiersLeft, "Followthrough - Display Separate", "accAverage", "Show the average followthrough angle score for each saber");
            posthitAngleDisplaySeparate.GetValue = PluginConfig.posthitAngleDisplaySeparate;
            posthitAngleDisplaySeparate.OnToggle += (value) => { PluginConfig.posthitAngleDisplaySeparate = value; };
        }
    }
}
