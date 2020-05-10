
using TMPro;
using UnityEngine;

namespace Omicron
{
    class ModuleDisplay : PartModule
    {
        #region Var
        private double Altitude;
        private TextMeshPro LCDTextMesh = null;
        private TextMeshPro LCDTextMeshR = null;
        private string ScreenName = "tmp_altitude_left";
        private string ScreenNameR = "tmp_altitude_right";
        private TMP_FontAsset[] loadedFonts;
        readonly bool autoFont = false;
        private Mesh M;
        private Mesh MR;
        private RectTransform T;
        private RectTransform TR;
        #endregion

        #region OnCopy
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
        public override void OnAwake()
        {
            loadedFonts = Resources.FindObjectsOfTypeAll<TMP_FontAsset>();
        }
        #endregion

        #region Start
        public void Start()
        {
            GameObject LCDScreen = new GameObject();
            Transform screenTransform = part.FindModelTransform(ScreenName);
            LCDScreen.transform.parent = screenTransform;
            LCDScreen.transform.localRotation = screenTransform.localRotation;
            LCDScreen.transform.localRotation = Quaternion.Euler(0, 0, 90);
            LCDTextMesh = LCDScreen.AddComponent<TextMeshPro>();
            M = screenTransform.GetComponent<MeshFilter>().mesh;
            T = LCDTextMesh.gameObject.GetComponent<RectTransform>();
            T.sizeDelta = new Vector2(M.bounds.size.y/7, M.bounds.size.x/7);
            LCDScreen.transform.localPosition = new Vector3(0, 0, (M.bounds.size.z / 2) + 0.01f);

            GameObject LCDScreenR = new GameObject();
            Transform screenTransformR = part.FindModelTransform(ScreenNameR);
            LCDScreenR.transform.parent = screenTransformR;
            LCDScreenR.transform.localRotation = screenTransformR.localRotation;
            LCDScreenR.transform.localRotation = Quaternion.Euler(0, 0, 90);
            LCDTextMeshR = LCDScreenR.AddComponent<TextMeshPro>();
            MR = screenTransformR.GetComponent<MeshFilter>().mesh;
            TR = LCDTextMeshR.gameObject.GetComponent<RectTransform>();
            TR.sizeDelta = new Vector2(MR.bounds.size.y / 7, MR.bounds.size.x / 7);
            LCDScreenR.transform.localPosition = new Vector3(0, 0, (MR.bounds.size.z / 2) + 0.01f);


            LCDTextMesh.fontSize = 0.22f;
            LCDTextMesh.enableAutoSizing = autoFont;
            LCDTextMesh.color = Color.white;
            LCDTextMesh.font = loadedFonts[1];
            LCDTextMesh.fontSizeMin = 0.01f;
            LCDTextMesh.overflowMode = TextOverflowModes.Truncate;
            LCDTextMesh.alignment = TextAlignmentOptions.Center;
            LCDTextMesh.text = "0";


            LCDTextMeshR.fontSize = 0.22f;
            LCDTextMeshR.enableAutoSizing = autoFont;
            LCDTextMeshR.color = Color.white;
            LCDTextMeshR.font = loadedFonts[1];
            LCDTextMeshR.fontSizeMin = 0.01f;
            LCDTextMeshR.overflowMode = TextOverflowModes.Truncate;
            LCDTextMeshR.alignment = TextAlignmentOptions.Center;
            LCDTextMeshR.text = "0";
        }
        #endregion

        #region Update
        public void Update()
        {
            if (HighLogic.LoadedSceneIsEditor)
            {
                return;
            }

            if (vessel.altimeterDisplayState == AltimeterDisplayState.AGL)
            {
                Altitude = vessel.radarAltitude;
            }
            else
            {
                Altitude = vessel.altitude;
            }

            while (Altitude < 0)
            {
                Altitude *= 1000;
            }

            while (Altitude > 1000000)
            {
                Altitude /= 1000;
            }

            string Alt = Altitude.ToString("0.0");  //G6 (6 digitos, entre decimais e fracionais)  //D6  (6 digitos decimais)  //("0.#")
            LCDTextMesh.text = Alt;
            LCDTextMeshR.text = Alt;
        }
        #endregion
    }
}