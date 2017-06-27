using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace ToyRoom
{

    [RequireComponent(typeof(Collider))]
    public class Car : Toy
    {
        public GameObject car;
        public float speed = 1;
        public Material inactiveMaterial;
        public Material gazedAtMaterial;
        public Transform[] trackPoints;
        public Transform trackPivotPoint;

        private Vector3 carStartPos;
        private Vector3 trackPivotPos1;
        private Vector3 trackPivotPos2;

        private Vector3 currentTrackPivot;
        private Vector3 targetTrackPivot;

        private LTSpline track;
        private float trackPosition = 0; // ratio 0,1 of the avatars position on the track


        private void Awake()
        {
            canSeeToyKey = GameVals.AnimatorParameterKeys.canSeeCar;
            animatorParamDictionary = new Dictionary<string, bool>();
            animatorParamDictionary.Add(canSeeToyKey, false);
            animatorParamDictionary.Add(GameVals.AnimatorParameterKeys.carIsGazed, false);
            animatorParamDictionary.Add(GameVals.AnimatorParameterKeys.carIsDriving, false);

            carStartPos = transform.position;
            trackPivotPos1 = trackPivotPoint.localPosition;
            trackPivotPos2 = new Vector3(-1.695f, 0, 2.694f);
        }


        private void Start()
        {
            // Make the track from the provided transforms
            SetSpline();
            currentTrackPivot = trackPivotPos1;
            targetTrackPivot = trackPivotPos1;
            LeanTween.pauseAll();
        }

        private void SetSpline()
        {
            Vector3[] pointsOne = new Vector3[trackPoints.Length];
            for (int i = 0; i < pointsOne.Length; i++)
            {
                pointsOne[i] = trackPoints[i].position;
            }
            track = new LTSpline(pointsOne);
            LeanTween.moveSpline(this.gameObject, track, 10.0f).setOrientToPath(true).setLoopClamp();

            if (!animatorParamDictionary[GameVals.AnimatorParameterKeys.carIsDriving])
            {
                LeanTween.pauseAll();
            }
        }

        private void Update()
        {
            trackPosition += (Time.deltaTime * 0.03f) * speed;

            if (trackPosition < 0 || trackPosition > 1f)
            {
                if (trackPosition < 0f) // We need to keep the ratio between 0-1 so after one we will loop back to the beginning of the track\
                    trackPosition = 1f;
                else if (trackPosition > 1f)
                    trackPosition = 0f;
            }

            if ( (transform.position - carStartPos).magnitude <= 0.1f )
            {
                if (currentTrackPivot != targetTrackPivot)
                {
                    SetSpline();
                    currentTrackPivot = targetTrackPivot;
                }
            }
        }


        public void SetGazedAt(bool gazedAt)
        {
            animatorParamDictionary[GameVals.AnimatorParameterKeys.carIsGazed] = gazedAt;

            if (inactiveMaterial != null && gazedAtMaterial != null)
            {
                car.GetComponent<Renderer>().material = gazedAt ? gazedAtMaterial : inactiveMaterial;
                return;
            }
            car.GetComponent<Renderer>().material.color = gazedAt ? Color.green : Color.red;
        }


        public void ToggleDriving()
        {
            if (animatorParamDictionary[GameVals.AnimatorParameterKeys.carIsDriving])
            {
                StopDriving();
            }
            else
            {
                StartDriving();
            }
        }

        public void ToggleTrack()
        {
            if (trackPivotPoint.localPosition == trackPivotPos1)
            {
                trackPivotPoint.localPosition = trackPivotPos2;
                targetTrackPivot = trackPivotPos2;
            }
            else
            {
                trackPivotPoint.localPosition = trackPivotPos1;
                targetTrackPivot = trackPivotPos1;
            }
        }

        private void StartDriving()
        {
            LeanTween.resumeAll();
            animatorParamDictionary[GameVals.AnimatorParameterKeys.carIsDriving] = true;
        }


        private void StopDriving()
        {
            LeanTween.pauseAll();
            animatorParamDictionary[GameVals.AnimatorParameterKeys.carIsDriving] = false;
        }


        // Use this for visualizing what the track looks like in the editor
        void OnDrawGizmos()
        {
            LTSpline.drawGizmo(trackPoints, Color.red);
        }

    }

}
