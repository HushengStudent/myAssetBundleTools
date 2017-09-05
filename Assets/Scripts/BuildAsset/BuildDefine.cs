/***************************************************************************************
* Name: BuildDefine.cs
* Function:AssetBundle打包相关定义;
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
using UnityEditor;
using System;
using System.IO;

public static class BuildDefine
{
    /// <summary>
    /// 打包选项,打包AssetBundle不压缩,使用第三方压缩软件压缩,再解压到沙盒路径,既可以减少包体,加快读取速度,但是占物理磁盘空间;
    /// CompleteAssets默认开启;CollectDependencies默认开启;DeterministicAssetBundle默认开启;ChunkBasedCompression使用LZ4压缩;
    /// </summary>
    public static BuildAssetBundleOptions options = BuildAssetBundleOptions.UncompressedAssetBundle;

    /// <summary>
    /// assetbundle打包存放路径;
    /// </summary>
    public static string assetBundlePath = "Assets/../AssetBundle/";

    /// <summary>
    /// AssetBundle打包目标平台;
    /// </summary>
    public static BuildTarget buildTarget =

#if UNITY_IOS    //unity5.x UNITY_IPHONE换成UNITY_IOS
	BuildTarget.iOS;
#elif UNITY_ANDROID
    BuildTarget.Android;
#else
    EditorUserBuildSettings.activeBuildTarget;
#endif

    /// <summary>
    /// 根据资源路径获取资源类型;
    /// </summary>
    /// <param name="path">资源路径</param>
    /// <returns></returns>
    public static AssetType GetAssetType(string path)
    {
        if (null == path) return AssetType.Non;
        switch (Path.GetExtension(path).ToLower())
        {
            case ".anim":
                return AssetType.AnimeClip;
            case ".controller":
            case ".overridecontroller":
                return AssetType.AnimeCtrl;
            case ".ogg":
            case ".wav":
            case ".mp3":
                return AssetType.Audio;
            case ".png":
            case ".bmp":
            case ".tga":
            case ".psd":
            case ".dds":
            case ".jpg":
                return AssetType.Texture;
            case ".shader":
                return AssetType.Shader;
            case ".prefab":
                if (path.Contains("Atlas"))
                {
                    return AssetType.AssetPrefab;
                }
                return AssetType.Prefab;
            case ".unity":
                return AssetType.Scene;
            case ".mat":
                return AssetType.Material;
            case ".cs":
                return AssetType.Scripts;
            case ".ttf":
                return AssetType.Font;
        }
        return AssetType.Non;
    }
}

/// <summary>
/// 资源类型;
/// </summary>
public enum AssetType
{
    Non,
    Prefab,
    Scene,
    Material,
    Scripts,
    Font,
    /// <summary>
    /// 不需要实例化的的Prefab资源,如图集;
    /// </summary>
    AssetPrefab,
    Shader,
    Texture,
    Audio,
    AnimeCtrl,
    AnimeClip
}
