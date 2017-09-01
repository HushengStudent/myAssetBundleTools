using UnityEngine;
using System.Collections;
using UnityEditor;
using System;

/// <summary>
/// 打包测试脚本;
/// </summary>
public static class BuildABs
{
    public static string outPutPath = "Assets/../AssetBundle-Test";

    [MenuItem("ABTest/Build Test")]
    public static void BuildTest()
    {
        BuildPipeline.BuildAssetBundles(outPutPath);
        AssetDatabase.Refresh();
    }

    [MenuItem("ABTest/Manifest Test")]
    public static void ManifestTest()
    {
        AssetBundle ab = AssetBundle.LoadFromFile(outPutPath + "/AssetBundle-Test");
        if (ab == null) return;

        AssetBundleManifest abManifest = ab.LoadAsset("AssetBundleManifest") as AssetBundleManifest;

        string[] abs = abManifest.GetAllAssetBundles();
        string[] abAllDepend;
        foreach (string temp in abs)
        {
            Debug.LogError("--->assetbundle:" + temp);
        }

        abAllDepend = abManifest.GetAllDependencies("cubea.assetbundle");

        foreach (string allStr in abAllDepend)
        {
            Debug.LogError("--->GetAllDependencies" + "--->" + "CubeA.prefab" + ":" + allStr);
        }

        abAllDepend = abManifest.GetDirectDependencies("cubea.assetbundle");

        foreach (string allStr in abAllDepend)
        {
            Debug.LogError("--->GetDirectDependencies" + "--->" + "CubeA.prefab" + ":" + allStr);
        }

        abAllDepend = abManifest.GetAllDependencies("sphereb.assetbundle");

        foreach (string allStr in abAllDepend)
        {
            Debug.LogError("--->GetAllDependencies" + "--->" + "SphereB.prefab" + ":" + allStr);
        }

        abAllDepend = abManifest.GetAllDependencies("capsulec.assetbundle");

        foreach (string allStr in abAllDepend)
        {
            Debug.LogError("--->GetAllDependencies" + "--->" + "CapsuleC.prefab" + ":" + allStr);
        }

        ab.Unload(true);
    }
}
