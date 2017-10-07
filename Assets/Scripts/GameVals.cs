using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace ToyRoom
{

    public static class GameVals
    {

        public static class AnimParams
        {
            // Person
            public static string personDefault = "Default";

            // Car related
            public static string carDefault = "CarDefault";
            public static string carIsGazed = "CarIsGazed";
            public static string carIsDriving = "CarIsDriving";

            // Gun related
            public static string gunDefault = "GunDefault";
            public static string gunIsGazed = "GunIsGazed";
            public static string gunHasShotFront = "GunHasShotFront";

            // Globe releated
            public static string globeDefault = "GlobeDefault";
            public static string globeIsSpinning = "GlobeIsSpinning";
            public static string globeIsDancing = "GlobeIsDancing";

            // House related
            public static string houseDefault = "HouseDefault";
            public static string houseIsOpen = "HouseIsOpen";
            public static string layDown = "LayDown";
        }

        public static PersonTrigger[] personTriggers = {
                new PersonTrigger(AnimParams.carDefault, 10, 0),
                new PersonTrigger(AnimParams.carIsGazed, 15, 0, new string[] {AnimParams.carIsDriving}),
                new PersonTrigger(AnimParams.carIsDriving, 20, 2, new string[] {AnimParams.carIsGazed}),
                new PersonTrigger(AnimParams.gunDefault, 1, 0),
                new PersonTrigger(AnimParams.gunIsGazed, 100, 0.5f),
                new PersonTrigger(AnimParams.gunHasShotFront, 100, 2),
                new PersonTrigger(AnimParams.globeDefault, 1, 0),
                new PersonTrigger(AnimParams.globeIsSpinning, 25, 1, new string[] {AnimParams.globeIsDancing}),
                new PersonTrigger(AnimParams.globeIsDancing, 50, 2, new string[] {AnimParams.globeIsSpinning}),
                new PersonTrigger(AnimParams.houseDefault, 1, 0),
                new PersonTrigger(AnimParams.houseIsOpen, 50, 2f)
        };

    } // end of class

} // end of namespace
