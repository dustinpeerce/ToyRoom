using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ToyRoom
{

    public class TrackTracer : MonoBehaviour
    {
        public GameObject trackTrailOneObjects;
        public List<TrailRenderer> trackTrailOneRenderers;

        public GameObject trackTrailTwoObjects;
        public List<TrailRenderer> trackTrailTwoRenderers;

        public Transform[] trackPointsOne;
        public Transform[] trackPointsTwo;

        private LTSpline trackOne;
        private LTSpline trackTwo;

        public enum TrackIndex { TrackOne, TrackTwo };
        private TrackIndex currentTrackIndex;

        private void Start()
        {
            Vector3[] pointsOne = new Vector3[trackPointsOne.Length];
            for (int i = 0; i < pointsOne.Length; i++)
            {
                pointsOne[i] = trackPointsOne[i].position;
            }
            trackOne = new LTSpline(pointsOne);
            LeanTween.moveSpline(trackTrailOneObjects, trackOne, 0.75f).setOrientToPath(true).setLoopClamp();


            Vector3[] pointsTwo = new Vector3[trackPointsTwo.Length];
            for (int i = 0; i < pointsTwo.Length; i++)
            {
                pointsTwo[i] = trackPointsTwo[i].position;
            }
            trackTwo = new LTSpline(pointsTwo);
            LeanTween.moveSpline(trackTrailTwoObjects, trackTwo, 0.75f).setOrientToPath(true).setLoopClamp();

            LeanTween.pauseAll();

            currentTrackIndex = TrackIndex.TrackOne;
            UpdateTrack();
        }

        // Use this for visualizing what the track looks like in the editor
        void OnDrawGizmos()
        {
            LTSpline.drawGizmo(trackPointsOne, Color.red);
            LTSpline.drawGizmo(trackPointsTwo, Color.blue);
        }

        public void UpdateTrack()
        {
            if (currentTrackIndex == TrackIndex.TrackOne)
            {
                foreach (TrailRenderer rend in trackTrailOneRenderers)
                {
                    rend.startColor = new Color(rend.startColor.r, rend.startColor.g, rend.startColor.b, 1f);
                }
                foreach (TrailRenderer rend in trackTrailTwoRenderers)
                {
                    rend.startColor = new Color(rend.startColor.r, rend.startColor.g, rend.startColor.b, 0f);
                }
            }
            else
            {
                foreach (TrailRenderer rend in trackTrailOneRenderers)
                {
                    rend.startColor = new Color(rend.startColor.r, rend.startColor.g, rend.startColor.b, 0f);
                }
                foreach (TrailRenderer rend in trackTrailTwoRenderers)
                {
                    rend.startColor = new Color(rend.startColor.r, rend.startColor.g, rend.startColor.b, 1f);
                }
            }
        }

        public void ChangeCurrentTrackIndex()
        {
            if (currentTrackIndex == TrackIndex.TrackOne)
                currentTrackIndex = TrackIndex.TrackTwo;
            else
                currentTrackIndex = TrackIndex.TrackOne;

            UpdateTrack();
        }
    }
}
