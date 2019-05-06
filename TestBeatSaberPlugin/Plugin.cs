using IPA;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using IPALogger = IPA.Logging.Logger;

namespace TestBeatSaberPlugin
{
    internal struct HitScore
    {
        public int beforeCut;
        public int afterCut;
        public int cutDistance;

        public HitScore(int beforeCut, int afterCut, int cutDistance)
        {
            this.beforeCut = beforeCut;
            this.afterCut = afterCut;
            this.cutDistance = cutDistance;
        }
    }

    public class Plugin : IBeatSaberPlugin
    {
        public string Name => "AccuracyAverageTestPlugin";
        public string Version => "0.0.1";

        internal static List<HitScore> lSaberCut = new List<HitScore>();
        internal static List<HitScore> rSaberCut = new List<HitScore>();

        public void Init(IPALogger logger)
        {
            Logger.log = logger;
        }

        public void OnApplicationStart()
        {

        }

        public void OnApplicationQuit()
        {

        }

        public void OnFixedUpdate()
        {

        }

        public void OnUpdate()
        {

        }

        public void OnActiveSceneChanged(Scene prevScene, Scene nextScene)
        {
            if (!PluginConfig.enabled)
            {
                return;
            }
            else if (nextScene.name == "MenuCore")
            {
                if (prevScene.name == "GameCore")
                {
                    new GameObject("Results Viewer").AddComponent<ResultsViewer>();
                    Logger.log.Debug("OnActiveSceneChanged from the test plugin has created a ResultsViewer object");
                }
            }
            else if (nextScene.name == "GameCore")
            {
                new GameObject("Accuracy Lister").AddComponent<AccuracyLists>();
                Logger.log.Debug("OnActiveSceneChanged from the test plugin has created an AccuracyLists object");
            }
        }

        public void OnSceneLoaded(Scene scene, LoadSceneMode sceneMode)
        {
            if (scene.name == "MenuCore")
                BasicUI.CreateGameplayOptionsUI();
        }

        public void OnSceneUnloaded(Scene scene)
        {

        }
    }
}
