using System.Reflection;
using System.Linq;
using UnityEngine;
using BeatSaberMarkupLanguage;
using BeatSaberMarkupLanguage.Attributes;

namespace TestBeatSaberPlugin.UI
{
    internal class MainMenuViewControllerEditor : PersistentSingleton<MainMenuViewControllerEditor>
    {
        private TestViewFlowCoordinator _testViewFlowCoordinator;

        private bool _screenShown = false;

        internal void Setup()
        {
            var mainMenuViewController = Resources.FindObjectsOfTypeAll<MainMenuViewController>().First();
            BSMLParser.instance.Parse(Utilities.GetResourceContent(Assembly.GetExecutingAssembly(), "TestBeatSaberPlugin.UI.Views.MainMenu.bsml"), mainMenuViewController.gameObject, this);
        }

        [UIAction("testview-button-click")]
        private void ShowTestViewButtonClicked()
        {
            if (_testViewFlowCoordinator == null)
            {
                _testViewFlowCoordinator = BeatSaberUI.CreateFlowCoordinator<TestViewFlowCoordinator>();
                _testViewFlowCoordinator.BackButtonPressed += () => BeatSaberUI.MainFlowCoordinator.DismissFlowCoordinator(_testViewFlowCoordinator);
            }

            BeatSaberUI.MainFlowCoordinator.PresentFlowCoordinator(_testViewFlowCoordinator);
        }

        [UIAction("screen-button-click")]
        private void ToggleScreenButtonClicked()
        {
            if (_screenShown)
                ScreenTest.instance.HideScreen();
            else
                ScreenTest.instance.ShowScreen();

            _screenShown = !_screenShown;
        }
    }
}
