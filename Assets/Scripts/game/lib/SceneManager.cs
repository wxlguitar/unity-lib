using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class SceneManager 
{
    static private SceneManager m_instance;

    private int m_iCurSceneID = -1;
    private MapData m_kCurMapData = null;

    // Scene Object
    private List<UnityEngine.Object> m_listSceneObj;
    private string m_strLightMapPath;
    private string m_strLightProbePath;
    //private string m_strSceneFileName;


    public SceneManager()
    {
        if(m_instance != null)
        {
            throw new Exception("SceneManager is a singleton");
        }

        m_instance = this;
        m_listSceneObj = new List<UnityEngine.Object>();
    }

    static public SceneManager instance
    {
        get { return m_instance; }
    }

    public void LoadScene(int iSceneID, Action<Boolean> funSceneLoaded, Action<int> funLoadPercent = null, bool bShowLoadingUI = true)
    {
        if(bShowLoadingUI)
        {
            //
        }

        if(m_iCurSceneID == iSceneID)
        {
            if (funSceneLoaded != null)
                funSceneLoaded(true);
            return;
        }

        MapData kMapData = null;
        if (!MapData.dataDic.TryGetValue(iSceneID, out kMapData))
        {
            return;
        }

        UnLoadScene();

        m_iCurSceneID = iSceneID;
        m_kCurMapData = kMapData;

        LoadScene();
    }

    private void UnLoadScene()
    {
        if (m_kCurMapData == null)
            return;

        // 卸载场景中的模型
        foreach(UnityEngine.Object obj in m_listSceneObj)
        {
            AssetManager.instance.ReleaseInstance(obj, true);
        }
        m_listSceneObj.Clear();

        // LightMap卸载，www.assetBundle.Unload(true);
        AssetManager.instance.ReleaseAsset(m_strLightMapPath, true);
        m_strLightMapPath = string.Empty;

        // LightProbe卸载
        AssetManager.instance.ReleaseAsset(m_strLightProbePath, true);
        m_strLightProbePath = string.Empty;

        // 场景文件卸载
        string strSceneFileName = string.Concat(m_kCurMapData.sceneName, ".unity");
        AssetManager.instance.ReleaseAsset(strSceneFileName, true);
    }

    private void LoadScene()
    {
        Action<UnityEngine.Object> funSceneFileLoaded = (obj)=>
        {

        };

        string strSceneFileName = string.Concat(m_kCurMapData.sceneName, ".unity");
        AssetManager.instance.GetAssetAndUnloadAB(strSceneFileName, funSceneFileLoaded);
    }
}
