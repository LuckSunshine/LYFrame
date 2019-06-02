using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
public class Script_03_27window : EditorWindow
{
    private GameObject m_MyGo;
    private Editor     m_MyEditor;

    [MenuItem("Window/TAOSHUI Open My Window")]
    static void Init()
    {
        Script_03_27window window = (Script_03_27window)EditorWindow.GetWindow(typeof(Script_03_27window));
        window.Show();
    }
    void OnGUI()
    {
        //设置一个游戏对象
        m_MyGo = (GameObject)EditorGUILayout.ObjectField(m_MyGo,
            typeof(GameObject), true);
        if (m_MyGo != null)
        {
            if (m_MyEditor == null)
            {
                //创建Editor 实例
                m_MyEditor = Editor.CreateEditor(m_MyGo);
            }
            //预览它
            m_MyEditor.OnPreviewGUI(GUILayoutUtility.GetRect(200, 200),
                EditorStyles.whiteLabel);
        }
    }
}
