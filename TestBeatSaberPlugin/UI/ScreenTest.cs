using System;
using System.Collections;
using System.Linq;
using System.Reflection;
using UnityEngine;
using UnityEngine.UI;
using Image = UnityEngine.UI.Image;
using Screen = HMUI.Screen;
using BeatSaberMarkupLanguage;
using BeatSaberMarkupLanguage.Attributes;

namespace TestBeatSaberPlugin.UI
{
    class ScreenTest : PersistentSingleton<ScreenTest>
    {
        GameObject _container;

#pragma warning disable CS0649
#pragma warning disable CS0414
#pragma warning disable CS0169
        [UIValue("show-search")]
        private bool _showSearch = true;

        [UIComponent("search-button")]
        private Button _searchButton;
        [UIComponent("filter-button")]
        private Button _filterButton;
        [UIComponent("clear-filter-button")]
        private Button _clearFilterButton;
#pragma warning restore CS0649
#pragma warning restore CS0414
#pragma warning restore CS0169

        private const float DefaultYScale = 0.02f;
        private const float HiddenYScale = 0f;

        public void Setup()
        {
            var topScreen = Resources.FindObjectsOfTypeAll<GameObject>().First(x => x.name == "TopScreen");
            var mainScreen = Resources.FindObjectsOfTypeAll<GameObject>().First(x => x.name == "MainScreen");

            _container = Instantiate(topScreen, topScreen.transform.parent, true);
            Destroy(_container.GetComponentInChildren<SetMainCameraToCanvas>(true));
            Destroy(_container.transform.Find("TitleViewController"));
            Destroy(_container.GetComponentInChildren<Screen>(true));
            Destroy(_container.GetComponentInChildren<HorizontalLayoutGroup>(true));

            // position the screen
            var rt = _container.transform as RectTransform;
            rt.sizeDelta = new Vector2(28f, 30f);
            rt.pivot = new Vector2(1f, 0f);
            rt.anchorMin = new Vector2(1f, 0f);
            rt.anchorMax = new Vector2(1f, 0f);
            rt.anchoredPosition = new Vector2(1.6f, 2.44f);
            rt.localRotation = Quaternion.Euler(345f, 0f, 0f);

            BSMLParser.instance.Parse(Utilities.GetResourceContent(Assembly.GetExecutingAssembly(), "TestBeatSaberPlugin.UI.Views.ScreenTestView.bsml"), _container, this);

            // replace the ugly looking button with the marginally better looking keyboard button (RoundRectBig)
            var replacementButtonImage = Resources.FindObjectsOfTypeAll<TextMeshProButton>().First(x => x.name == "KeyboardButton").GetComponentInChildren<Image>().sprite;
            var buttonBg = _searchButton?.GetComponentInChildren<Image>();
            if (buttonBg != null)
                buttonBg.sprite = replacementButtonImage;
            buttonBg = _filterButton?.GetComponentInChildren<Image>();
            if (buttonBg != null)
                buttonBg.sprite = replacementButtonImage;
            buttonBg = _clearFilterButton?.GetComponentInChildren<Image>();
            if (buttonBg != null)
                buttonBg.sprite = replacementButtonImage;

            HideScreen(true);
        }

        [UIAction("search-button-clicked")]
        private void SearchButtonClicked()
        {
            Logger.log.Notice("search button clicked");
        }

        [UIAction("filter-button-clicked")]
        private void FilterButtonClicked()
        {
            Logger.log.Notice("filter button clicked");

            ShowScreen();
        }

        [UIAction("clear-filter-button-clicked")]
        private void ClearFilterButtonClicked()
        {
            Logger.log.Notice("clear filter button clicked");

            HideScreen();
        }

        public void ShowScreen(bool immediately = false)
        {
            if (immediately)
            {
                Vector3 localScale = this._container.transform.localScale;
                localScale.y = DefaultYScale;
                this._container.transform.localScale = localScale;

                _container.SetActive(true);
                return;
            }

            _container.SetActive(true);

            StopAllCoroutines();
            StartCoroutine(AnimationCoroutine(DefaultYScale));
        }

        public void HideScreen(bool immediately = false)
        {
            if (immediately)
            {
                Vector3 localScale = this._container.transform.localScale;
                localScale.y = HiddenYScale;
                this._container.transform.localScale = localScale;

                _container.SetActive(false);

                return;
            }

            StopAllCoroutines();
            StartCoroutine(AnimationCoroutine(HiddenYScale, true));
        }
        private IEnumerator AnimationCoroutine(float destAnimationValue, bool disableOnFinish = false)
        {
            yield return null;
            yield return null;

            Vector3 localScale = this._container.transform.localScale;
            while (Mathf.Abs(localScale.y - destAnimationValue) > 0.0001f)
            {
                float num = (localScale.y > destAnimationValue) ? 30f : 16f;
                localScale.y = Mathf.Lerp(localScale.y, destAnimationValue, Time.deltaTime * num);
                this._container.transform.localScale = localScale;

                yield return null;
            }

            localScale.y = destAnimationValue;
            this._container.transform.localScale = localScale;

            _container.SetActive(!disableOnFinish);
        }
    }
}
