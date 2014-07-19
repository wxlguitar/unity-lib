using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System.IO; 
using System;
using UnityEditorInternal;


public class ExtractAniClip
{
    /// <summary>
    /// 在工程视图选中包含有Fbx的文件夹（支持子文夹），然后执行此命令。
    /// 要求：
    ///     1. 带动画的Fbx的命名为,英雄名字@动作名
    ///     2. 不带动画的Fbx的命名为，英雄名
    /// </summary>
    #region 从Fbx中导出 AnimationClip
    [MenuItem("XArt/批量从Fbx中导出动画文件")]
    static private void ExportAniClip()
    {
        //DeleteAllAniClip();

        int iAniClipNum = 0;
        string strDir = AssetDatabase.GetAssetPath(Selection.activeObject);
        if (Directory.Exists(strDir))
        {
            DirectoryInfo kDI = new DirectoryInfo(strDir);
            FileInfo[] arrFI = kDI.GetFiles("*.fbx", SearchOption.AllDirectories);

            AnimationClip kAC = null;
            string strFileName, strExtention, strFbxDir, strFbxAssetPath;

            foreach (FileInfo kFI in arrFI)
            {
                strFileName = kFI.Name;
                strExtention = kFI.Extension;
                strFbxDir = XUtility.GetAssetDirPath(kFI.FullName);
                strFbxAssetPath = XUtility.GetAssetPath(kFI.FullName);
                ModelImporter kMI = ModelImporter.GetAtPath(strFbxAssetPath) as ModelImporter;
               
                if (kFI.Name.IndexOf("@") == -1)
                {
                    continue;
                }
                
                // 设置Fbx各项参数
                List<ModelImporterClipAnimation> listMCA = new List<ModelImporterClipAnimation>();
                foreach(ModelImporterClipAnimation kMCA in kMI.clipAnimations)
                {
                    listMCA.Add(kMCA);
                    kMCA.lockRootRotation = true;
                    kMCA.keepOriginalOrientation = true;
                    kMCA.lockRootHeightY = true;
                    kMCA.keepOriginalPositionY = true;
                    kMCA.lockRootPositionXZ = false;
                    kMCA.keepOriginalPositionXZ = false;
                }
                //kMI.clipAnimations = listMCA.ToArray();
                AssetDatabase.Refresh();

                UnityEngine.Object[] arrObj = AssetDatabase.LoadAllAssetRepresentationsAtPath(strFbxAssetPath);
                foreach (UnityEngine.Object obj in arrObj)
                {
                    kAC = obj as AnimationClip;
                    if (kAC != null)
                    {
                        if (kAC.name.IndexOf("Take") != -1)
                        {
                            kAC.name = strFileName.Substring(strFileName.IndexOf("@") + 1, strFileName.IndexOf(strExtention) - strFileName.IndexOf("@") - 1);
                        }
                        CreateAniClipAsset(kAC, strFbxDir, kAC.name);
                        iAniClipNum++;
                    }
                }
            }
        }

        if(iAniClipNum > 0)
        {
            string strContext = String.Format("导出完成，共导出{0}个动画文件", iAniClipNum);
            EditorUtility.DisplayDialog("导出动画文件", strContext, "确定");
        }
    }

    static private void CreateAniClipAsset(AnimationClip kSrcAniClip, string strDir, string strName)
    {
        string strClipAssetPath = strDir + strName + ".anim";

        AnimationClip kDstAniClip = new AnimationClip();
        EditorUtility.CopySerialized(kSrcAniClip, kDstAniClip);

        //if (AssetDatabase.LoadMainAssetAtPath(strClipAssetPath) != null)
        //{
        //    AssetDatabase.DeleteAsset(strClipAssetPath);
        //    AssetDatabase.Refresh();
        //}
        AssetDatabase.CreateAsset(kDstAniClip, strClipAssetPath);
        AssetDatabase.Refresh();
    }

    static private void DeleteAllAniClip()
    {
        string strDir = AssetDatabase.GetAssetPath(Selection.activeObject);
        if (Directory.Exists(strDir))
        {
            DirectoryInfo kDI = new DirectoryInfo(strDir);
            FileInfo[] arrFI = kDI.GetFiles("*.anim", SearchOption.AllDirectories);
            foreach (FileInfo kFI in arrFI)
            {
                kFI.Delete();
            }
            AssetDatabase.Refresh();
        }
    }
    #endregion

    /// <summary>
    /// 选中不带动画的模型，制作预设，会做一些优化
    /// </summary>
    #region 创建模型预设，并做些设置
    [MenuItem("XArt/制作模型预设(会做一些优化)")]
    static public void CreateModelPrefab()
    {
        string strModelPath = AssetDatabase.GetAssetPath(Selection.activeGameObject);
        if(strModelPath.EndsWith(".fbx", true, null))
        {
            FileInfo kFI = new FileInfo(strModelPath);
            if(kFI.Exists)
            {
                GameObject goFBX = AssetDatabase.LoadMainAssetAtPath(XUtility.GetAssetPath(kFI.FullName)) as GameObject;

                string strAssetDirPath = XUtility.GetAssetDirPath(kFI.FullName);
                string strPrefabPath = strAssetDirPath + goFBX.name + ".prefab";

                // create prefab
                if (AssetDatabase.LoadMainAssetAtPath(strPrefabPath) != null)
                {
                    AssetDatabase.DeleteAsset(strPrefabPath);
                }
                GameObject fbxPrefab = PrefabUtility.CreatePrefab(strPrefabPath, goFBX);
                AssetDatabase.Refresh();

                // reset render
                Renderer[] arrMR = fbxPrefab.GetComponentsInChildren<Renderer>(true);
                foreach (Renderer kMR in arrMR)
                {
                    kMR.castShadows = false;
                    kMR.receiveShadows = false;
                }

                EditorUtility.DisplayDialog("预设制作完成", "", "确定");
            }
        }
    }
  
    #endregion

    // 将AnimationClip添加到AnimatorController中
    //static private void CreateAniCtr()
    //{
    //    AnimatorController kAC = AnimatorController.CreateAnimatorControllerAtPath("Assets/haha.controller");
    //    AnimationClip ac = AssetDatabase.LoadMainAssetAtPath("Assets/attack.anim") as AnimationClip;
    //    AnimatorController.AddAnimationClipToController(kAC, ac);
    //}

    

}