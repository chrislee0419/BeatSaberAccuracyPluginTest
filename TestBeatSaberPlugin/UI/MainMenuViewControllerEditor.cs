using System.Reflection;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using BeatSaberMarkupLanguage;
using BeatSaberMarkupLanguage.Attributes;

namespace TestBeatSaberPlugin.UI
{
    internal class MainMenuViewControllerEditor : PersistentSingleton<MainMenuViewControllerEditor>
    {
        private TestViewFlowCoordinator _testViewFlowCoordinator;

        internal void Setup()
        {
            var mainMenuViewController = Resources.FindObjectsOfTypeAll<MainMenuViewController>().First();
            BSMLParser.instance.Parse(Utilities.GetResourceContent(Assembly.GetExecutingAssembly(), "TestBeatSaberPlugin.UI.Views.MainMenu.bsml"), mainMenuViewController.gameObject, this);
        }

        [UIAction("button-click")]
        private void ButtonClicked()
        {
            if (_testViewFlowCoordinator == null)
            {
                _testViewFlowCoordinator = BeatSaberUI.CreateFlowCoordinator<TestViewFlowCoordinator>();
                _testViewFlowCoordinator.BackButtonPressed += () => BeatSaberUI.MainFlowCoordinator.DismissFlowCoordinator(_testViewFlowCoordinator);
            }

            BeatSaberUI.MainFlowCoordinator.PresentFlowCoordinator(_testViewFlowCoordinator);
        }
    }
}
