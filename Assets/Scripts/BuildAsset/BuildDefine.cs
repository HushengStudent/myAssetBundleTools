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

public static class BuildDefine
{
    /// <summary>
    /// 打包选项,打包AssetBundle不压缩,使用第三方压缩软件压缩,再解压到沙盒路径,既可以减少包体,加快读取速度,但是占物理磁盘空间;
    /// CompleteAssets默认开启;CollectDependencies默认开启;DeterministicAssetBundle默认开启;ChunkBasedCompression使用LZ4压缩;
    /// </summary>
    public static BuildAssetBundleOptions options =  BuildAssetBundleOptions.UncompressedAssetBundle;

    /// <summary>
    /// assetbundle打包存放路径;
    /// </summary>
    public static string assetBundlePath = "Assets/../AssetBundle";

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

}
