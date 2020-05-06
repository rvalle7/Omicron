using System;
using UnityEngine;

namespace Omicron
{
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
