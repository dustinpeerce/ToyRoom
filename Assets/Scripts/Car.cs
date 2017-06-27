﻿using System.Collections;
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
        public Transform[] trackOnePoints;

        private LTSpline track;
        private float trackPosition = 0; // ratio 0,1 of the avatars position on the track
        private int tweenID;


        private void Awake()
        {
            canSeeToyKey = GameVals.AnimatorParameterKeys.canSeeCar;
            animatorParamDictionary = new Dictionary<string, bool>();
            animatorParamDictionary.Add(canSeeToyKey, false);
            animatorParamDictionary.Add(GameVals.AnimatorParameterKeys.carIsGazed, false);
            animatorParamDictionary.Add(GameVals.AnimatorParameterKeys.carIsDriving, false);
        }


        private void Start()
        {
            // Make the track from the provided transforms
            Vector3[] points = new Vector3[trackOnePoints.Length];
            for (int i = 0; i < points.Length; i++)
            {
                points[i] = trackOnePoints[i].position;
            }
            track = new LTSpline(points);

            tweenID = LeanTween.moveSpline(this.gameObject, track, 10.0f).setOrientToPath(true).setLoopClamp().id;

            LeanTween.pause(tweenID);
        }


        private void Update()
        {
            trackPosition += (Time.deltaTime * 0.03f) * speed;

            if (trackPosition < 0f) // We need to keep the ratio between 0-1 so after one we will loop back to the beginning of the track
                trackPosition = 1f;
            else if (trackPosition > 1f)
                trackPosition = 0f;
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


        private void StartDriving()
        {
            LeanTween.resume(tweenID);
            animatorParamDictionary[GameVals.AnimatorParameterKeys.carIsDriving] = true;
        }


        private void StopDriving()
        {
            LeanTween.pause(tweenID);
            animatorParamDictionary[GameVals.AnimatorParameterKeys.carIsDriving] = false;
        }


        // Use this for visualizing what the track looks like in the editor
        void OnDrawGizmos()
        {
            LTSpline.drawGizmo(trackOnePoints, Color.red);
        }

    }

}