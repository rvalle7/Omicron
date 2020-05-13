using System;
using UnityEngine;
using System.Linq;

namespace Omicron
{
    [KSPModule("ModuleOmicronVtol")]
    public class ModuleOmicronVtol : PartModule
    {
        #region Var

        //Vtol Engine
        private Transform VtolEngTrans;
        private string VtolEngnodeName = "vtol_control";
        private bool invertset = false;
        private bool invertmotion = false;
        private double AngleVtol;

        //Mirror Parts
        public Transform leftObject;
        public Transform rightObject;
        public string right = "right";
        public string left = "left";
        public string swap = "swap";
        [KSPField(isPersistant = true)]
        public string cloneSide;
        [KSPField(isPersistant = true)]
        public string flightSide;
        public ModuleOmicronVtol clone;
        #endregion

        #region Events Mirror
        [KSPEvent(guiName = "Left", guiActive = false, guiActiveEditor = true)]
        public void LeftSide()
        {
            FindClone();
            SetSide(left);

            if (clone)
            {
                clone.SetSide(right);
            }
        }
        [KSPEvent(guiName = "Right", guiActive = false, guiActiveEditor = true)]
        public void RightSide()
        {
            FindClone();
            SetSide(right);

            if (clone)
            {
                clone.SetSide(left);
            }
        }
        #endregion

        #region Fields Vtol

        [
            KSPAxisField
            (
                guiName = "Vtol_Engine_angle",
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
        public float VtolEngAngleE = 90f;

        private BaseAxisField VtolEngAngleAxis
        {
            get
            {
                return (BaseAxisField)Fields["VtolEngAngleE"];
            }
        }

        #endregion

        #region Class SetSide Mirror
        public void SetSide(string side)
        {
            if (side == left)
            {
                rightObject.gameObject.SetActive(false);
                leftObject.gameObject.SetActive(true);
                cloneSide = right;
                flightSide = side;
                Events["LeftSide"].active = false;
                Events["RightSide"].active = true;
            }
            if (side == right)
            {
                rightObject.gameObject.SetActive(true);
                leftObject.gameObject.SetActive(false);
                cloneSide = left;
                flightSide = side;
                Events["LeftSide"].active = true;
                Events["RightSide"].active = false;
            }

        }
        #endregion

        #region Class FindClone Mirror
        public void FindClone()
        {
            foreach (Part potentialMaster in this.part.symmetryCounterparts)
            {
                if (potentialMaster != null)
                {
                    clone = potentialMaster.Modules.OfType<ModuleOmicronVtol>().FirstOrDefault();
                }
            }
        }
        #endregion

        #region OnStart Mirror
        public override void OnStart(PartModule.StartState state)
        {
            base.OnStart(state);

            foreach (Transform tr in part.GetComponentsInChildren<Transform>())
            {
                if (tr.name.Equals("Left", StringComparison.Ordinal))
                {
                    leftObject = tr;
                }

                if (tr.name.Equals("Right", StringComparison.Ordinal))
                {
                    rightObject = tr;
                }
            }

            FindClone();
            if (clone != null)
            {
                SetSide(clone.cloneSide);
            }

            if (flightSide == "")
            {
                RightSide();
            }
            else
            {
                SetSide(flightSide);
            }

            if (HighLogic.LoadedSceneIsFlight)
            {
                SetSide(flightSide);
            }
        }
        #endregion

        #region Start Vtol Attitude Control (Menus, Actions, Editor and Flight)
        public void Start()
        {
            UI_FloatRange uiRange1 = (UI_FloatRange)VtolEngAngleAxis.uiControlEditor;
            UI_FloatRange uiRange2 = (UI_FloatRange)VtolEngAngleAxis.uiControlFlight;
            VtolEngAngleAxis.minValue = uiRange1.minValue = uiRange2.minValue = 0f;
            VtolEngAngleAxis.maxValue = uiRange1.maxValue = uiRange2.maxValue = 90f;
            uiRange1.stepIncrement = uiRange2.stepIncrement = 1f;
            VtolEngAngleAxis.incrementalSpeed = 20f;
            VtolEngAngleAxis.guiActiveEditor = true;
            VtolEngAngleAxis.guiActive = true;
            VtolEngAngleAxis.guiActiveUnfocused = false;
            VtolEngAngleAxis.guiInteractable = true;

            VtolEngTrans = part.transform.FindRecursive(VtolEngnodeName);
        }
        #endregion

        #region Update, Vtol Engine Attitude Control
        public void Update()
        {
            AngleVtol = VtolEngAngleE * 10;
            VtolEngAngleE = (float)(Math.Round(AngleVtol))/10;
            #region in Editor
            if (HighLogic.LoadedSceneIsEditor)
            {
                if (cloneSide == left)
                {
                    VtolEngTrans.localRotation = Quaternion.Euler((float) -VtolEngAngleE, 0f, 0f);
                }
                else
                {
                    VtolEngTrans.localRotation = Quaternion.Euler((float)VtolEngAngleE, 0f, 0f);
                }
            }
            #endregion

            #region in Flight
            if (HighLogic.LoadedSceneIsFlight)
            {
                AngleVtol = VtolEngAngleE;
                AngleVtol = Math.Round(AngleVtol);
                if (!invertset)
                {
                    if (Vector3.Dot(VtolEngTrans.position.normalized, vessel.ReferenceTransform.right) < 0)
                    {
                        invertmotion = true;
                    }
                    invertset = true;
                }

                if (invertmotion)
                {
                    VtolEngTrans.localRotation = Quaternion.Euler((float) -VtolEngAngleE, 0f, 0f);
                }
                else
                {
                    VtolEngTrans.localRotation = Quaternion.Euler((float)VtolEngAngleE, 0f, 0f);
                }
            }
            #endregion
        }
        #endregion
    }
}