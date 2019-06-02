using System.Collections;
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;

namespace LYFrameWork
{
    public class ReuseMenuItem : MonoBehaviour
    {
#if UNITY_EDITOR
        [MenuItem("LY/复用Item %e")]
#endif
        private static void RMenuItem()
        {
            EditorApplication.ExecuteMenuItem("LY/Build Package");
        }

    }

}

