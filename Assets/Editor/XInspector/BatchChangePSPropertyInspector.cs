using UnityEngine;
using System.Collections;
using UnityEditor;

/// <summary>
/// 批量修改ParticleSystem属性的脚本
/// 用法：将BatchChangePSProxy脚本挂在目标GameObject下即可
/// </summary>
[CustomEditor(typeof(BatchChangePSProxy))]
public class BatchChangePSPropertyInspector : Editor
{
    private float fPSDelayOffset = 0f;
    
    public override void OnInspectorGUI()
    {
        GUILayout.BeginVertical();

        fPSDelayOffset = EditorGUILayout.FloatField("PS Delay Offset", fPSDelayOffset);
        fPSDelayOffset = Mathf.FloorToInt(fPSDelayOffset * 100) * 0.01f;
        if (GUILayout.Button("开始批处理"))
        {
            ChangeProperty();
        }
        GUILayout.BeginVertical();
    }

    private void ChangeProperty()
    {
        BatchChangePSProxy proxy = target as BatchChangePSProxy;
        GameObject kGO = proxy.gameObject;

        ParticleSystem[] arrPS = kGO.GetComponentsInChildren<ParticleSystem>();
        float fTemp = 0f;
        foreach(ParticleSystem ps in arrPS)
        {
            fTemp = ps.startDelay + fPSDelayOffset;
            fTemp = Mathf.FloorToInt(fTemp * 100) * 0.01f;
            fTemp = fTemp < 0 ? 0 : fTemp;
            ps.startDelay = fTemp;          
        }
    }
}
