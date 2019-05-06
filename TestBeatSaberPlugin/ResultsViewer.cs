using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using UnityEngine;
using TMPro;
using CustomUI.BeatSaber;

namespace TestBeatSaberPlugin
{
    class ResultsViewer : MonoBehaviour
    {
        private List<GameObject> results = new List<GameObject>();
        private ResultsViewController rvc;

        void Awake()
        {
            StartCoroutine(GrabResultsScreen());
        }

        IEnumerator GrabResultsScreen()
        {
            int tries = 0;
            while (true)
            {
                rvc = Resources.FindObjectsOfTypeAll<ResultsViewController>().FirstOrDefault();

                if ((rvc != null && rvc.isActivated) || tries >= 20)
                    break;

                tries++;
                yield return new WaitForSeconds(0.1f);
            }

            if (tries < 20)
            {
                Init();
                Logger.log.Debug("ResultsViewController successfully acquired for the test plugin");
            }
        }

        void Init()
        {
            if (rvc.isActivated)
            {
                /*
                 * - canvas is:
                 * 160f wide
                 * 80f tall
                 * 
                 * - origin is located in the center of the canvas
                 * - origin of each TextMeshPro object is in the center
                 */
                float yPos = 36f;
                const float yDelta = -4f;
                const float xPos = -58f;
                const float xDelta = 12f;
                bool isTopTextCreated = false;

                if (PluginConfig.prehitAngleDisplayBoth || PluginConfig.prehitAngleDisplaySeparate)
                {
                    List<int> left = Plugin.lSaberCut.ConvertAll((hit) => hit.beforeCut);
                    List<int> right = Plugin.rSaberCut.ConvertAll((hit) => hit.beforeCut);

                    if (!isTopTextCreated)
                    {
                        CreateTitleText(xPos, yPos);
                        yPos += yDelta;
                        isTopTextCreated = true;
                    }

                    TextMeshProUGUI label = BeatSaberUI.CreateText(rvc.rectTransform, "Pre-hit Angle", new Vector2(xPos, yPos));
                    label.alignment = TextAlignmentOptions.Center;
                    label.fontSize = 3.5f;
                    results.Add(label.gameObject);
                    yPos += 1.5f * yDelta;

                    if (PluginConfig.prehitAngleDisplaySeparate)
                    {
                        CreateViewer(new Vector2(xPos - xDelta, yPos), left, "Left");
                        CreateViewer(new Vector2(xPos + xDelta, yPos), right, "Right");
                    }
                    if (PluginConfig.prehitAngleDisplayBoth)
                    {
                        List<int> combined = left;
                        combined.AddRange(right);
                        CreateViewer(new Vector2(xPos, yPos), combined, "Both");
                    }
                    yPos += yDelta;
                }

                if (PluginConfig.posthitAngleDisplayBoth || PluginConfig.posthitAngleDisplaySeparate)
                {
                    List<int> left = Plugin.lSaberCut.ConvertAll((hit) => hit.afterCut);
                    List<int> right = Plugin.rSaberCut.ConvertAll((hit) => hit.afterCut);

                    if (!isTopTextCreated)
                    {
                        CreateTitleText(xPos, yPos);
                        yPos += yDelta;
                        isTopTextCreated = true;
                    }

                    TextMeshProUGUI label = BeatSaberUI.CreateText(rvc.rectTransform, "Followthrough", new Vector2(xPos, yPos));
                    label.alignment = TextAlignmentOptions.Center;
                    label.fontSize = 3.5f;
                    results.Add(label.gameObject);
                    yPos += 1.5f * yDelta;

                    if (PluginConfig.posthitAngleDisplaySeparate)
                    {
                        CreateViewer(new Vector2(xPos - xDelta, yPos), left, "Left");
                        CreateViewer(new Vector2(xPos + xDelta, yPos), right, "Right");
                    }
                    if (PluginConfig.posthitAngleDisplayBoth)
                    {
                        List<int> combined = left;
                        combined.AddRange(right);
                        CreateViewer(new Vector2(xPos, yPos), combined, "Both");
                    }
                    yPos += yDelta;
                }

                if (PluginConfig.accDisplayBoth || PluginConfig.accDisplaySeparate)
                {
                    List<int> left = Plugin.lSaberCut.ConvertAll((hit) => hit.cutDistance);
                    List<int> right = Plugin.rSaberCut.ConvertAll((hit) => hit.cutDistance);

                    if (!isTopTextCreated)
                    {
                        CreateTitleText(xPos, yPos);
                        yPos += yDelta;
                        isTopTextCreated = true;
                    }

                    TextMeshProUGUI label = BeatSaberUI.CreateText(rvc.rectTransform, "Accuracy", new Vector2(xPos, yPos));
                    label.alignment = TextAlignmentOptions.Center;
                    label.fontSize = 3.5f;
                    results.Add(label.gameObject);
                    yPos += 1.5f * yDelta;
                    if (PluginConfig.accDisplaySeparate)
                    {
                        CreateViewer(new Vector2(xPos - xDelta, yPos), left, "Left");
                        CreateViewer(new Vector2(xPos + xDelta, yPos), right, "Right");
                    }
                    if (PluginConfig.accDisplayBoth)
                    {
                        List<int> combined = left;
                        combined.AddRange(right);
                        CreateViewer(new Vector2(xPos, yPos), combined, "Both");
                    }
                    yPos += yDelta;
                }

                rvc.continueButtonPressedEvent += Continue;
                rvc.restartButtonPressedEvent += Continue;
            }
        }

        private void CreateTitleText(float x, float y)
        {
            int notesCut = Plugin.lSaberCut.Count + Plugin.rSaberCut.Count;
            TextMeshProUGUI title = BeatSaberUI.CreateText(rvc.rectTransform, $"Average Scores ({notesCut} note{(notesCut == 1 ? "" : "s")} hit)", new Vector2(x, y));
            title.alignment = TextAlignmentOptions.Center;
            results.Add(title.gameObject);
        }

        private void OnDestroy()
        {
            // remove event handler, otherwise this deletion will not happen for new ResultsViewer objects
            rvc.continueButtonPressedEvent -= Continue;
            rvc.restartButtonPressedEvent -= Continue;
        }

        void Continue(ResultsViewController arg)
        {
            foreach (GameObject go in results)
                Destroy(go);

            // unlike AccuracyLists, ResultsViewer does not get destroyed before starting a new song for some reason, so we have to do this manually
            Destroy(this.gameObject);
        }

        void CreateViewer(Vector2 position, List<int> score, string label)
        {
            RectTransform transform = rvc.rectTransform;
            string text = score.Count == 0 ? "0" : score.Average().ToString($"0.00");

            TextMeshProUGUI viewerTMP = BeatSaberUI.CreateText(transform, text, position, new Vector2(12f, 4f));
            TextMeshProUGUI labelTMP = BeatSaberUI.CreateText(viewerTMP.rectTransform, label, new Vector2(0f, 3f), new Vector2(12f, 4f));

            viewerTMP.alignment = TextAlignmentOptions.Center;
            labelTMP.alignment = TextAlignmentOptions.Center;
            viewerTMP.fontSize = 3f;
            labelTMP.fontSize = 3f;

            results.Add(viewerTMP.gameObject);
            results.Add(labelTMP.gameObject);

            /*
            GameObject viewerGO = new GameObject("Results Viewer | " + label);
            viewerGO.SetActive(false);

            TextMeshPro viewer = viewerGO.AddComponent<TextMeshPro>();
            viewer.text = accuracyPoints.Average().ToString($"0.00");
            viewer.fontSize = 3;
            viewer.alignment = TextAlignmentOptions.Center;
            viewer.rectTransform.position = position;

            viewerGO.SetActive(true);

            GameObject labelGO = new GameObject("Viewer Label | " + label);
            labelGO.SetActive(false);

            TextMeshPro labelTMPro = labelGO.AddComponent<TextMeshPro>();
            labelTMPro.text = label;
            labelTMPro.fontSize = 2;
            labelTMPro.alignment = TextAlignmentOptions.Center;
            labelTMPro.rectTransform.parent = viewer.rectTransform;
            labelTMPro.rectTransform.localPosition = new Vector3(0, 0.3f, 0);

            labelGO.SetActive(true);

            results.Add(viewerGO);
            results.Add(labelGO);
            */
        }
    }
}
