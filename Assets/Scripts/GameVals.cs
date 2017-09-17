using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace ToyRoom
{

    public static class GameVals
    {

        public static class AnimParams
        {
            // Car related
            public static string canSeeCar = "CanSeeCar";
            public static string carIsGazed = "CarIsGazed";
            public static string carIsDriving = "CarIsDriving";

            // Gun related
            public static string canSeeGun = "CanSeeGun";
            public static string gunIsGazed = "GunIsGazed";
            public static string gunHasShotFront = "GunHasShotFront";

            // Globe releated
            public static string canSeeGlobe = "CanSeeGlobe";
            public static string globeIsSpinning = "GlobeIsSpinning";
            public static string globeIsDancing = "GlobeIsDancing";

            // House related
            public static string canSeeHouse = "CanSeeHouse";
            public static string houseIsOpen = "HouseIsOpen";
            public static string layDown = "LayDown";
        }

        public static PersonTrigger[] personTriggers = {
                new PersonTrigger(AnimParams.carIsGazed, 5),
                new PersonTrigger(AnimParams.carIsDriving, 10),
                new PersonTrigger(AnimParams.gunIsGazed, 90),
                new PersonTrigger(AnimParams.gunHasShotFront, 99),
                new PersonTrigger(AnimParams.globeIsSpinning, 20),
                new PersonTrigger(AnimParams.globeIsDancing, 50),
                new PersonTrigger(AnimParams.houseIsOpen, 50)
        };

    } // end of class

} // end of namespace
