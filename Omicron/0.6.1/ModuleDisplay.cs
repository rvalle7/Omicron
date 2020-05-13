
using TMPro;
using UnityEngine;

namespace Omicron
{
    class ModuleDisplay : PartModule
    {
        #region Var
        //Altitude
        private double Altitude;
        private TextMeshPro LCDTextMesh = null;
        private TextMeshPro LCDTextMeshR = null;
        private string ScreenName = "tmp_altitude_left";
        private string ScreenNameR = "tmp_altitude_right";
        private Mesh M;
        private Mesh MR;
        private RectTransform T;
        private RectTransform TR;

        //Speed
        private double Speed;
        private TextMeshPro LCDTextMeshS = null;
        private TextMeshPro LCDTextMeshSR = null;
        private string ScreenNameS = "tmp_speed_left";
        private string ScreenNameSR = "tmp_speed_right";
        private Mesh MS;
        private Mesh MSR;
        private RectTransform TS;
        private RectTransform TSR;

        //Vertical Speed
        private double Vspeed;
        private TextMeshPro LCDTextMeshVS = null;
        private TextMeshPro LCDTextMeshVSR = null;
        private string ScreenNameVS = "tmp_vert_speed_left";
        private string ScreenNameVSR = "tmp_vert_speed_right";
        private Mesh MVS;
        private Mesh MVSR;
        private RectTransform TVS;
        private RectTransform TVSR;

        //Horizontal Speed
        private double Hspeed;
        private TextMeshPro LCDTextMeshHS = null;
        private TextMeshPro LCDTextMeshHSR = null;
        private string ScreenNameHS = "tmp_hor_speed_left";
        private string ScreenNameHSR = "tmp_hor_speed_right";
        private Mesh MHS;
        private Mesh MHSR;
        private RectTransform THS;
        private RectTransform THSR;

        //Periapsis
        private double Periapsis;
        private TextMeshPro LCDTextMeshPe = null;
        private TextMeshPro LCDTextMeshPeR = null;
        private string ScreenNamePe = "tmp_pe_left";
        private string ScreenNamePeR = "tmp_pe_right";
        private Mesh MPe;
        private Mesh MPeR;
        private RectTransform TPe;
        private RectTransform TPeR;

        //Apoapsis
        private double Apoapsis;
        private TextMeshPro LCDTextMeshAp = null;
        private TextMeshPro LCDTextMeshApR = null;
        private string ScreenNameAp = "tmp_ap_left";
        private string ScreenNameApR = "tmp_ap_right";
        private Mesh MAp;
        private Mesh MApR;
        private RectTransform TAp;
        private RectTransform TApR;

        //Vtol Attitude Display
        private Transform VtolTrans;
        private double VtolAngle;
        private TextMeshPro LCDTextMeshVtol = null;
        private string ScreenNameVtol = "tmp_vtol_angle";
        private Mesh MVtol;
        private RectTransform TVtol;

        //Generic
        //private string Unit = "m/s";
        private TMP_FontAsset[] loadedFonts;
        readonly bool autoFont = false;
        #endregion

        #region Fields

        [
            KSPAxisField
            (
                guiName = "Vtol_angle",
                isPersistant = true,
                guiActive = true,
                guiActiveEditor = true,
                axisMode = KSPAxisMode.Incremental,
                minValue = 0f,
                maxValue = 90f,
                incrementalSpeed = 1f
            ),
            UI_FloatRange
            (
                scene = UI_Scene.All,
                affectSymCounterparts = UI_Scene.All,
                controlEnabled = true,
                minValue = 0f,
                maxValue = 90f,
                stepIncrement = 1f
            )
        ]
        public float VtolAngleE = 90f;

        private BaseAxisField VtolAngleAxis
        {
            get
            {
                return (BaseAxisField)Fields["VtolAngleE"];
            }
        }

        #endregion

        #region OnCopy
        //Check for when you copy a part on SPH or VAB
        public override void OnCopy(PartModule fromModule)
        {
            base.OnCopy(fromModule);
            TextMeshPro thisMesh = GetComponentInChildren<TextMeshPro>();
            if (thisMesh != null)
            {
                thisMesh.gameObject.DestroyGameObject();
            }
        }
        #endregion

        #region OnAwake
        //Load Fonts
        public override void OnAwake()
        {
            loadedFonts = Resources.FindObjectsOfTypeAll<TMP_FontAsset>();
        }
        #endregion

        #region Start
        public void Start()
        {
            #region Formating Display Altitude Left

            GameObject LCDScreen = new GameObject();
            Transform screenTransform = part.FindModelTransform(ScreenName);
            LCDScreen.transform.parent = screenTransform;
            LCDScreen.transform.localRotation = screenTransform.localRotation;
            LCDScreen.transform.localRotation = Quaternion.Euler(0, 0, 90);
            LCDTextMesh = LCDScreen.AddComponent<TextMeshPro>();
            M = screenTransform.GetComponent<MeshFilter>().mesh;
            T = LCDTextMesh.gameObject.GetComponent<RectTransform>();
            T.sizeDelta = new Vector2(M.bounds.size.y / 6, M.bounds.size.x / 6);
            LCDScreen.transform.localPosition = new Vector3(0, 0, (M.bounds.size.z / 2) + 0.01f);
            LCDTextMesh.fontSize = 0.22f;
            LCDTextMesh.enableAutoSizing = autoFont;
            LCDTextMesh.color = Color.white;
            LCDTextMesh.font = loadedFonts[1];
            LCDTextMesh.fontSizeMin = 0.01f;
            LCDTextMesh.overflowMode = TextOverflowModes.Truncate;
            LCDTextMesh.alignment = TextAlignmentOptions.Center;
            LCDTextMesh.text = "0.0";
            #endregion

            #region Formating Display Altitude Right

            GameObject LCDScreenR = new GameObject();
            Transform screenTransformR = part.FindModelTransform(ScreenNameR);
            LCDScreenR.transform.parent = screenTransformR;
            LCDScreenR.transform.localRotation = screenTransformR.localRotation;
            LCDScreenR.transform.localRotation = Quaternion.Euler(0, 0, 90);
            LCDTextMeshR = LCDScreenR.AddComponent<TextMeshPro>();
            MR = screenTransformR.GetComponent<MeshFilter>().mesh;
            TR = LCDTextMeshR.gameObject.GetComponent<RectTransform>();
            TR.sizeDelta = new Vector2(MR.bounds.size.y / 6, MR.bounds.size.x / 6);
            LCDScreenR.transform.localPosition = new Vector3(0, 0, (MR.bounds.size.z / 2) + 0.01f);
            LCDTextMeshR.fontSize = 0.22f;
            LCDTextMeshR.enableAutoSizing = autoFont;
            LCDTextMeshR.color = Color.white;
            LCDTextMeshR.font = loadedFonts[1];
            LCDTextMeshR.fontSizeMin = 0.01f;
            LCDTextMeshR.overflowMode = TextOverflowModes.Truncate;
            LCDTextMeshR.alignment = TextAlignmentOptions.Center;
            LCDTextMeshR.text = "0.0";
            #endregion

            #region Formating Display Periapsis Left

            GameObject LCDScreenPe = new GameObject();
            Transform screenTransformPe = part.FindModelTransform(ScreenNamePe);
            LCDScreenPe.transform.parent = screenTransformPe;
            LCDScreenPe.transform.localRotation = screenTransformPe.localRotation;
            LCDScreenPe.transform.localRotation = Quaternion.Euler(0, 0, 90);
            LCDTextMeshPe = LCDScreenPe.AddComponent<TextMeshPro>();
            MPe = screenTransformPe.GetComponent<MeshFilter>().mesh;
            TPe = LCDTextMeshPe.gameObject.GetComponent<RectTransform>();
            TPe.sizeDelta = new Vector2(MPe.bounds.size.y / 6, MPe.bounds.size.x / 6);
            LCDScreenPe.transform.localPosition = new Vector3(0, 0, (MPe.bounds.size.z / 2) + 0.01f);
            LCDTextMeshPe.fontSize = 0.12f;
            LCDTextMeshPe.enableAutoSizing = autoFont;
            LCDTextMeshPe.color = Color.white;
            LCDTextMeshPe.font = loadedFonts[1];
            LCDTextMeshPe.fontSizeMin = 0.01f;
            LCDTextMeshPe.overflowMode = TextOverflowModes.Truncate;
            LCDTextMeshPe.alignment = TextAlignmentOptions.Center;
            LCDTextMeshPe.text = "";
            #endregion

            #region Formating Display Periapsis Right

            GameObject LCDScreenPeR = new GameObject();
            Transform screenTransformPeR = part.FindModelTransform(ScreenNamePeR);
            LCDScreenPeR.transform.parent = screenTransformPeR;
            LCDScreenPeR.transform.localRotation = screenTransformPeR.localRotation;
            LCDScreenPeR.transform.localRotation = Quaternion.Euler(0, 0, 90);
            LCDTextMeshPeR = LCDScreenPeR.AddComponent<TextMeshPro>();
            MPeR = screenTransformPeR.GetComponent<MeshFilter>().mesh;
            TPeR = LCDTextMeshPeR.gameObject.GetComponent<RectTransform>();
            TPeR.sizeDelta = new Vector2(MPeR.bounds.size.y / 6, MPeR.bounds.size.x / 6);
            LCDScreenPeR.transform.localPosition = new Vector3(0, 0, (MPeR.bounds.size.z / 2) + 0.01f);
            LCDTextMeshPeR.fontSize = 0.12f;
            LCDTextMeshPeR.enableAutoSizing = autoFont;
            LCDTextMeshPeR.color = Color.white;
            LCDTextMeshPeR.font = loadedFonts[1];
            LCDTextMeshPeR.fontSizeMin = 0.01f;
            LCDTextMeshPeR.overflowMode = TextOverflowModes.Truncate;
            LCDTextMeshPeR.alignment = TextAlignmentOptions.Center;
            LCDTextMeshPeR.text = "";
            #endregion

            #region Formating Display Apoapsis Left

            GameObject LCDScreenAp = new GameObject();
            Transform screenTransformAp = part.FindModelTransform(ScreenNameAp);
            LCDScreenAp.transform.parent = screenTransformAp;
            LCDScreenAp.transform.localRotation = screenTransformAp.localRotation;
            LCDScreenAp.transform.localRotation = Quaternion.Euler(0, 0, 90);
            LCDTextMeshAp = LCDScreenAp.AddComponent<TextMeshPro>();
            MAp = screenTransformAp.GetComponent<MeshFilter>().mesh;
            TAp = LCDTextMeshAp.gameObject.GetComponent<RectTransform>();
            TAp.sizeDelta = new Vector2(MAp.bounds.size.y / 6, MAp.bounds.size.x / 6);
            LCDScreenAp.transform.localPosition = new Vector3(0, 0, (MAp.bounds.size.z / 2) + 0.01f);
            LCDTextMeshAp.fontSize = 0.12f;
            LCDTextMeshAp.enableAutoSizing = autoFont;
            LCDTextMeshAp.color = Color.white;
            LCDTextMeshAp.font = loadedFonts[1];
            LCDTextMeshAp.fontSizeMin = 0.01f;
            LCDTextMeshAp.overflowMode = TextOverflowModes.Truncate;
            LCDTextMeshAp.alignment = TextAlignmentOptions.Center;
            LCDTextMeshAp.text = "";
            #endregion

            #region Formating Display Apoapsis Left

            GameObject LCDScreenApR = new GameObject();
            Transform screenTransformApR = part.FindModelTransform(ScreenNameApR);
            LCDScreenApR.transform.parent = screenTransformApR;
            LCDScreenApR.transform.localRotation = screenTransformApR.localRotation;
            LCDScreenApR.transform.localRotation = Quaternion.Euler(0, 0, 90);
            LCDTextMeshApR = LCDScreenApR.AddComponent<TextMeshPro>();
            MApR = screenTransformApR.GetComponent<MeshFilter>().mesh;
            TApR = LCDTextMeshApR.gameObject.GetComponent<RectTransform>();
            TApR.sizeDelta = new Vector2(MApR.bounds.size.y / 6, MApR.bounds.size.x / 6);
            LCDScreenApR.transform.localPosition = new Vector3(0, 0, (MApR.bounds.size.z / 2) + 0.01f);
            LCDTextMeshApR.fontSize = 0.12f;
            LCDTextMeshApR.enableAutoSizing = autoFont;
            LCDTextMeshApR.color = Color.white;
            LCDTextMeshApR.font = loadedFonts[1];
            LCDTextMeshApR.fontSizeMin = 0.01f;
            LCDTextMeshApR.overflowMode = TextOverflowModes.Truncate;
            LCDTextMeshApR.alignment = TextAlignmentOptions.Center;
            LCDTextMeshApR.text = "";
            #endregion

            #region Formating Display Speed Left

            GameObject LCDScreenS = new GameObject();
            Transform screenTransformS = part.FindModelTransform(ScreenNameS);
            LCDScreenS.transform.parent = screenTransformS;
            LCDScreenS.transform.localRotation = screenTransformS.localRotation;
            LCDScreenS.transform.localRotation = Quaternion.Euler(0, 0, 90);
            LCDTextMeshS = LCDScreenS.AddComponent<TextMeshPro>();
            MS = screenTransformS.GetComponent<MeshFilter>().mesh;
            TS = LCDTextMeshS.gameObject.GetComponent<RectTransform>();
            TS.sizeDelta = new Vector2(MS.bounds.size.y / 7, MS.bounds.size.x / 7);
            LCDScreenS.transform.localPosition = new Vector3(0, 0, (MS.bounds.size.z / 2) + 0.01f);
            LCDTextMeshS.fontSize = 0.2f;
            LCDTextMeshS.enableAutoSizing = autoFont;
            LCDTextMeshS.color = Color.white;
            LCDTextMeshS.font = loadedFonts[1];
            LCDTextMeshS.fontSizeMin = 0.01f;
            LCDTextMeshS.overflowMode = TextOverflowModes.Truncate;
            LCDTextMeshS.alignment = TextAlignmentOptions.Center;
            LCDTextMeshS.text = "0.0";
            #endregion

            #region Formating Display Speed Right

            GameObject LCDScreenSR = new GameObject();
            Transform screenTransformSR = part.FindModelTransform(ScreenNameSR);
            LCDScreenSR.transform.parent = screenTransformSR;
            LCDScreenSR.transform.localRotation = screenTransformSR.localRotation;
            LCDScreenSR.transform.localRotation = Quaternion.Euler(0, 0, 90);
            LCDTextMeshSR = LCDScreenSR.AddComponent<TextMeshPro>();
            MSR = screenTransformSR.GetComponent<MeshFilter>().mesh;
            TSR = LCDTextMeshSR.gameObject.GetComponent<RectTransform>();
            TSR.sizeDelta = new Vector2(MSR.bounds.size.y / 7, MSR.bounds.size.x / 7);
            LCDScreenSR.transform.localPosition = new Vector3(0, 0, (MSR.bounds.size.z / 2) + 0.01f);
            LCDTextMeshSR.fontSize = 0.2f;
            LCDTextMeshSR.enableAutoSizing = autoFont;
            LCDTextMeshSR.color = Color.white;
            LCDTextMeshSR.font = loadedFonts[1];
            LCDTextMeshSR.fontSizeMin = 0.01f;
            LCDTextMeshSR.overflowMode = TextOverflowModes.Truncate;
            LCDTextMeshSR.alignment = TextAlignmentOptions.Center;
            LCDTextMeshSR.text = "0.0";
            #endregion

            #region Formating Display Vertical Speed Left

            GameObject LCDScreenVS = new GameObject();
            Transform screenTransformVS = part.FindModelTransform(ScreenNameVS);
            LCDScreenVS.transform.parent = screenTransformVS;
            LCDScreenVS.transform.localRotation = screenTransformVS.localRotation;
            LCDScreenVS.transform.localRotation = Quaternion.Euler(0, 0, 90);
            LCDTextMeshVS = LCDScreenVS.AddComponent<TextMeshPro>();
            MVS = screenTransformVS.GetComponent<MeshFilter>().mesh;
            TVS = LCDTextMeshVS.gameObject.GetComponent<RectTransform>();
            TVS.sizeDelta = new Vector2(MVS.bounds.size.y / 7, MVS.bounds.size.x / 7);
            LCDScreenVS.transform.localPosition = new Vector3(0, 0, (MVS.bounds.size.z / 2) + 0.01f);
            LCDTextMeshVS.fontSize = 0.12f;
            LCDTextMeshVS.enableAutoSizing = autoFont;
            LCDTextMeshVS.color = new Color32(255, 250, 200, 200);
            LCDTextMeshVS.font = loadedFonts[1];
            LCDTextMeshVS.fontSizeMin = 0.01f;
            LCDTextMeshVS.overflowMode = TextOverflowModes.Truncate;
            LCDTextMeshVS.alignment = TextAlignmentOptions.Center;
            LCDTextMeshVS.text = "";
            #endregion

            #region Formating Display Vertical Speed Right

            GameObject LCDScreenVSR = new GameObject();
            Transform screenTransformVSR = part.FindModelTransform(ScreenNameVSR);
            LCDScreenVSR.transform.parent = screenTransformVSR;
            LCDScreenVSR.transform.localRotation = screenTransformVSR.localRotation;
            LCDScreenVSR.transform.localRotation = Quaternion.Euler(0, 0, 90);
            LCDTextMeshVSR = LCDScreenVSR.AddComponent<TextMeshPro>();
            MVSR = screenTransformVSR.GetComponent<MeshFilter>().mesh;
            TVSR = LCDTextMeshVSR.gameObject.GetComponent<RectTransform>();
            TVSR.sizeDelta = new Vector2(MVSR.bounds.size.y / 7, MVSR.bounds.size.x / 7);
            LCDScreenVSR.transform.localPosition = new Vector3(0, 0, (MVSR.bounds.size.z / 2) + 0.01f);
            LCDTextMeshVSR.fontSize = 0.12f;
            LCDTextMeshVSR.enableAutoSizing = autoFont;
            LCDTextMeshVSR.color = new Color32(255, 250, 200, 200);
            LCDTextMeshVSR.font = loadedFonts[1];
            LCDTextMeshVSR.fontSizeMin = 0.01f;
            LCDTextMeshVSR.overflowMode = TextOverflowModes.Truncate;
            LCDTextMeshVSR.alignment = TextAlignmentOptions.Center;
            LCDTextMeshVSR.text = "";
            #endregion

            #region Formating Display Horizontal Speed Left

            GameObject LCDScreenHS = new GameObject();
            Transform screenTransformHS = part.FindModelTransform(ScreenNameHS);
            LCDScreenHS.transform.parent = screenTransformHS;
            LCDScreenHS.transform.localRotation = screenTransformHS.localRotation;
            LCDScreenHS.transform.localRotation = Quaternion.Euler(0, 0, 90);
            LCDTextMeshHS = LCDScreenHS.AddComponent<TextMeshPro>();
            MHS = screenTransformHS.GetComponent<MeshFilter>().mesh;
            THS = LCDTextMeshHS.gameObject.GetComponent<RectTransform>();
            THS.sizeDelta = new Vector2(MHS.bounds.size.y / 7, MHS.bounds.size.x / 7);
            LCDScreenHS.transform.localPosition = new Vector3(0, 0, (MHS.bounds.size.z / 2) + 0.01f);
            LCDTextMeshHS.fontSize = 0.12f;
            LCDTextMeshHS.enableAutoSizing = autoFont;
            LCDTextMeshHS.color = new Color32(255, 200, 200, 200);
            LCDTextMeshHS.font = loadedFonts[1];
            LCDTextMeshHS.fontSizeMin = 0.01f;
            LCDTextMeshHS.overflowMode = TextOverflowModes.Truncate;
            LCDTextMeshHS.alignment = TextAlignmentOptions.Center;
            LCDTextMeshHS.text = "";
            #endregion

            #region Formating Display Horizontal Speed Right

            GameObject LCDScreenHSR = new GameObject();
            Transform screenTransformHSR = part.FindModelTransform(ScreenNameHSR);
            LCDScreenHSR.transform.parent = screenTransformHSR;
            LCDScreenHSR.transform.localRotation = screenTransformHSR.localRotation;
            LCDScreenHSR.transform.localRotation = Quaternion.Euler(0, 0, 90);
            LCDTextMeshHSR = LCDScreenHSR.AddComponent<TextMeshPro>();
            MHSR = screenTransformHSR.GetComponent<MeshFilter>().mesh;
            THSR = LCDTextMeshHSR.gameObject.GetComponent<RectTransform>();
            THSR.sizeDelta = new Vector2(MHSR.bounds.size.y / 7, MHSR.bounds.size.x / 7);
            LCDScreenHSR.transform.localPosition = new Vector3(0, 0, (MHSR.bounds.size.z / 2) + 0.01f);
            LCDTextMeshHSR.fontSize = 0.12f;
            LCDTextMeshHSR.enableAutoSizing = autoFont;
            LCDTextMeshHSR.color = new Color32(255, 200, 200, 200);
            LCDTextMeshHSR.font = loadedFonts[1];
            LCDTextMeshHSR.fontSizeMin = 0.01f;
            LCDTextMeshHSR.overflowMode = TextOverflowModes.Truncate;
            LCDTextMeshHSR.alignment = TextAlignmentOptions.Center;
            LCDTextMeshHSR.text = "";
            #endregion

            #region Formating Display Vtol Angle

            GameObject LCDScreenVtol = new GameObject();
            Transform screenTransformVtol = part.FindModelTransform(ScreenNameVtol);
            LCDScreenVtol.transform.parent = screenTransformVtol;
            LCDScreenVtol.transform.localRotation = screenTransformVtol.localRotation;
            LCDScreenVtol.transform.localRotation = Quaternion.Euler(0, 0, 90);
            LCDTextMeshVtol = LCDScreenVtol.AddComponent<TextMeshPro>();
            MVtol = screenTransformVtol.GetComponent<MeshFilter>().mesh;
            TVtol = LCDTextMeshVtol.gameObject.GetComponent<RectTransform>();
            TVtol.sizeDelta = new Vector2(MVtol.bounds.size.y, MVtol.bounds.size.x);
            LCDScreenVtol.transform.localPosition = new Vector3(0, 0, (MVtol.bounds.size.z / 2) + 0.01f);
            LCDTextMeshVtol.fontSize = 0.22f;
            LCDTextMeshVtol.enableAutoSizing = autoFont;
            LCDTextMeshVtol.color = new Color32(255, 255, 255, 255);
            LCDTextMeshVtol.font = loadedFonts[1];
            LCDTextMeshVtol.fontSizeMin = 0.01f;
            LCDTextMeshVtol.overflowMode = TextOverflowModes.Truncate;
            LCDTextMeshVtol.alignment = TextAlignmentOptions.Center;
            LCDTextMeshVtol.text = "Vtol";
            #endregion

            #region Vtol Attitude Control (Menus, Actions, Editor and Flight)

            UI_FloatRange uiRange1 = (UI_FloatRange)VtolAngleAxis.uiControlEditor;
            UI_FloatRange uiRange2 = (UI_FloatRange)VtolAngleAxis.uiControlFlight;
            VtolAngleAxis.minValue = uiRange1.minValue = uiRange2.minValue = 0f;
            VtolAngleAxis.maxValue = uiRange1.maxValue = uiRange2.maxValue = 90f;
            uiRange1.stepIncrement = uiRange2.stepIncrement = 1f;
            VtolAngleAxis.incrementalSpeed = 25f;
            VtolAngleAxis.guiActiveEditor = true;
            VtolAngleAxis.guiActive = true;
            VtolAngleAxis.guiActiveUnfocused = false;
            VtolAngleAxis.guiInteractable = true;

            VtolTrans = part.transform.FindRecursive("attitude_node");
            #endregion
        }
        #endregion

        #region Update

        public void Update()
        {
            if (HighLogic.LoadedSceneIsEditor)
            {
                #region Control Vtol Attitude on Editor

                VtolTrans.localRotation = Quaternion.Euler(-VtolAngleE, 0f, 0f);
                VtolAngle = -VtolTrans.localRotation.x * 128;
                string VtlE = VtolAngle.ToString("#0.#");
                LCDTextMeshVtol.text = VtlE + "°";
                return;
                #endregion
            }
            #region Display Altitude
            //Check if KSP Altitude Display is AGL or MSL
            if (vessel.altimeterDisplayState == AltimeterDisplayState.AGL)
            {
                Altitude = vessel.radarAltitude;
            }
            else
            {
                Altitude = vessel.altitude;
            }

            //Convert Meters, Kilometers, Million Meters etc.
            while (Altitude < 0)
            {
                Altitude *= 1000;
            }

            while (Altitude > 1000000)
            {
                Altitude /= 1000;
            }

            //Display Altitude
            string Alt = Altitude.ToString("0.0");
            LCDTextMesh.text = Alt;
            LCDTextMeshR.text = Alt;
            #endregion

            #region Display Speed
            //Check if in Atmo and Above Surface Speed
            if (vessel.atmDensity < 0.2 & vessel.altitude > 25000)
            {
                Speed = vessel.obt_speed;
            }
            else
            {
                Speed = vessel.srfSpeed;
            };

            //Convert Meters, Kilometers, Million Meters, etc per second.
            //while (Speed < 0)
            //{
            //    Speed *= 1000;
            //}

            //while (Speed > 1000000)
            //{
            //    Speed /= 1000;
            //}

            //Display Speed
            string Spd = Speed.ToString("0.0");
            LCDTextMeshS.text = Spd;
            LCDTextMeshSR.text = Spd;
            #endregion

            #region Condition to Show Vspeed and Hspeed -> Flying or Sub-Orbital

            if (vessel.SituationString == "FLYING" || vessel.SituationString == "SUB_ORBITAL")
            {
                #region Display Vertical Speed

                Vspeed = vessel.verticalSpeed;
                string Vspd = Vspeed.ToString("0.0");
                LCDTextMeshVS.text = "v" + Vspd;
                LCDTextMeshVSR.text = "v" + Vspd;
                #endregion


                #region Display Horizontal Speed

                Hspeed = vessel.horizontalSrfSpeed;
                string Hspd = Hspeed.ToString("0.0");
                LCDTextMeshHS.text = "h" + Hspd;
                LCDTextMeshHSR.text = "h" + Hspd;
                #endregion
            }
            else
            {
                #region Vertical and Horizontal Speed cleanup

                LCDTextMeshVS.text = "";        // + Unit;
                LCDTextMeshVSR.text = "";     // + Unit;
                LCDTextMeshHS.text = "";      // + Unit;
                LCDTextMeshHSR.text = "";     // + Unit;
                #endregion
            }
            #endregion

            #region Condition to Show Apoapsis

            if (vessel.SituationString == "FLYING" || vessel.SituationString == "SUB_ORBITAL" || vessel.SituationString == "ORBITING" || vessel.SituationString == "DOCKED")
            {
                #region Display Apoapsis

                Apoapsis = vessel.orbit.ApA;
                string Apo = Apoapsis.ToString("0.0");
                LCDTextMeshAp.text = "Ap" + Apo;
                LCDTextMeshApR.text = "Ap" + Apo;
                #endregion
            }
            else
            {
                #region Clean Apoapsis if condition false

                LCDTextMeshAp.text = "";
                LCDTextMeshApR.text = "";
                #endregion
            }
            #endregion

            #region Condition to Show Periapsis

            if (vessel.SituationString == "ESCAPING" || vessel.SituationString == "ORBITING" || vessel.SituationString == "DOCKED")
            {
                #region Display Periapsis

                Periapsis = vessel.orbit.PeA;
                string Per = Periapsis.ToString("0.0");
                LCDTextMeshPe.text = "Pe" + Per;
                LCDTextMeshPeR.text = "Pe" + Per;
                #endregion
            }
            else
            {
                #region Clean Periapsis if condition false

                LCDTextMeshPe.text = "";
                LCDTextMeshPeR.text = "";
                #endregion
            }
            #endregion

            #region Display Vtol Angle and control Vtol Attitude

            VtolTrans.localRotation = Quaternion.Euler(-VtolAngleE, 0f, 0f);
            VtolAngle = -VtolTrans.localRotation.x * 128;
            string Vtl = VtolAngle.ToString("#0.#");
            LCDTextMeshVtol.text = Vtl + "°";

            #endregion
        }
        #endregion
    }
}