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
        public Transform[] trackPoints1;
        public Transform[] trackPoints2;

        private Vector3 carStartPos;
        private Vector3 trackPivotPos1;
        private Vector3 trackPivotPos2;

        private Transform[] currentTrackPoints;
        private Transform[] targetTrackPoints;

        private LTSpline track;
        private float trackPosition = 0; // ratio 0,1 of the avatars position on the track

        private bool trackOneIsActive;

        private void Awake()
        {
            canSeeToyKey = GameVals.AnimatorParameterKeys.canSeeCar;
            animatorParamDictionary = new Dictionary<string, bool>();
            animatorParamDictionary.Add(canSeeToyKey, false);
            animatorParamDictionary.Add(GameVals.AnimatorParameterKeys.carIsGazed, false);
            animatorParamDictionary.Add(GameVals.AnimatorParameterKeys.carIsDriving, false);

            carStartPos = transform.position;
        }


        private void Start()
        {
            // Make the track from the provided transforms
            currentTrackPoints = trackPoints1;
            targetTrackPoints = trackPoints1;
            trackOneIsActive = true;
            SetSpline();
            LeanTween.pauseAll();
        }

        private void SetSpline()
        {
            Vector3[] points = new Vector3[targetTrackPoints.Length];
            for (int i = 0; i < points.Length; i++)
            {
                points[i] = targetTrackPoints[i].position;
            }
            track = new LTSpline(points);
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
                if (currentTrackPoints != targetTrackPoints)
                {
                    SetSpline();
                    currentTrackPoints = targetTrackPoints;
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
            if (trackOneIsActive)
            {
                targetTrackPoints = trackPoints2;
            }
            else
            {
                targetTrackPoints = trackPoints1;
            }

            trackOneIsActive = !trackOneIsActive;
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
            LTSpline.drawGizmo(trackPoints1, Color.red);
            LTSpline.drawGizmo(trackPoints2, Color.blue);
        }

    }

}
