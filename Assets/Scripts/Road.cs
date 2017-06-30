using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ToyRoom
{

    public class Road : MonoBehaviour
    {
        public enum TrackIndex { TrackOne, TrackTwo };

        public GameObject trackOne;
        public GameObject trackTwo;

        public Material darkRoadMat;
        public Material lightRoadMat;

        private MeshRenderer trackOneRenderer;
        private MeshRenderer trackTwoRenderer;

        private TrackIndex currentTrackIndex;

        private void Awake()
        {
            trackOneRenderer = trackOne.GetComponent<MeshRenderer>();
            trackTwoRenderer = trackTwo.GetComponent<MeshRenderer>();
        }

        private void Start()
        {
            currentTrackIndex = TrackIndex.TrackOne;
            UpdateTrack();
        }

        public void UpdateTrack()
        {
            if (currentTrackIndex == TrackIndex.TrackOne)
            {
                trackOneRenderer.material = lightRoadMat;
                trackTwoRenderer.material = darkRoadMat;
            }
            else
            {
                trackOneRenderer.material = darkRoadMat;
                trackTwoRenderer.material = lightRoadMat;
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
