/***************************************************************************************
* Name: FilePathUtil.cs
* Function:路径管理工具;
* 
* Version     Date                Name                            Change Content
* ────────────────────────────────────────────────────────────────────────────────
* V1.0.0    20170901    http://blog.csdn.net/husheng0
* 
* Copyright (c). All rights reserved.
* 
***************************************************************************************/

using UnityEngine;
using System.Collections;

public static class FilePathUtil
{
    /// <summary>
    /// AssetBundle存放路径;
    /// </summary>
    public static string assetBundlePath =

#if UNITY_IOS && !UNITY_EDITOR    //unity5.x UNITY_IPHONE换成UNITY_IOS
	Application.persistentDataPath;
#elif UNITY_ANDROID && !UNITY_EDITOR
    Application.persistentDataPath;
#else
    BuildDefine.assetBundlePath;
#endif

    /// <summary>
    /// 需要打包的资源所在的目录;
    /// </summary>
    public static string resPath = "Assets/AssetBundleSrc/";

    /// <summary>
    /// 该路径下的资源单独打包,主要是为了方便使用资源,如图集,字体,场景大的背景贴图等等;
    /// </summary>
    public static string singleResPath = "Assets/AssetBundleSrc/SingleAssetBundleSrc";

    /// <summary>
    /// 获取AssetBundle文件的名字;
    /// </summary>
    /// <param name="type">资源类型</param>
    /// <param name="assetName">资源名字</param>
    /// <returns>AssetBundle资源名字</returns>
    public static string GetAssetBundleFileName(AssetType type,string assetName)
    {
        string assetBundleName = null;

        if (type == AssetType.Non || string.IsNullOrEmpty(assetName)) return assetBundleName;
        //AssetBundle的名字不支持大写;
        //AssetBundle打包命名方式为[assetType.assetName.assetbundle],开发时同种资源不允许名字相同,一般同一文件夹下不会重复,每个
        //文件夹下的资源都带有相同的前缀,不同文件夹下,资源前缀不同;
        assetBundleName = (type.ToString()+"."+assetName+".assetbundle").ToLower();
        return assetBundleName;
    }

    /// <summary>
    /// 获取AssetBundle文件加载路径;
    /// </summary>
    /// <param name="type">资源类型</param>
    /// <param name="assetName">资源名字</param>
    /// <returns>AssetBundle资源路径</returns>
    public static string GetAssetBundlePath(AssetType type, string assetName)
    {
        string assetBundleName = GetAssetBundleFileName(type, assetName);
        if (string.IsNullOrEmpty(assetBundleName)) return null;
        return assetBundlePath + assetBundleName;
    }

    /// <summary>
    /// 获取Resource文件加载路径;
    /// </summary>
    /// <param name="type">资源类型</param>
    /// <param name="assetName">资源名字</param>
    /// <returns>Resource资源路径;</returns>
    public static string GetResourcePath(AssetType type, string assetName)
    {
        if (type == AssetType.Non || type == AssetType.Scripts || string.IsNullOrEmpty(assetName)) return null;
        string assetPath = null;
        switch (type)
        {
            case AssetType.Prefab: assetPath = "Prefabs/"; break;
            default:
                assetPath = type.ToString() + "/";
                break;
        }
        assetPath = assetPath + assetName;
        return assetPath;
    }
}
