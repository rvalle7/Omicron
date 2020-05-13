using System;
using UnityEngine;
using KSP.UI.Screens.Flight;
using TMPro;
using UnityEngine.UI;

namespace Omicron
{
    [KSPModule("ModuleOmicron")]
    public class ModuleOmicron : PartModule
    {
        #region Var
        //NavBall
        private NavBall stockNavball;
        private Transform myNavballTrans;
        private string NavBallName = "navball_node";

        //SAS
        private bool SASstat;
        private Transform SASglass;
        private string SAS_Name = "SAS_glass";

        //RCS
        private bool RCSstat;
        private Transform RCSglass;
        private string RCS_Name = "RCS_glass";

        //Throttle
        private Transform Throttle_Dial_L;
        private Transform Throttle_Dial_R;
        private Transform Throttle_L;
        private Transform Throttle_R;
        private float Throttle;
        private string Throttle_Dial_Left_Name = "dial_throttle_left";
        private string Throttle_Left_Name = "throttle_left";
        private string Throttle_Dial_Right_Name = "dial_throttle_right";
        private string Throttle_Right_Name = "throttle_right";

        //Engine Status
        private Transform EngLedTrans_left;
        private Transform EngLedrans_right;
        private string EngLedName_left = "eng_status_left";
        private string EngLedName_right = "eng_status_right";
        private bool EnginePointer = false;
        private float ScreenTime = 3.0f;

        private TextMeshPro LCDTextMeshEng = null;
        private string ScreenNameEng = "tmp_engine_status";
        private Mesh MEng;
        private RectTransform TEng;
        private string Eng_info = "offline";

        //Speed
        private Transform Speed_Dial_10_L;
        private Transform Speed_Dial_100_L;
        private Transform Speed_Dial_1000_L;
        private Transform Speed_Dial_10_R;
        private Transform Speed_Dial_100_R;
        private Transform Speed_Dial_1000_R;
        private double Speed;
        private string Speed_Dial_10_Left_Name = "Dial_10_speed_left";
        private string Speed_Dial_100_Left_Name = "Dial_100_speed_left";
        private string Speed_Dial_1000_Left_Name = "Dial_1000_speed_left";
        private string Speed_Dial_10_Right_Name = "Dial_10_speed_right";
        private string Speed_Dial_100_Right_Name = "Dial_100_speed_right";
        private string Speed_Dial_1000_Right_Name = "Dial_1000_speed_right";

        private TextMeshPro LCDTextMeshS = null;
        private TextMeshPro LCDTextMeshSR = null;
        private string ScreenNameS = "tmp_speed_left";
        private string ScreenNameSR = "tmp_speed_right";
        private Mesh MS;
        private Mesh MSR;
        private RectTransform TS;
        private RectTransform TSR;
        private string SpdUnity = "m/s";

        //Unit Display for Speed
        private TextMeshPro LCDTextMeshSp = null;
        private TextMeshPro LCDTextMeshSpR = null;
        private string ScreenNameSp = "tmp_speedunit_left";
        private string ScreenNameSpR = "tmp_speedunit_right";
        private Mesh MSp;
        private Mesh MSpR;
        private RectTransform TSp;
        private RectTransform TSpR;

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

        //Altitude
        private Transform Alt_Dial_10_L;
        private Transform Alt_Dial_100_L;
        private Transform Alt_Dial_1000_L;
        private Transform Alt_Dial_10_R;
        private Transform Alt_Dial_100_R;
        private Transform Alt_Dial_1000_R;
        private double Altitude;
        private string Alt_Dial_10_Left_Name = "Dial_10_altitude_left";
        private string Alt_Dial_100_Left_Name = "Dial_100_altitude_left";
        private string Alt_Dial_1000_Left_Name = "Dial_1000_altitude_left";
        private string Alt_Dial_10_Right_Name = "Dial_10_altitude_right";
        private string Alt_Dial_100_Right_Name = "Dial_100_altitude_right";
        private string Alt_Dial_1000_Right_Name = "Dial_1000_altitude_right";

        private TextMeshPro LCDTextMesh = null;
        private TextMeshPro LCDTextMeshR = null;
        private string ScreenName = "tmp_altitude_left";
        private string ScreenNameR = "tmp_altitude_right";
        private Mesh M;
        private Mesh MR;
        private RectTransform T;
        private RectTransform TR;
        //pointer for altitude
        private int altpointer;
        private int altpointerold;

        //Unit Display for Altitude
        private TextMeshPro LCDTextMeshAU = null;
        private TextMeshPro LCDTextMeshAUR = null;
        private string ScreenNameAU = "tmp_altunit_left";
        private string ScreenNameAUR = "tmp_altunit_right";
        private Mesh MAU;
        private Mesh MAUR;
        private RectTransform TAU;
        private RectTransform TAUR;
        private string AltUnity = "meters";

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

        //Signal Strength
        private double commStatus;
        private Transform commOnOff;
        //pointer for comm
        private int commpointer;
        private int commpointerold;

        //Generic
        private TMP_FontAsset[] loadedFonts;
        readonly bool autoFont = false;
        #endregion

        #region Fields
        [KSPField(isPersistant = true, guiName = "Engine Status", guiActive = true, guiActiveEditor = true)]
        private string EngineStatus = "Off";

        [KSPField(isPersistant = true, guiName = "Engine Mode", guiActive = true, guiActiveEditor = true)]
        private string EngineMode = "Air Breathing";
        #endregion

        #region Events
        [KSPEvent(name = "Toggle Engine", guiActive = true, guiActiveEditor = true, guiActiveUnfocused = false, guiName = "Toggle Engine")]
        public void ToggleEngine()
        {
            if (EngineStatus == "On")
            {
                EngineStatus = "Off";
            }
            else
            {
                EngineStatus = "On";
            }
        }

        [KSPEvent(name = "Switch Engine Mode", guiActive = true, guiActiveEditor = true, guiActiveUnfocused = false, guiName = "Switch Engine Mode")]
        public void SwitchEngineMode()
        {
            if (EngineMode == "Air Breathing")
            {
                EngineMode = "Rocket";
            }
            else
            {
                EngineMode = "Air Breathing";
            }
        }

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

        #region Actions
        [KSPAction("Toggle Engine")]
        public void doToggleEngine(KSPActionParam Par)
        {
            ToggleEngine();
        }

        [KSPAction("Engine On")]
        public void doEngineOn(KSPActionParam Par)
        {
            EngineStatus = "Off";
            ToggleEngine();
        }

        [KSPAction("Engine Off")]
        public void doEngineOff(KSPActionParam Par)
        {
            EngineStatus = "On";
            ToggleEngine();
        }

        [KSPAction("Switch Engine Mode")]
        public void doSwitchEngineMode(KSPActionParam Par)
        {
            SwitchEngineMode();
        }

        [KSPAction("Engine Air breathing Mode")]
        public void doEngineAir(KSPActionParam Par)
        {
            EngineMode = "Rocket";
            SwitchEngineMode();
        }

        [KSPAction("Engine Rocket Mode")]
        public void doEngineRocket(KSPActionParam Par)
        {
            EngineMode = "Air Breathing";
            SwitchEngineMode();
        }


        #endregion

        #region OnCopy Check TextMesh Copy
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

        #region OnAwake Load TMP Fonts
        //Load Fonts
        public override void OnAwake()
        {
            loadedFonts = Resources.FindObjectsOfTypeAll<TMP_FontAsset>();
        }
        #endregion

        #region OnStart Hide Transforms for Speed and Altitude Units, SAS, RCS, Comm and Engine Led's
        //Hide Altimeter and Speed  and Comm transformers on Start
        public override void OnStart(PartModule.StartState state)
        {
            foreach (Transform Obj in part.GetComponentsInChildren<Transform>())
            {
                if
                    (
                    Obj.name.Equals(SAS_Name, StringComparison.Ordinal) ||
                    Obj.name.Equals(RCS_Name, StringComparison.Ordinal) ||
                    Obj.name.Equals("comm_on_1", StringComparison.Ordinal) ||
                    Obj.name.Equals("comm_on_2", StringComparison.Ordinal) ||
                    Obj.name.Equals("comm_on_3", StringComparison.Ordinal) ||
                    Obj.name.Equals(EngLedName_left, StringComparison.Ordinal) ||
                    Obj.name.Equals(EngLedName_right, StringComparison.Ordinal)
                    )
                {
                    Obj.gameObject.SetActive(false);
                }
            }
        }
        #endregion

        #region Start Format TMPro Text Fields and Some Flight initialization for NavBall, SAS, RCS, Altitude, Speed
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
            LCDTextMesh.text = "Omicron";
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
            LCDTextMeshR.text = "Omicron";
            #endregion

            #region Formating Display Altitude Unity Left
            GameObject LCDScreenAU = new GameObject();
            Transform screenTransformAU = part.FindModelTransform(ScreenNameAU);
            LCDScreenAU.transform.parent = screenTransformAU;
            LCDScreenAU.transform.localRotation = screenTransformAU.localRotation;
            LCDScreenAU.transform.localRotation = Quaternion.Euler(0, 0, 90);
            LCDTextMeshAU = LCDScreenAU.AddComponent<TextMeshPro>();
            MAU = screenTransformAU.GetComponent<MeshFilter>().mesh;
            TAU = LCDTextMeshAU.gameObject.GetComponent<RectTransform>();
            TAU.sizeDelta = new Vector2(MAU.bounds.size.y, MAU.bounds.size.x);
            LCDScreenAU.transform.localPosition = new Vector3(0, 0, (MAU.bounds.size.z / 2) + 0.01f);
            LCDTextMeshAU.fontSize = 0.16f;
            LCDTextMeshAU.enableAutoSizing = autoFont;
            LCDTextMeshAU.color = Color.white;
            LCDTextMeshAU.font = loadedFonts[1];
            LCDTextMeshAU.fontSizeMin = 0.01f;
            LCDTextMeshAU.overflowMode = TextOverflowModes.Truncate;
            LCDTextMeshAU.alignment = TextAlignmentOptions.Center;
            LCDTextMeshAU.text = "";
            #endregion

            #region Formating Display Altitude Unity Right
            GameObject LCDScreenAUR = new GameObject();
            Transform screenTransformAUR = part.FindModelTransform(ScreenNameAUR);
            LCDScreenAUR.transform.parent = screenTransformAUR;
            LCDScreenAUR.transform.localRotation = screenTransformAUR.localRotation;
            LCDScreenAUR.transform.localRotation = Quaternion.Euler(0, 0, 90);
            LCDTextMeshAUR = LCDScreenAUR.AddComponent<TextMeshPro>();
            MAUR = screenTransformAUR.GetComponent<MeshFilter>().mesh;
            TAUR = LCDTextMeshAUR.gameObject.GetComponent<RectTransform>();
            TAUR.sizeDelta = new Vector2(MAUR.bounds.size.y, MAUR.bounds.size.x);
            LCDScreenAUR.transform.localPosition = new Vector3(0, 0, (MAUR.bounds.size.z / 2) + 0.01f);
            LCDTextMeshAUR.fontSize = 0.16f;
            LCDTextMeshAUR.enableAutoSizing = autoFont;
            LCDTextMeshAUR.color = Color.white;
            LCDTextMeshAUR.font = loadedFonts[1];
            LCDTextMeshAUR.fontSizeMin = 0.01f;
            LCDTextMeshAUR.overflowMode = TextOverflowModes.Truncate;
            LCDTextMeshAUR.alignment = TextAlignmentOptions.Center;
            LCDTextMeshAUR.text = "";
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
            LCDTextMeshPe.fontSize = 0.13f;
            LCDTextMeshPe.enableAutoSizing = autoFont;
            LCDTextMeshPe.color = new Color32(200, 200, 255, 200);
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
            LCDTextMeshPeR.fontSize = 0.13f;
            LCDTextMeshPeR.enableAutoSizing = autoFont;
            LCDTextMeshPeR.color = new Color32(200, 200, 255, 200);
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
            LCDTextMeshAp.fontSize = 0.13f;
            LCDTextMeshAp.enableAutoSizing = autoFont;
            LCDTextMeshAp.color = new Color32(200, 250, 255, 200);
            LCDTextMeshAp.font = loadedFonts[1];
            LCDTextMeshAp.fontSizeMin = 0.01f;
            LCDTextMeshAp.overflowMode = TextOverflowModes.Truncate;
            LCDTextMeshAp.alignment = TextAlignmentOptions.Center;
            LCDTextMeshAp.text = "";
            #endregion

            #region Formating Display Apoapsis Right
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
            LCDTextMeshApR.fontSize = 0.13f;
            LCDTextMeshApR.enableAutoSizing = autoFont;
            LCDTextMeshApR.color = new Color32(200, 250, 255, 200);
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
            LCDTextMeshS.text = "hello!";
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
            LCDTextMeshSR.text = "hello!";
            #endregion

            #region Formating Display Speed Unity Left
            GameObject LCDScreenSp = new GameObject();
            Transform screenTransformSp = part.FindModelTransform(ScreenNameSp);
            LCDScreenSp.transform.parent = screenTransformSp;
            LCDScreenSp.transform.localRotation = screenTransformSp.localRotation;
            LCDScreenSp.transform.localRotation = Quaternion.Euler(0, 0, 90);
            LCDTextMeshSp = LCDScreenSp.AddComponent<TextMeshPro>();
            MSp = screenTransformSp.GetComponent<MeshFilter>().mesh;
            TSp = LCDTextMeshSp.gameObject.GetComponent<RectTransform>();
            TSp.sizeDelta = new Vector2(MSp.bounds.size.y, MSp.bounds.size.x);
            LCDScreenSp.transform.localPosition = new Vector3(0, 0, (MSp.bounds.size.z / 2) + 0.01f);
            LCDTextMeshSp.fontSize = 0.16f;
            LCDTextMeshSp.enableAutoSizing = autoFont;
            LCDTextMeshSp.color = Color.white;
            LCDTextMeshSp.font = loadedFonts[1];
            LCDTextMeshSp.fontSizeMin = 0.01f;
            LCDTextMeshSp.overflowMode = TextOverflowModes.Truncate;
            LCDTextMeshSp.alignment = TextAlignmentOptions.Center;
            LCDTextMeshSp.text = "";
            #endregion

            #region Formating Display Speed Unity Right
            GameObject LCDScreenSpR = new GameObject();
            Transform screenTransformSpR = part.FindModelTransform(ScreenNameSpR);
            LCDScreenSpR.transform.parent = screenTransformSpR;
            LCDScreenSpR.transform.localRotation = screenTransformSpR.localRotation;
            LCDScreenSpR.transform.localRotation = Quaternion.Euler(0, 0, 90);
            LCDTextMeshSpR = LCDScreenSpR.AddComponent<TextMeshPro>();
            MSpR = screenTransformSpR.GetComponent<MeshFilter>().mesh;
            TSpR = LCDTextMeshSpR.gameObject.GetComponent<RectTransform>();
            TSpR.sizeDelta = new Vector2(MSpR.bounds.size.y, MSpR.bounds.size.x);
            LCDScreenSpR.transform.localPosition = new Vector3(0, 0, (MSpR.bounds.size.z / 2) + 0.01f);
            LCDTextMeshSpR.fontSize = 0.16f;
            LCDTextMeshSpR.enableAutoSizing = autoFont;
            LCDTextMeshSpR.color = Color.white;
            LCDTextMeshSpR.font = loadedFonts[1];
            LCDTextMeshSpR.fontSizeMin = 0.01f;
            LCDTextMeshSpR.overflowMode = TextOverflowModes.Truncate;
            LCDTextMeshSpR.alignment = TextAlignmentOptions.Center;
            LCDTextMeshSpR.text = "";
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
            LCDTextMeshVS.fontSize = 0.13f;
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
            LCDTextMeshVSR.fontSize = 0.13f;
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
            LCDTextMeshHS.fontSize = 0.13f;
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
            LCDTextMeshHSR.fontSize = 0.13f;
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
            LCDTextMeshVtol.fontSize = 0.25f;
            LCDTextMeshVtol.enableAutoSizing = autoFont;
            LCDTextMeshVtol.color = new Color32(255, 255, 255, 255);
            LCDTextMeshVtol.font = loadedFonts[1];
            LCDTextMeshVtol.fontSizeMin = 0.01f;
            LCDTextMeshVtol.overflowMode = TextOverflowModes.Truncate;
            LCDTextMeshVtol.alignment = TextAlignmentOptions.Center;
            LCDTextMeshVtol.text = "Vtol";
            #endregion

            #region Formating Display Engine Status
            GameObject LCDScreenEng = new GameObject();
            Transform screenTransformEng = part.FindModelTransform(ScreenNameEng);
            LCDScreenEng.transform.parent = screenTransformEng;
            LCDScreenEng.transform.localRotation = screenTransformEng.localRotation;
            LCDScreenEng.transform.localRotation = Quaternion.Euler(0, 0, 90);
            LCDTextMeshEng = LCDScreenEng.AddComponent<TextMeshPro>();
            MEng = screenTransformEng.GetComponent<MeshFilter>().mesh;
            TEng = LCDTextMeshEng.gameObject.GetComponent<RectTransform>();
            TEng.sizeDelta = new Vector2(MEng.bounds.size.y * 5f, MEng.bounds.size.x * 5f);
            LCDScreenEng.transform.localPosition = new Vector3(0, 0, (MEng.bounds.size.z / 2) + 0.01f);
            LCDTextMeshEng.fontSize = 0.25f;
            LCDTextMeshEng.enableAutoSizing = autoFont;
            LCDTextMeshEng.color = new Color32(255, 0, 0, 255);
            LCDTextMeshEng.font = loadedFonts[1];
            LCDTextMeshEng.fontSizeMin = 0.01f;
            LCDTextMeshEng.overflowMode = TextOverflowModes.Truncate;
            LCDTextMeshEng.alignment = TextAlignmentOptions.Center;
            LCDTextMeshEng.text = Eng_info;
            #endregion

            #region Vtol Attitude Control (Menus, Actions, Editor and Flight)
            UI_FloatRange uiRange1 = (UI_FloatRange)VtolAngleAxis.uiControlEditor;
            UI_FloatRange uiRange2 = (UI_FloatRange)VtolAngleAxis.uiControlFlight;
            VtolAngleAxis.minValue = uiRange1.minValue = uiRange2.minValue = 0f;
            VtolAngleAxis.maxValue = uiRange1.maxValue = uiRange2.maxValue = 90f;
            uiRange1.stepIncrement = uiRange2.stepIncrement = 1f;
            VtolAngleAxis.incrementalSpeed = 20f;
            VtolAngleAxis.guiActiveEditor = true;
            VtolAngleAxis.guiActive = true;
            VtolAngleAxis.guiActiveUnfocused = false;
            VtolAngleAxis.guiInteractable = true;

            VtolTrans = part.transform.FindRecursive("attitude_node");
            #endregion

            #region Do only if outside Editor, On Flight
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
        #endregion

        #region FixedUpdate //Update
        public void FixedUpdate()  //Update()
        {
            #region Display Vtol Angle and control Vtol Attitude

            VtolAngle = VtolAngleE * 10;
            VtolAngleE = (float)(Math.Round(VtolAngle)) / 10;
            VtolTrans.localRotation = Quaternion.Euler((float)-VtolAngleE, 0f, 0f);
            string Vtl = VtolAngleE.ToString("#0.#");
            LCDTextMeshVtol.text = Vtl + "°";

            #endregion

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
            Throttle_L.localPosition = new Vector3(0, 0, (float)((Throttle * 0.008) + 0.029));
            Throttle_R.localPosition = new Vector3(0, 0, (float)((Throttle * 0.008) + 0.029));
            #endregion

            #region Speed
            //Speed Orbit or Surface
            if (vessel.SituationString == "SUB-ORBITAL" || vessel.SituationString == "ORBITING" || vessel.SituationString == "ESCAPING" || vessel.SituationString == "DOCKED")
            {
                Speed = vessel.obt_speed;
            }
            else
            {
                Speed = vessel.srfSpeed;
            }

            //Speed Dials
            Quaternion DialSpeed10 = Quaternion.Euler(0, (float)((Speed * 3.6) % 360), 0);
            Quaternion DialSpeed100 = Quaternion.Euler(0, (float)((Speed * 0.36) % 360), 0);
            Quaternion DialSpeed1000 = Quaternion.Euler(0, (float)((Speed * 0.036) % 360), 0);
            Speed_Dial_10_L.localRotation = DialSpeed10;
            Speed_Dial_100_L.localRotation = DialSpeed100;
            Speed_Dial_1000_L.localRotation = DialSpeed1000;
            Speed_Dial_10_R.localRotation = DialSpeed10;
            Speed_Dial_100_R.localRotation = DialSpeed100;
            Speed_Dial_1000_R.localRotation = DialSpeed1000;
            Speed_Dial_10_L.localPosition = new Vector3(0, (float)((0.02 * (Speed % 100) / 100) + 0.17), 0);
            Speed_Dial_100_L.localPosition = new Vector3(0, (float)((0.02 * (Speed % 1000) / 1000) + 0.165), 0);
            Speed_Dial_1000_L.localPosition = new Vector3(0, (float)((0.02 * (Speed % 100000) / 10000) + 0.16), 0);
            Speed_Dial_10_R.localPosition = new Vector3(0, (float)((0.02 * (Speed % 100) / 100) + 0.17), 0);
            Speed_Dial_100_R.localPosition = new Vector3(0, (float)((0.02 * (Speed % 1000) / 1000) + 0.165), 0);
            Speed_Dial_1000_R.localPosition = new Vector3(0, (float)((0.02 * (Speed % 10000) / 10000) + 0.16), 0);

            //Display Speed
            string Spd = Speed.ToString("0.0");
            LCDTextMeshS.text = Spd;
            LCDTextMeshSR.text = Spd;

            //Display Speed Unit
            LCDTextMeshSp.text = SpdUnity;
            LCDTextMeshSpR.text = SpdUnity;

            #endregion

            #region Condition to Show Vspeed and Hspeed

            if (vessel.SituationString == "FLYING" || vessel.SituationString == "SUB-ORBITAL")
            {
                Vspeed = vessel.verticalSpeed;
                string Vspd = Vspeed.ToString("0.0");
                LCDTextMeshVS.text = Vspd + "v";
                LCDTextMeshVSR.text = Vspd + "v";

                Hspeed = vessel.horizontalSrfSpeed;
                string Hspd = Hspeed.ToString("0.0");
                LCDTextMeshHS.text = Hspd + "h";
                LCDTextMeshHSR.text = Hspd + "h";
            }
            else
            {
                LCDTextMeshVS.text = "";
                LCDTextMeshVSR.text = "";
                LCDTextMeshHS.text = "";
                LCDTextMeshHSR.text = "";
            }
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

            //Calculate Ap e Pe
            Apoapsis = vessel.orbit.ApA;
            Periapsis = vessel.orbit.PeA;

            //Check altitude unity;
            altpointer = 1;
            while (Altitude < 100 & altpointer > 1)
            {
                altpointer -= 1;
                Altitude *= 1000;
                Apoapsis *= 1000;
                Periapsis *= 1000;
            }

            while (Altitude > 100000 & altpointer < 5)
            {
                altpointer += 1;
                Altitude /= 1000;
                Apoapsis /= 1000;
                Periapsis /= 1000;
            }

            //Change Unit labels if needed
            if (altpointer != altpointerold)
            {
                switch (altpointer)
                {
                    case 1:          //int n when (n >= 1 && n <= 3):
                        AltUnity = "meters";
                        break;
                    case 2:          //int n when (n >= 4 && n <= 6):
                        AltUnity = "Km";
                        break;
                    case 3:          //int n when (n >= 7 && n <= 9):
                        AltUnity = "Mm";
                        break;
                    case 4:          //int n when (n >= 10 && n <= 12):
                        AltUnity = "Bm";
                        break;
                    case 5:          //int n when (n >= 13 && n <= 15):
                        AltUnity = "Tm";
                        break;
                    default:
                        AltUnity = "meters";
                        break;
                }
            }

            //Altitude Dials
            Quaternion DialAlt10 = Quaternion.Euler(0, (float)((Altitude * 0.36) % 360), 0);
            Quaternion DialAlt100 = Quaternion.Euler(0, (float)((Altitude * 0.036) % 360), 0);
            Quaternion DialAlt1000 = Quaternion.Euler(0, (float)((Altitude * 0.0036) % 360), 0);
            Alt_Dial_10_L.localRotation = DialAlt10;
            Alt_Dial_100_L.localRotation = DialAlt100;
            Alt_Dial_1000_L.localRotation = DialAlt1000;
            Alt_Dial_10_R.localRotation = DialAlt10;
            Alt_Dial_100_R.localRotation = DialAlt100;
            Alt_Dial_1000_R.localRotation = DialAlt1000;
            Alt_Dial_10_L.localPosition = new Vector3(0, (float)((0.02 * (Altitude % 1000) / 1000) + 0.17), 0);
            Alt_Dial_100_L.localPosition = new Vector3(0, (float)((0.02 * (Altitude % 10000) / 10000) + 0.165), 0);
            Alt_Dial_1000_L.localPosition = new Vector3(0, (float)((0.02 * (Altitude % 100000) / 100000) + 0.16), 0);
            Alt_Dial_10_R.localPosition = new Vector3(0, (float)((0.02 * (Altitude % 1000) / 1000) + 0.17), 0);
            Alt_Dial_100_R.localPosition = new Vector3(0, (float)((0.02 * (Altitude % 10000) / 10000) + 0.16), 0);
            Alt_Dial_1000_R.localPosition = new Vector3(0, (float)((0.02 * (Altitude % 100000) / 100000) + 0.155), 0);

            //Display Altitude
            string Alt = Altitude.ToString("0.00");
            LCDTextMesh.text = Alt;
            LCDTextMeshR.text = Alt;

            //Display para Unidade Dinamica de Altitude
            LCDTextMeshAU.text = AltUnity;
            LCDTextMeshAUR.text = AltUnity;

            //Leverage pointers
            altpointerold = altpointer;
            #endregion

            #region Condition to Show Apoapsis

            if (vessel.SituationString == "FLYING" || vessel.SituationString == "SUB-ORBITAL" || vessel.SituationString == "ORBITING" || vessel.SituationString == "DOCKED")
            {
                string Apo = Apoapsis.ToString("0.00");
                LCDTextMeshAp.text = "Ap" + Apo;
                LCDTextMeshApR.text = "Ap" + Apo;
            }
            else if (vessel.SituationString == "ESCAPING")
            {
                LCDTextMeshAp.text = "escape";
                LCDTextMeshApR.text = "escape";
            }
            else
            {
                LCDTextMeshAp.text = "";
                LCDTextMeshApR.text = "";
            }
            #endregion

            #region Condition to Show Periapsis

            if (vessel.SituationString == "ESCAPING" || vessel.SituationString == "SUB-ORBITAL" || vessel.SituationString == "ORBITING" || vessel.SituationString == "DOCKED")
            {
                string Per = Periapsis.ToString("0.00");
                LCDTextMeshPe.text = "Pe" + Per;
                LCDTextMeshPeR.text = "Pe" + Per;
            }
            else if (vessel.SituationString == "FLYING")
            {
                LCDTextMeshPe.text = "Atmosphere";
                LCDTextMeshPeR.text = "Atmosphere";
            }
            else
            {
                LCDTextMeshPe.text = "";
                LCDTextMeshPeR.text = "";
            }
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

            #region Engine Status
            if (EngineStatus == "On")
            {
                EnginePointer = false;
                ScreenTime -= Time.deltaTime;
                if (ScreenTime < 0)
                {
                    if (EngineMode == "Air Breathing")
                    {
                        Eng_info = "air breathing";
                    }
                    else
                    {
                        Eng_info = "rocket";
                    }
                }
                else
                {
                    Eng_info = "engine on";
                }
            }
            else if (!EnginePointer)
            {
                ScreenTime = 3.0f;
                Eng_info = "engine off";
                EnginePointer = true;
            }
            else
            {
                ScreenTime -= Time.deltaTime;
                if (ScreenTime < 0)
                {
                    Eng_info = "";
                    ScreenTime = 3.0f;
                }
            }
            LCDTextMeshEng.text = Eng_info;
            print("TEng size: " + TEng.sizeDelta + " / " + Eng_info);
            #endregion
        }
        #endregion
    }
}