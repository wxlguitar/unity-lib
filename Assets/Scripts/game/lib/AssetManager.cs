using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using Object = UnityEngine.Object;


public class AssetManager 
{
    static private AssetManager m_kInstance;

    private ILoadAsset m_kLoadMgr;

    private Dictionary<int, string> m_dicGOName = new Dictionary<int, string>();    //  记录GameObject对应的文件名，卸载实例(ReleaseInstance)的时候，要通过文件名去Unload对应的AssetBundle
    private Dictionary<int, string> m_dicAssetName = new Dictionary<int, string>(); // 记录AssetObject对应的文件名，卸载Asset时，通过文件名去Unload对应的AssetBundle


    public AssetManager()
    {
        if(m_kInstance != null)
        {
            throw new Exception("AssetCacheManager in a singleton");
        }

        m_kInstance = this;
    }

    static public AssetManager instance
    {
        get 
        {
            if (m_kInstance == null)
                new AssetManager();
            return m_kInstance; 
        }
    }

    public ILoadAsset loadMgr
    {
        get{return m_kLoadMgr;}
        set{m_kLoadMgr = value;}
    }

#region 加载

    //public void GetInstance(string strFileName, Action<string, Object> funLoaded)
    //{
    //    Action<string, Object> funInstanceRet = (strName, obj) =>
    //    {
    //        if (obj != null)
    //        {
    //            int iInstanceID = obj.GetInstanceID();
    //            m_dicGOName.Add(iInstanceID, strName);
    //        }

    //        if (funLoaded != null)
    //            funLoaded(strName, obj);
    //    };
    //    m_kLoadMgr.GetInstance(strFileName, funInstanceRet);
    //}

    //public void GetInstanceArray(string[] arrFileName, Action<Object[]> funLoaded)
    //{
    //    if (arrFileName == null || arrFileName.Length == 0)
    //    {
    //        if (funLoaded != null)
    //            funLoaded(null);
    //        return;
    //    }

    //    int iIndex = 0;
    //    int iCount = arrFileName.Length;
    //    Object[] arrObj = new Object[arrFileName.Length];

    //    Action<string, Object> funSingleLoaded = (name, obj) =>
    //    {
    //        arrObj[iIndex++] = obj;

    //        if (iIndex == iCount && funLoaded != null)
    //            funLoaded(arrObj);
    //    };

    //    foreach (string strName in arrFileName)
    //    {
    //        GetInstance(strName, funSingleLoaded);
    //    }
    //}

    public void GetInstanceAndUnloadAB(string strFileName, Action<string, Object> funLoaded)
    {
        Action<string, Object> funInstanceRet = (strName, obj) =>
        {
            if (obj == null)
            {
                if (funLoaded != null)
                    funLoaded(strName, null);
                return;
            }

            UnloadAssetBundle(strName, false);

            if (funLoaded != null)
                funLoaded(strName, obj);
        };
        LoadAsset(strFileName, funInstanceRet);
    }

    public void GetInstancesAndUnloadAB(string[] arrFileName, Action<Object[]> funLoaded)
    {
        if (arrFileName == null || arrFileName.Length == 0)
        {
            if (funLoaded != null)
                funLoaded(null);
            return;
        }

        int iIndex = 0;
        int iCount = arrFileName.Length;
        Object[] arrObj = new Object[arrFileName.Length];

        Action<string, Object> funSingleLoaded = (name, obj) =>
        {
            arrObj[iIndex++] = obj;
            if (iIndex == iCount && funLoaded != null)
                funLoaded(arrObj);
        };

        foreach (string strName in arrFileName)
        {
            GetInstanceAndUnloadAB(strName, funSingleLoaded);
        }
    }

    //public void GetAsset(string strFileName, Action<Object> funLoaded = null, Action<float> funLoadPercent = null, int iPriority = 0)
    //{
    //    Action<string, Object> funAssetLoaded = (strName, objAsset) =>
    //    {
    //        if (objAsset == null)
    //        {
    //            if (funLoaded != null)
    //                funLoaded(null);
    //            return;
    //        }

    //        int iInstanceID = objAsset.GetInstanceID();
    //        if (m_dicAssetName.ContainsKey(iInstanceID) == false)
    //        {
    //            m_dicAssetName.Add(iInstanceID, strName);
    //        }

    //        if (funLoaded != null)
    //            funLoaded(objAsset);
    //    };

    //    m_kLoadMgr.GetAsset(strFileName, funAssetLoaded);
    //}

    //public void GetAssets(string[] arrFileName, Action<Object[]> funLoaded = null, Action<float> funLoadPercent = null, int iPriority = 0)
    //{
    //    if (arrFileName == null || arrFileName.Length == 0)
    //        return;

    //    int iIndex = 0;
    //    int iCount = arrFileName.Length;
    //    Object[] arrObj = new Object[iCount];

    //    Action<Object> funAssetLoaded = (obj) =>
    //    {
    //        arrObj[iIndex++] = obj;
    //        if (iIndex == iCount && funLoaded != null)
    //            funLoaded(arrObj);
    //    };

    //    foreach( string str in arrFileName)
    //    {
    //        GetAsset(str, funAssetLoaded);
    //    }
    //}

    public void GetAssetAndUnloadAB(string strFileName, Action<Object> funLoaded, Action<float> funLoadPercent = null, int iPriority = 0)
    {
        Action<string, Object> funAssetLoaded = (strName, objAsset) =>
        {
            if (objAsset == null)
            {
                if (funLoaded != null)
                    funLoaded(null);
                return;
            }

            UnloadAssetBundle(strName, false);

            if (funLoaded != null)
                funLoaded(objAsset);
        };

        LoadAsset(strFileName, funAssetLoaded);
    }

    public void GetAssetsAndUnloadAB(string[] arrFileName, Action<Object[]> funLoaded, Action<float> funLoadPercent = null, int iPriority = 0)
    {
        if (arrFileName == null || arrFileName.Length == 0)
            return;

        int iIndex = 0;
        int iCount = arrFileName.Length;
        Object[] arrObj = new Object[iCount];

        Action<Object> funAssetLoaded = (obj) =>
        {
            arrObj[iIndex++] = obj;
            if (iIndex == iCount && funLoaded != null)
                funLoaded(arrObj);
        };

        foreach (string str in arrFileName)
        {
            GetAssetAndUnloadAB(str, funAssetLoaded);
        }
    }

    public Object SynGetInstance(string strFileName)
    {
        return m_kLoadMgr.SynGetInstance(strFileName);
    }

    public Object SynGetAsset(string strFileName)
    {
        return m_kLoadMgr.SynGetAsset(strFileName);
    }
#endregion

#region 卸载

    public void DestroyInstance(Object go)
    {
        if (go == null)
            return;

        GameObject.Destroy(go);
    }

    public void ReleaseInstance(Object go, bool bTryUnloadAB = true)
    {
        if (go == null)
            return;

        int iInstanceID = go.GetInstanceID();
        GameObject.Destroy(go);

        string strAssetName;
        if (bTryUnloadAB && m_dicGOName.TryGetValue(iInstanceID, out strAssetName))
        {
            m_dicGOName.Remove(iInstanceID);
            UnloadAssetBundle(strAssetName, false);
        }
    }

    /// <summary>
    /// 用于卸载非实例化的资源，或者说是被引用的资源(LightMap，Material，Texture等等)
    /// 资源引用数为0时：AssetBundle.Unload(false);
    /// </summary>
    public void ReleaseAsset(string strFileName, bool bTryUnloadAB = true)
    {
        if(string.IsNullOrEmpty(strFileName))
        {
            Debug.LogWarning("");
            return;
        }

        if(bTryUnloadAB)
        {
            m_kLoadMgr.UnloadAssetBundle(strFileName, false);
        }
    }
   
    public void ReleaseAsset(Object obj, bool bTryUnloadAB = true)
    {
        if (obj == null)
            return;

        string strAssetName;
        int iInstanceID = obj.GetInstanceID();
        
        if (bTryUnloadAB && m_dicAssetName.TryGetValue(iInstanceID, out strAssetName))
        {
            m_dicAssetName.Remove(iInstanceID);
            ReleaseAsset(strAssetName, bTryUnloadAB);
        }
    }

    /// <summary>
    /// 强制卸载资源/实例，不管引用数是否为0：AssetBundle.Unload(true);
    /// </summary>
    public void ForceReleaseResource(string strFileName)
    {
        if (string.IsNullOrEmpty(strFileName))
        {
            Debug.LogWarning("");
            return;
        }

        UnloadAssetBundle(strFileName, true);
    }

    /// <summary>
    /// 强制卸载资源/实例，不管引用数是否为0：AssetBundle.Unload(true);
    /// </summary>
    public void ForceReleaseResource(Object obj)
    {
        if (obj == null)
        {
            Debug.LogWarning("");
            return;
        }
        
        string strName;
        int iInstanceID = obj.GetInstanceID();

        if(m_dicAssetName.TryGetValue(iInstanceID, out strName) == true)
        {
            m_dicAssetName.Remove(iInstanceID);
        }
        else if(m_dicGOName.TryGetValue(iInstanceID, out strName) == true)
        {
            GameObject.Destroy(obj);
            m_dicGOName.Remove(iInstanceID);
        }
        ForceReleaseResource(strName);
    }

    /// <summary>
    /// 强制卸载资源/实例，不管引用数是否为0：AssetBundle.Unload(true);
    /// </summary>
    public void ForceReleaseResources(string[] arrFileName)
    {
        if (arrFileName == null || arrFileName.Length == 0)
            return;

        foreach(string strName in arrFileName)
        {
            ForceReleaseResource(strName);
        }
    }
#endregion    

#region 内部方法

    private void LoadAsset(string strFileName, Action<string, Object> funLoaded, int iPriority = 0, Action<float> funLoadPercent = null)
    {

    }

    private void UnloadAssetBundle(string strFileName, bool bUnloadAllLoadedObject = false)
    {
        m_kLoadMgr.UnloadAssetBundle(strFileName, bUnloadAllLoadedObject);
    }
#endregion


}
