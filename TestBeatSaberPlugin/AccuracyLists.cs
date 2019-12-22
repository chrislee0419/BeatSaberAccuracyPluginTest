using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace TestBeatSaberPlugin
{
    internal struct CutScoreTuple
    {
        public NoteType noteType;
        public NoteCutInfo noteCutInfo;
        public SaberSwingRatingCounter saberSwingRatingCounter;

        public CutScoreTuple(NoteType noteType, NoteCutInfo noteCutInfo, SaberSwingRatingCounter saberSwingRatingCounter)
        {
            this.noteType = noteType;
            this.noteCutInfo = noteCutInfo;
            this.saberSwingRatingCounter = saberSwingRatingCounter;
        }
    }

    class AccuracyLists : MonoBehaviour
    {
        private ScoreController sc;
        private List<CutScoreTuple> cstList = new List<CutScoreTuple>();

        void Awake()
        {
            Plugin.lSaberCut.Clear();
            Plugin.rSaberCut.Clear();

            StartCoroutine(GrabRequired());
        }

        IEnumerator GrabRequired()
        {
            yield return new WaitUntil(() => Resources.FindObjectsOfTypeAll<ScoreController>().Any());
            sc = Resources.FindObjectsOfTypeAll<ScoreController>().FirstOrDefault();
            Init();
        }

        void Init()
        {
            sc.noteWasCutEvent += NoteCut;
        }

        private void OnDestroy()
        {
            sc.noteWasCutEvent -= NoteCut;
        }

        void NoteCut(NoteData data, NoteCutInfo cutInfo, int n)
        {
            if (data.noteType == NoteType.Bomb || !cutInfo.allIsOK) return;

            cutInfo.swingRatingCounter.didFinishEvent += OnCutFinished;
            cstList.Add(new CutScoreTuple(data.noteType, cutInfo, cutInfo.swingRatingCounter));

            //int beforeCut, afterCut, cutDistance;
            //ScoreController.ScoreWithoutMultiplier(cutInfo, null, out beforeCut, out afterCut, out cutDistance);

            //if (data.noteType == NoteType.NoteA)
            //    Plugin.lSaberCut.Add(new HitScore(beforeCut, afterCut, cutDistance));
            //else if (data.noteType == NoteType.NoteB)
            //    Plugin.rSaberCut.Add(new HitScore(beforeCut, afterCut, cutDistance));
        }

        private void OnCutFinished(SaberSwingRatingCounter saberSwingRatingCounter)
        {
            CutScoreTuple cst = cstList.Find((elem) => elem.saberSwingRatingCounter == saberSwingRatingCounter);

            int beforeCut, afterCut, cutDistance;
            ScoreController.RawScoreWithoutMultiplier(cst.noteCutInfo, out beforeCut, out afterCut, out cutDistance);

            if (cst.noteType == NoteType.NoteA)
                Plugin.lSaberCut.Add(new HitScore(beforeCut - cutDistance, afterCut, cutDistance));
            else if (cst.noteType == NoteType.NoteB)
                Plugin.rSaberCut.Add(new HitScore(beforeCut - cutDistance, afterCut, cutDistance));

            cstList.Remove(cst);
        }
    }
}
