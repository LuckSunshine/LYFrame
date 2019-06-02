using System;
using System.Collections;
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;

namespace LYFrameWork
{
    public class Exemp1 : MonoBehaviour
    {
#if UNITY_EDITOR
        [MenuItem("LY/Build Package")]
        private static void SetPackageName()
        {
            string packageName = "LYFrameWork_" + DateTime.Now.ToString("yyyyMMdd_HH")+".unitypackage";
            //GUIUtility.systemCopyBuffer = packageName;
            Debug.Log(packageName);
            AssetDatabase.ExportPackage("Assets/LYFrameWork",packageName,ExportPackageOptions.Recurse);
            Application.OpenURL("file:///"+Application.dataPath);
        }
    }
#endif
}

