using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using System.Reflection;


[CustomEditor(typeof(CopyComponentsProxy))]
public class CopyComponentsInspector : Editor
{
    Object goTarget;

    public override void OnInspectorGUI()
    {
        GUILayout.BeginVertical();

        GUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("目标GO", GUILayout.MaxWidth(50));
        goTarget = EditorGUILayout.ObjectField(goTarget, typeof(GameObject), true);
        GUILayout.EndHorizontal();

        EditorGUILayout.Separator();

        if (GUILayout.Button("开始克隆Component"))
        {
            if (goTarget == null)
            {
                EditorUtility.DisplayDialog("Error: 没有设置目标对象", "", "OK");
                return;
            }
            CopyComponentsProxy proxy = target as CopyComponentsProxy;

            CloneComponent(proxy.gameObject, goTarget as GameObject);
        }
        GUILayout.BeginVertical();
    }

    private void CloneComponent(GameObject goSrc, GameObject goDest)
    {
        int iNum = 0;

        System.Type typeCom = typeof(Component);
        if (typeCom == null)
            return;

        Component[] arrComponent = goSrc.GetComponents(typeCom);
        foreach (Component kC in arrComponent)
        {
            if (kC is CopyComponentsProxy || kC is Transform || kC is Renderer || kC is Collider || kC is Rigidbody || kC is Animation || kC is Animator)
                continue;

            CopyData(kC, goDest.AddComponent(kC.GetType()));
            iNum++;
        }

        if (iNum != 0)
            EditorUtility.DisplayDialog("组件克隆成功,共 " + iNum + " 个", "", "OK");
    }

    private void CopyData(object objSrc, object objDest)
    {
        System.Type type = objSrc.GetType();

        FieldInfo[] arrFI = type.GetFields(BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);

        foreach (FieldInfo kFI in arrFI)
        {
            kFI.SetValue(objDest, kFI.GetValue(objSrc));
        }
    }
}

