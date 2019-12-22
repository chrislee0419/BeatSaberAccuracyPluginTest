using HMUI;
using Harmony;

namespace TestBeatSaberPlugin.HarmonyPatches
{
    [HarmonyPatch(typeof(EnvironmentOverrideSettingsPanelController))]
    [HarmonyPatch("HandleDropDownDidSelectCellWithIdx", MethodType.Normal)]
    class SelectOverrideEnvironmentDropdownPatch
    {
        public static void Prefix(EnvironmentsListSO ____allEnvironments, DropdownWithTableView dropDownWithTableView, int idx)
        {
            var allEnvs = ____allEnvironments.environmentInfos;

            Logger.log.Notice("Logging all environments...");
            foreach (var env in allEnvs)
            {
                Logger.log.Notice($"\tenvName={env.environmentName}, serializedName={env.serializedName}, envType.name={env.environmentType.name}, envType.localizationKey{env.environmentType.typeNameLocalizationKey}, sceneInfo.sceneName={env.sceneInfo.sceneName}");
            }

            // current env selected
            LabelAndValueDropdownWithTableView labelAndValueDropdownWithTableView = dropDownWithTableView as LabelAndValueDropdownWithTableView;
            EnvironmentInfoSO environmentInfoSO = (dropDownWithTableView.tableViewDataSource as EnvironmentsTableViewDataSource).environmentInfos[idx];

            Logger.log.Notice($"Current override environment selected = {environmentInfoSO.environmentName}");
        }
    }

    [HarmonyPatch(typeof(MenuTransitionsHelper))]
    [HarmonyPatch("StartBeatmapEditor", MethodType.Normal)]
    class LoadBeatmapEditorPatch
    {
        public static void Prefix(BeatmapEditorScenesTransitionSetupDataSO ____beatmapEditorScenesTransitionSetupData)
        {
            //foreach (var dataPair in ____beatmapEditorScenesTransitionSetupData.sceneInfoSceneSetupDataPairs)
            //{
            //    dataPair.data
            //}
        }

    }
}
