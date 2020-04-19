using System;
using UnityEngine;
using KSP.UI.Screens.Flight;

// KSP 1.7.3 e Unity 2017.1.3p1

namespace Omicron
{
    public class Omicron : PartModule
    {
        private NavBall stockNavball;
        private Transform myNavballTrans;

        [KSPField]
        public string NavBallName = "navball_node";

        public void Start()
        {
            if (HighLogic.LoadedSceneIsEditor)
            {
                return;
            }
            stockNavball = FindObjectOfType<NavBall>();
            myNavballTrans = part.transform.FindRecursive(NavBallName);
        }

        public void Update()
        {
            if (HighLogic.LoadedSceneIsEditor)
            {
                return;
            }
            Quaternion attitudeGymbal = stockNavball.navBall.rotation;
            var delta = Quaternion.Inverse(attitudeGymbal);
            delta.z = -delta.z;
            myNavballTrans.localRotation = delta;

            print("Navball Rotation = " + myNavballTrans.eulerAngles + ", Local Rotation = " + myNavballTrans.localEulerAngles + "Delta = " + delta);
        }
    }

    public static class Utils
    {
        public static Transform FindRecursive(this Transform transform, String name)
        {
            if (transform.name == name) { return transform; }
            Transform tr = transform.Find(name);
            if (tr != null) { return tr; }
            foreach (Transform child in transform)
            {
                tr = child.FindRecursive(name);
                if (tr != null) { return tr; }
            }
            return null;
        }
    }
}
