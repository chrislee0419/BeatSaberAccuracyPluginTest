using System;
using System.Reflection;
using TMPro;
using UnityEngine;
using HMUI;
using BeatSaberMarkupLanguage;
using BeatSaberMarkupLanguage.Components;
using BeatSaberMarkupLanguage.Attributes;
using BeatSaberMarkupLanguage.ViewControllers;

namespace TestBeatSaberPlugin.UI
{
    internal class TestViewController : BSMLResourceViewController
    {
        public override string ResourceName => "TestBeatSaberPlugin.UI.Views.TestView.bsml";

#pragma warning disable CS0649
#pragma warning disable CS0414
        [UIComponent("some-text")]
        private TextMeshProUGUI text;
#pragma warning restore CS0649
#pragma warning restore CS0414

        [UIAction("press")]
        private void ButtonPress()
        {
            text.text = "Hey look, the text changed";
        }
    }

    internal class Test2ViewController : BSMLResourceViewController
    {
        public event Action<GameObject> CellWasSelected;

        public override string ResourceName => "TestBeatSaberPlugin.UI.Views.ListTestView.bsml";

#pragma warning disable CS0649
#pragma warning disable CS0414
        [UIComponent("test-list")]
        private CustomListTableData customList;

        [UIValue("topleft-text")]
        private static readonly string text = "This is some <color=#FF1111>example text</color> that should appear in a <color=#3333FF>box</color>.";
#pragma warning restore CS0649
#pragma warning restore CS0414

        private GameObject[] _viewContents = new GameObject[] { null, null, null, null };

        protected override void DidActivate(bool firstActivation, ActivationType type)
        {
            base.DidActivate(firstActivation, type);

            if (firstActivation)
            {
                customList.data.Add(new CustomListTableData.CustomCellInfo("item one"));
                customList.data.Add(new CustomListTableData.CustomCellInfo("two"));
                customList.data.Add(new CustomListTableData.CustomCellInfo("third"));
                customList.data.Add(new CustomListTableData.CustomCellInfo("another one"));
                customList.tableView.ReloadData();
            }
        }

        [UIAction("select-cell")]
        private void CellSelected(TableView tableView, int index)
        {
            if (_viewContents[index] == null)
            {
                GameObject container = new GameObject("TestContentContainer");
                BSMLParser.instance.Parse(Utilities.GetResourceContent(Assembly.GetExecutingAssembly(), $"TestBeatSaberPlugin.UI.Views.Content{index+1}View.bsml"), container, this);

                _viewContents[index] = container;
            }

            CellWasSelected?.Invoke(_viewContents[index]);
        }
    }

    internal class Test3ViewController : BSMLResourceViewController
    {
        public override string ResourceName => "TestBeatSaberPlugin.UI.Views.DisplayTestView.bsml";

#pragma warning disable CS0649
#pragma warning disable CS0414
        [UIObject("container")]
        private GameObject container;
#pragma warning restore CS0649
#pragma warning restore CS0414

        private GameObject _currentView;

        public void SetContent(GameObject content)
        {
            if (_currentView != null)
                _currentView.SetActive(false);

            content.transform.SetParent(container.transform, false);
            content.SetActive(true);

            _currentView = content;
        }
    }

    internal class TestViewFlowCoordinator : FlowCoordinator
    {
        public event Action BackButtonPressed;

        protected override void DidActivate(bool firstActivation, ActivationType activationType)
        {
            if (firstActivation)
            {
                title = "Sandbox Area";
                showBackButton = true;

                var testViewController = BeatSaberUI.CreateViewController<TestViewController>();
                var test2ViewController = BeatSaberUI.CreateViewController<Test2ViewController>();
                var test3ViewController = BeatSaberUI.CreateViewController<Test3ViewController>();

                test2ViewController.CellWasSelected += (content) => test3ViewController.SetContent(content);

                ProvideInitialViewControllers(testViewController, test2ViewController, test3ViewController);
            }
        }

        protected override void BackButtonWasPressed(ViewController topViewController)
        {
            BackButtonPressed?.Invoke();
        }
    }
}
