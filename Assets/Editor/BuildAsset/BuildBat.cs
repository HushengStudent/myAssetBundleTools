/***************************************************************************************
* Name: BuildBat.cs
* Function:AssetBundle打包方法;
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
using System.Diagnostics;
using Debug = UnityEngine.Debug;

public static class BuildBat
{
    /// <summary>
    /// 打包AssetBundle,默认只打包有更改的资源;
    /// </summary>
    [MenuItem("AssetBundleTools/BuildAssetBundle")]
    public static void BuildAllAssetBundle()
    {

    }

    public static void BuildAssetBundle(AssetBundleBuild[] builds)
    {
        Stopwatch watch = Stopwatch.StartNew();//开启计时;
        BuildPipeline.BuildAssetBundles(BuildDefine.assetBundlePath, builds, BuildDefine.options, BuildDefine.buildTarget);
        watch.Stop();
        Debug.LogWarning(string.Format("[BuildBat]BuildAllAssetBundle Spend Time:{0}s", watch.Elapsed.TotalSeconds));
    }
}
