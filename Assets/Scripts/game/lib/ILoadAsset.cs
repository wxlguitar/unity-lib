using UnityEngine;
using System.Collections;
using System;
using Object = UnityEngine.Object;


public interface ILoadAsset 
{

    void GetInstance(string strFileName, Action<string, Object> funLoaded);

    void GetAsset(string strFileName, Action<string, Object> funLoaded);

    Object SynGetInstance(string strFileName);

    Object SynGetAsset(string strFileName);

    void UnloadAssetBundle(string strFileName, bool bUnloadAllLoadedObjects = false);
}
