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
                new PersonTrigger(AnimParams.carDefault, 2),
                new PersonTrigger(AnimParams.carIsGazed, 5, new string[] {AnimParams.carIsDriving}),
                new PersonTrigger(AnimParams.carIsDriving, 10, new string[] {AnimParams.carIsGazed}),
                new PersonTrigger(AnimParams.gunDefault, 1),
                new PersonTrigger(AnimParams.gunIsGazed, 90),
                new PersonTrigger(AnimParams.gunHasShotFront, 99),
                new PersonTrigger(AnimParams.globeDefault, 1),
                new PersonTrigger(AnimParams.globeIsSpinning, 20, new string[] {AnimParams.globeIsDancing}),
                new PersonTrigger(AnimParams.globeIsDancing, 50, new string[] {AnimParams.globeIsSpinning}),
                new PersonTrigger(AnimParams.houseDefault, 1),
                new PersonTrigger(AnimParams.houseIsOpen, 50)
        };

    } // end of class

} // end of namespace
