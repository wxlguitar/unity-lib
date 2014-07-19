using UnityEngine;
using System.Collections;

public class XUtility
{

    static public string GetAssetPath(string strFilePath)
    {
        return strFilePath.Substring(strFilePath.IndexOf("Assets"));
    }

    static public string GetAssetDirPath(string strFilePath)
    {
        string strPath = strFilePath.Substring(strFilePath.IndexOf("Assets"));
        strPath = strPath.Substring(0, strPath.LastIndexOf("\\")) + "/";    // 创建预设的路径，目录最后一个分隔符不能为"\"，否则创建时会报错
        return strPath;
    }
}
