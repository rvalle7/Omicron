using System;
using System.Linq;
using UnityEngine;

// KSP 1.9.1 e Unity 2019.2.2f1

namespace Omicron
{
    [KSPModule("ModuleMirror")]
    public class ModuleMirror : PartModule
    {
        #region Var
        public Transform leftObject;
        public Transform rightObject;
        public string right = "right";
        public string left = "left";
        public string swap = "swap";
        [KSPField(isPersistant = true)]
        public string cloneSide;
        [KSPField(isPersistant = true)]
        public string flightSide;

        public ModuleMirror clone;
        #endregion

        #region OnStart
        public override void OnStart(PartModule.StartState state)
        {
            base.OnStart(state);

            foreach (Transform tr in this.part.GetComponentsInChildren<Transform>())
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

            if (HighLogic.LoadedSceneIsFlight)
            {
                SetSide(flightSide);
            }

            FindClone();
            if (clone != null)
            {
                 SetSide(clone.cloneSide);
            }

            if (flightSide == "")
            {
                 LeftSide();
            }
            else
            {
                //print("Setting value from persistence");
                SetSide(flightSide);
            }
        }
        #endregion

        #region Events
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

        #region Class SetSide
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

        #region Class FindClone
        public void FindClone()
        {
            foreach (Part potentialMaster in this.part.symmetryCounterparts)
            {
                if (potentialMaster != null)
                {
                    clone = potentialMaster.Modules.OfType<ModuleMirror>().FirstOrDefault();
                }
            }
        }
        #endregion
    }
}
