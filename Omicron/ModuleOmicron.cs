using System;
using UnityEngine;
using KSP.UI.Screens.Flight;
using TMPro;

namespace Omicron
{
    public class ModuleOmicron : PartModule
    {
        #region Var
        //NavBall
        private NavBall stockNavball;
        private Transform myNavballTrans;
        private Transform myProgradeTrans;

        //SAS
        private bool SASstat;
        private Transform SASglass;

        //RCS
        private bool RCSstat;
        private Transform RCSglass;

        //Throttle
        private Transform Throttle_Dial_L;
        private Transform Throttle_Dial_R;
        private Transform Throttle_L;
        private Transform Throttle_R;
        private float Throttle;

        //Speed Dials
        private Transform Speed_Dial_10_L;
        private Transform Speed_Dial_100_L;
        private Transform Speed_Dial_1000_L;
        private Transform Speed_Dial_10_R;
        private Transform Speed_Dial_100_R;
        private Transform Speed_Dial_1000_R;
        private float Speed;
        private Transform SpeedUnits;

        //pointer for speed
        private int speedpointer;
        private int speedpointerold;

        //Altitude Dials;
        private Transform Alt_Dial_10_L;
        private Transform Alt_Dial_100_L;
        private Transform Alt_Dial_1000_L;
        private Transform Alt_Dial_10_R;
        private Transform Alt_Dial_100_R;
        private Transform Alt_Dial_1000_R;
        private double Altitude;
        private Transform AltUnits;

        //pointer for altitude
        private int altpointer;
        private int altpointerold;

        //Signal Strength
        private double commStatus;
        private Transform commOnOff;

        //pointer for comm
        private int commpointer;
        private int commpointerold;

        //NavBall
        private string NavBallName = "navball_node";
        //private string ProgradeName = "prograde_node";

        //SAS
        private string SAS_Name = "SAS_glass";

        //RCS
        private string RCS_Name = "RCS_glass";

        //Throttle
        private string Throttle_Dial_Left_Name = "dial_throttle_left";
        private string Throttle_Left_Name = "throttle_left";
        private string Throttle_Dial_Right_Name = "dial_throttle_right";
        private string Throttle_Right_Name = "throttle_right";

        //Speed Dials
        private string Speed_Dial_10_Left_Name = "Dial_10_speed_left";
        private string Speed_Dial_100_Left_Name = "Dial_100_speed_left";
        private string Speed_Dial_1000_Left_Name = "Dial_1000_speed_left";
        private string Speed_Dial_10_Right_Name = "Dial_10_speed_right";
        private string Speed_Dial_100_Right_Name = "Dial_100_speed_right";
        private string Speed_Dial_1000_Right_Name = "Dial_1000_speed_right";

        //Altitude Dials
        private string Alt_Dial_10_Left_Name = "Dial_10_altitude_left";
        private string Alt_Dial_100_Left_Name = "Dial_100_altitude_left";
        private string Alt_Dial_1000_Left_Name = "Dial_1000_altitude_left";
        private string Alt_Dial_10_Right_Name = "Dial_10_altitude_right";
        private string Alt_Dial_100_Right_Name = "Dial_100_altitude_right";
        private string Alt_Dial_1000_Right_Name = "Dial_1000_altitude_right";

        #endregion

        #region OnStart
        //Hide Altimeter and Speed  and Comm transformers on Start
        public override void OnStart(PartModule.StartState state)
        {
            foreach (Transform Obj in part.GetComponentsInChildren<Transform>())
            {
                if 
                    (
                    Obj.name.Equals("altitude_Km_L", StringComparison.Ordinal) ||
                    Obj.name.Equals("altitude_Mm_L", StringComparison.Ordinal) ||
                    Obj.name.Equals("altitude_Bm_L", StringComparison.Ordinal) ||
                    Obj.name.Equals("altitude_Tm_L", StringComparison.Ordinal) ||
                    Obj.name.Equals("altitude_Km_R", StringComparison.Ordinal) ||
                    Obj.name.Equals("altitude_Mm_R", StringComparison.Ordinal) ||
                    Obj.name.Equals("altitude_Bm_R", StringComparison.Ordinal) ||
                    Obj.name.Equals("altitude_Tm_R", StringComparison.Ordinal) ||

                    Obj.name.Equals("speed_Kms_L", StringComparison.Ordinal) ||
                    Obj.name.Equals("speed_Mms_L", StringComparison.Ordinal) ||
                    Obj.name.Equals("speed_Bms_L", StringComparison.Ordinal) ||
                    Obj.name.Equals("speed_Kms_R", StringComparison.Ordinal) ||
                    Obj.name.Equals("speed_Mms_R", StringComparison.Ordinal) ||
                    Obj.name.Equals("speed_Bms_R", StringComparison.Ordinal) ||

                    Obj.name.Equals(SAS_Name, StringComparison.Ordinal) ||
                    Obj.name.Equals(RCS_Name, StringComparison.Ordinal) ||
                    Obj.name.Equals("comm_on_1", StringComparison.Ordinal) ||
                    Obj.name.Equals("comm_on_2", StringComparison.Ordinal) ||
                    Obj.name.Equals("comm_on_3", StringComparison.Ordinal)
                    )
                {
                    Obj.gameObject.SetActive(false);
                }
            }
        }
        #endregion

        #region Start
        public void Start()
        {
            if (HighLogic.LoadedSceneIsEditor)
            {
                return;
            }
            //NavBall
            stockNavball = FindObjectOfType<NavBall>();
            myNavballTrans = part.transform.FindRecursive(NavBallName);
            //myProgradeTrans = part.transform.FindRecursive(ProgradeName);

            //RCS
            SASstat = vessel.ActionGroups[KSPActionGroup.SAS];
            SASglass = part.transform.FindRecursive(SAS_Name);

            //SAS
            RCSstat = vessel.ActionGroups[KSPActionGroup.RCS];
            RCSglass = part.transform.FindRecursive(RCS_Name);

            //Set initial state for altitude pointers
            altpointer = 1;
            altpointerold = altpointer;

            //Set initial state for speed pointers
            speedpointer = 1;
            speedpointerold = speedpointer;

            //Set initial state for comm pointers
            commpointer = 0;
            commpointerold = commpointer;

            //Throttle
            Throttle_Dial_L = part.transform.FindRecursive(Throttle_Dial_Left_Name);
            Throttle_Dial_R = part.transform.FindRecursive(Throttle_Dial_Right_Name);
            Throttle_L = part.transform.FindRecursive(Throttle_Left_Name);
            Throttle_R = part.transform.FindRecursive(Throttle_Right_Name);

            //Speed Dials
            Speed_Dial_10_L = part.transform.FindRecursive(Speed_Dial_10_Left_Name);
            Speed_Dial_100_L = part.transform.FindRecursive(Speed_Dial_100_Left_Name);
            Speed_Dial_1000_L = part.transform.FindRecursive(Speed_Dial_1000_Left_Name);
            Speed_Dial_10_R = part.transform.FindRecursive(Speed_Dial_10_Right_Name);
            Speed_Dial_100_R = part.transform.FindRecursive(Speed_Dial_100_Right_Name);
            Speed_Dial_1000_R = part.transform.FindRecursive(Speed_Dial_1000_Right_Name);

            //Altitude Dials
            Alt_Dial_10_L = part.transform.FindRecursive(Alt_Dial_10_Left_Name);
            Alt_Dial_100_L = part.transform.FindRecursive(Alt_Dial_100_Left_Name);
            Alt_Dial_1000_L = part.transform.FindRecursive(Alt_Dial_1000_Left_Name);
            Alt_Dial_10_R = part.transform.FindRecursive(Alt_Dial_10_Right_Name);
            Alt_Dial_100_R = part.transform.FindRecursive(Alt_Dial_100_Right_Name);
            Alt_Dial_1000_R = part.transform.FindRecursive(Alt_Dial_1000_Right_Name);
        }
        #endregion

        #region Update
        public void Update()
        {
            if (HighLogic.LoadedSceneIsEditor)
            {
                return;
            };

            #region NavBall
            //NavBall
            Quaternion attitudeGymbal = stockNavball.navBall.rotation;
            var delta = Quaternion.Inverse(attitudeGymbal);
            delta.y = -delta.y;
            myNavballTrans.localRotation = delta;

            //Prograde
            //if (vessel.srfSpeed > 1)
            //{
            //myProgradeTrans.gameObject.SetActive(true);

            //Quaternion progradeGymbal = Quaternion.LookRotation(vessel.srf_vel_direction,vessel.up);
            //var gama = Quaternion.Inverse(progradeGymbal);
            //gama.y = -gama.y;
            //myProgradeTrans.localRotation = gama;

            //myProgradeTrans.forward = vessel.srf_vel_direction;
            //Vector3 progrademultiply = new Vector3(0, -1, 0);
            //myProgradeTrans.forward = Vector3.Project(vessel.srf_vel_direction, vessel.transform.forward);
            //}
            //else
            //{
            // myProgradeTrans.gameObject.SetActive(false);
            //}
            #endregion

            #region SAS
            //SAS state LED
            SASstat = vessel.ActionGroups[KSPActionGroup.SAS];
            RCSstat = vessel.ActionGroups[KSPActionGroup.RCS];
            if (SASstat)
            {
                SASglass.gameObject.SetActive(true);
            }
            else
            {
                SASglass.gameObject.SetActive(false);
            }
            #endregion

            #region RCS
            //RCS state LED
            if (RCSstat)
            {
                RCSglass.gameObject.SetActive(true);
            }
            else
            {
                RCSglass.gameObject.SetActive(false);
            }
            #endregion

            #region Throttle
            //Throttle
            Throttle = vessel.ctrlState.mainThrottle;
            Quaternion jogThrottle = Quaternion.Euler(0, 0, (Throttle * 270));
            Throttle_Dial_L.localRotation = jogThrottle;
            Throttle_Dial_R.localRotation = jogThrottle;
            Throttle_L.localRotation = jogThrottle;
            Throttle_R.localRotation = jogThrottle;
            Throttle_L.localPosition = new Vector3(0, 0, (float) ((Throttle * 0.008) + 0.029));
            Throttle_R.localPosition = new Vector3(0, 0, (float) ((Throttle * 0.008) + 0.029));
            #endregion

            #region Speed
            //Speed Orbit or Surface
            if (vessel.atmDensity < 0.2 & vessel.altitude > 25000)
            {
                Speed = (float)vessel.obt_speed;
            }
            else
            {
                Speed = (float)vessel.srfSpeed;
            };

            //Check speed unity;
            speedpointer = 1;
            while (Speed < 100 & altpointer > 1)
            {
                speedpointer += 1;
                Speed *= 10;
            }

            while (Speed > 10000 & speedpointer < 8)
            {
                speedpointer -= 1;
                Speed /= 10;
            }

            //Change Unit labels if needed
            if (speedpointer != speedpointerold)
            {
                SpeedUnits = part.transform.FindRecursive("speed_ms_L");
                SpeedUnits.gameObject.SetActive(false);
                SpeedUnits = part.transform.FindRecursive("speed_Kms_L");
                SpeedUnits.gameObject.SetActive(false);
                SpeedUnits = part.transform.FindRecursive("speed_Mms_L");
                SpeedUnits.gameObject.SetActive(false);
                SpeedUnits = part.transform.FindRecursive("speed_Bms_L");
                SpeedUnits.gameObject.SetActive(false);

                SpeedUnits = part.transform.FindRecursive("speed_ms_R");
                SpeedUnits.gameObject.SetActive(false);
                SpeedUnits = part.transform.FindRecursive("speed_Kms_R");
                SpeedUnits.gameObject.SetActive(false);
                SpeedUnits = part.transform.FindRecursive("speed_Mms_R");
                SpeedUnits.gameObject.SetActive(false);
                SpeedUnits = part.transform.FindRecursive("speed_Bms_R");
                SpeedUnits.gameObject.SetActive(false);

                switch (altpointer)
                {
                    case int n when (n >= 1 && n <= 2):
                        SpeedUnits = part.transform.FindRecursive("speed_ms_L");
                        SpeedUnits.gameObject.SetActive(true);
                        SpeedUnits = part.transform.FindRecursive("speed_ms_R");
                        SpeedUnits.gameObject.SetActive(true);
                        break;
                    case int n when (n >= 3 && n <= 4):
                        SpeedUnits = part.transform.FindRecursive("speed_Kms_L");
                        SpeedUnits.gameObject.SetActive(true);
                        SpeedUnits = part.transform.FindRecursive("speed_Kms_R");
                        SpeedUnits.gameObject.SetActive(true);
                        break;
                    case int n when (n >= 5 && n <= 6):
                        SpeedUnits = part.transform.FindRecursive("speed_Mms_L");
                        SpeedUnits.gameObject.SetActive(true);
                        SpeedUnits = part.transform.FindRecursive("speed_Mms_R");
                        SpeedUnits.gameObject.SetActive(true);
                        break;
                    case int n when (n >= 7 && n <= 8):
                        SpeedUnits = part.transform.FindRecursive("speed_Bms_L");
                        SpeedUnits.gameObject.SetActive(true);
                        SpeedUnits = part.transform.FindRecursive("speed_Bms_R");
                        SpeedUnits.gameObject.SetActive(true);
                        break;
                    default:
                        SpeedUnits = part.transform.FindRecursive("speed_ms_L");
                        SpeedUnits.gameObject.SetActive(true);
                        SpeedUnits = part.transform.FindRecursive("speed_ms_R");
                        SpeedUnits.gameObject.SetActive(true);
                        break;
                }
            }

            //Speed Dials
            Quaternion DialSpeed10 = Quaternion.Euler(0, (float) ((Speed * 3.6) % 360), 0);
            Quaternion DialSpeed100 = Quaternion.Euler(0, (float)((Speed * 0.36) % 360), 0);
            Quaternion DialSpeed1000 = Quaternion.Euler(0, (float)((Speed * 0.036) % 360), 0);
            Speed_Dial_10_L.localRotation = DialSpeed10;
            Speed_Dial_100_L.localRotation = DialSpeed100;
            Speed_Dial_1000_L.localRotation = DialSpeed1000;
            Speed_Dial_10_R.localRotation = DialSpeed10;
            Speed_Dial_100_R.localRotation = DialSpeed100;
            Speed_Dial_1000_R.localRotation = DialSpeed1000;
            Speed_Dial_10_L.localPosition = new Vector3(0, (float) ((0.02 * (Speed % 100)/100) + 0.17), 0);
            Speed_Dial_100_L.localPosition = new Vector3(0, (float)((0.02 * (Speed % 1000) / 1000) + 0.165), 0);
            Speed_Dial_1000_L.localPosition = new Vector3(0, (float)((0.02 * (Speed % 10000) / 10000) + 0.16), 0);
            Speed_Dial_10_R.localPosition = new Vector3(0, (float)((0.02 * (Speed % 100) / 100) + 0.17), 0);
            Speed_Dial_100_R.localPosition = new Vector3(0, (float)((0.02 * (Speed % 1000) / 1000) + 0.165), 0);
            Speed_Dial_1000_R.localPosition = new Vector3(0, (float)((0.02 * (Speed % 10000) / 10000) + 0.16), 0);

            //Leverage pointers
            speedpointerold = speedpointer;
            #endregion

            #region Altitude
            //Altitude AGL or MSL
            if (vessel.altimeterDisplayState == AltimeterDisplayState.AGL)
            {
                Altitude = vessel.radarAltitude;
            }
            else
            {
                Altitude = vessel.altitude;
            };

            //Check altitude unity;
            altpointer = 1;
            while (Altitude < 100 & altpointer > 1)
            {
                altpointer -= 1;
                Altitude *= 10;
            }

            while (Altitude > 10000 & altpointer < 15)
            {
                altpointer += 1;
                Altitude /= 10;
            }

            //Change Unit labels if needed
            if (altpointer != altpointerold)
            {
                AltUnits = part.transform.FindRecursive("altitude_m_L");
                AltUnits.gameObject.SetActive(false);
                AltUnits = part.transform.FindRecursive("altitude_Km_L");
                AltUnits.gameObject.SetActive(false);
                AltUnits = part.transform.FindRecursive("altitude_Mm_L");
                AltUnits.gameObject.SetActive(false);
                AltUnits = part.transform.FindRecursive("altitude_Bm_L");
                AltUnits.gameObject.SetActive(false);
                AltUnits = part.transform.FindRecursive("altitude_Tm_L");
                AltUnits.gameObject.SetActive(false);

                AltUnits = part.transform.FindRecursive("altitude_m_R");
                AltUnits.gameObject.SetActive(false);
                AltUnits = part.transform.FindRecursive("altitude_Km_R");
                AltUnits.gameObject.SetActive(false);
                AltUnits = part.transform.FindRecursive("altitude_Mm_R");
                AltUnits.gameObject.SetActive(false);
                AltUnits = part.transform.FindRecursive("altitude_Bm_R");
                AltUnits.gameObject.SetActive(false);
                AltUnits = part.transform.FindRecursive("altitude_Tm_R");
                AltUnits.gameObject.SetActive(false);

                switch (altpointer)
                {
                    case int n when (n >= 1 && n <= 3):
                        AltUnits = part.transform.FindRecursive("altitude_m_L");
                        AltUnits.gameObject.SetActive(true);
                        AltUnits = part.transform.FindRecursive("altitude_m_R");
                        AltUnits.gameObject.SetActive(true);
                        break;
                    case int n when (n >= 4 && n <= 6):
                        AltUnits = part.transform.FindRecursive("altitude_Km_L");
                        AltUnits.gameObject.SetActive(true);
                        AltUnits = part.transform.FindRecursive("altitude_Km_R");
                        AltUnits.gameObject.SetActive(true);
                        break;
                    case int n when (n >= 7 && n <= 9):
                        AltUnits = part.transform.FindRecursive("altitude_Mm_L");
                        AltUnits.gameObject.SetActive(true);
                        AltUnits = part.transform.FindRecursive("altitude_Mm_R");
                        AltUnits.gameObject.SetActive(true);
                        break;
                    case int n when (n >= 10 && n <= 12):
                        AltUnits = part.transform.FindRecursive("altitude_Bm_L");
                        AltUnits.gameObject.SetActive(true);
                        AltUnits = part.transform.FindRecursive("altitude_Bm_R");
                        AltUnits.gameObject.SetActive(true);
                        break;
                    case int n when (n >= 13 && n <= 15):
                        AltUnits = part.transform.FindRecursive("altitude_Tm_L");
                        AltUnits.gameObject.SetActive(true);
                        AltUnits = part.transform.FindRecursive("altitude_Tm_R");
                        AltUnits.gameObject.SetActive(true);
                        break;
                    default:
                        AltUnits = part.transform.FindRecursive("altitude_m_L");
                        AltUnits.gameObject.SetActive(true);
                        AltUnits = part.transform.FindRecursive("altitude_m_R");
                        AltUnits.gameObject.SetActive(true);
                        break;
                }
            }

            //Altitude Dials
            Quaternion DialAlt10 = Quaternion.Euler(0, (float)((Altitude * 3.6) % 360), 0);
            Quaternion DialAlt100 = Quaternion.Euler(0, (float)((Altitude * 0.36) % 360), 0);
            Quaternion DialAlt1000 = Quaternion.Euler(0, (float)((Altitude * 0.036) % 360), 0);
            Alt_Dial_10_L.localRotation = DialAlt10;
            Alt_Dial_100_L.localRotation = DialAlt100;
            Alt_Dial_1000_L.localRotation = DialAlt1000;
            Alt_Dial_10_R.localRotation = DialAlt10;
            Alt_Dial_100_R.localRotation = DialAlt100;
            Alt_Dial_1000_R.localRotation = DialAlt1000;
            Alt_Dial_10_L.localPosition = new Vector3(0, (float)((0.02 * (Altitude % 100) / 100) + 0.17), 0);
            Alt_Dial_100_L.localPosition = new Vector3(0, (float)((0.02 * (Altitude % 1000) / 1000) + 0.165), 0);
            Alt_Dial_1000_L.localPosition = new Vector3(0, (float)((0.02 * (Altitude % 10000) / 10000) + 0.16), 0);
            Alt_Dial_10_R.localPosition = new Vector3(0, (float)((0.02 * (Altitude % 100) / 100) + 0.17), 0);
            Alt_Dial_100_R.localPosition = new Vector3(0, (float)((0.02 * (Altitude % 1000) / 1000) + 0.16), 0);
            Alt_Dial_1000_R.localPosition = new Vector3(0, (float)((0.02 * (Altitude % 10000) / 10000) + 0.155), 0);

            //Leverage pointers
            altpointerold = altpointer;
            #endregion

            #region Comm
            //Comm Status
            commStatus = vessel.connection.SignalStrength;
            commpointer = 0;

            if (commStatus > 0)
            {
                commpointer++;
            }
            if (commStatus > 0.35)
            {
                commpointer++;
            }
            if (commStatus > 0.75)
            {
                commpointer++;
            }
            
            //check if comm state changed and set accordingly
            if (commpointer != commpointerold)
            {
                switch (commpointer)
                {
                    case 1:
                        commOnOff = part.transform.FindRecursive("comm_off_1");
                        commOnOff.gameObject.SetActive(false);
                        commOnOff = part.transform.FindRecursive("comm_off_2");
                        commOnOff.gameObject.SetActive(true);
                        commOnOff = part.transform.FindRecursive("comm_off_3");
                        commOnOff.gameObject.SetActive(true);
                        commOnOff = part.transform.FindRecursive("comm_on_1");
                        commOnOff.gameObject.SetActive(true);
                        commOnOff = part.transform.FindRecursive("comm_on_2");
                        commOnOff.gameObject.SetActive(false);
                        commOnOff = part.transform.FindRecursive("comm_on_3");
                        commOnOff.gameObject.SetActive(false);
                        break;
                    case 2:
                        commOnOff = part.transform.FindRecursive("comm_off_1");
                        commOnOff.gameObject.SetActive(false);
                        commOnOff = part.transform.FindRecursive("comm_off_2");
                        commOnOff.gameObject.SetActive(false);
                        commOnOff = part.transform.FindRecursive("comm_off_3");
                        commOnOff.gameObject.SetActive(true);
                        commOnOff = part.transform.FindRecursive("comm_on_1");
                        commOnOff.gameObject.SetActive(true);
                        commOnOff = part.transform.FindRecursive("comm_on_2");
                        commOnOff.gameObject.SetActive(true);
                        commOnOff = part.transform.FindRecursive("comm_on_3");
                        commOnOff.gameObject.SetActive(false);
                        break;
                    case 3:
                        commOnOff = part.transform.FindRecursive("comm_off_1");
                        commOnOff.gameObject.SetActive(false);
                        commOnOff = part.transform.FindRecursive("comm_off_2");
                        commOnOff.gameObject.SetActive(false);
                        commOnOff = part.transform.FindRecursive("comm_off_3");
                        commOnOff.gameObject.SetActive(false);
                        commOnOff = part.transform.FindRecursive("comm_on_1");
                        commOnOff.gameObject.SetActive(true);
                        commOnOff = part.transform.FindRecursive("comm_on_2");
                        commOnOff.gameObject.SetActive(true);
                        commOnOff = part.transform.FindRecursive("comm_on_3");
                        commOnOff.gameObject.SetActive(true);
                        break;
                    default:
                        commOnOff = part.transform.FindRecursive("comm_off_1");
                        commOnOff.gameObject.SetActive(true);
                        commOnOff = part.transform.FindRecursive("comm_off_2");
                        commOnOff.gameObject.SetActive(true);
                        commOnOff = part.transform.FindRecursive("comm_off_3");
                        commOnOff.gameObject.SetActive(true);
                        commOnOff = part.transform.FindRecursive("comm_on_1");
                        commOnOff.gameObject.SetActive(false);
                        commOnOff = part.transform.FindRecursive("comm_on_2");
                        commOnOff.gameObject.SetActive(false);
                        commOnOff = part.transform.FindRecursive("comm_on_3");
                        commOnOff.gameObject.SetActive(false);
                        break;
                }
            }
            commpointerold = commpointer;
            #endregion

            #endregion
        }

    }
}
