using UnityEngine;
using System.Collections;
using UnityEditor;
using System;

public static class BuildABs
{
    private static string outPutPath = "Assets/_TestFolder/AssetBundles";

    private static string AssetBundleSrc = "Assets/_TestFolder/AssetBundleSrc/Prefab"; 

    [MenuItem("ABTest/Build")]
    public static void BuildTest()
    {
        BuildPipeline.BuildAssetBundles(outPutPath);
        AssetDatabase.Refresh();
    }

    [MenuItem("ABTest/Manifest")]
    public static void ManifestTest()
    {
        AssetBundle ab = AssetBundle.LoadFromFile(outPutPath + "/AssetBundles");
        if (ab == null) return;

        AssetBundleManifest abManifest = ab.LoadAsset("AssetBundleManifest") as AssetBundleManifest;

        string[] abs = abManifest.GetAllAssetBundles();
        string[] abAllDepend;
        foreach (string temp in abs)
        {
            Debug.LogError("--->assetbundle:" + temp);
        }

        abAllDepend = abManifest.GetAllDependencies("testa.assetbundle");

        foreach (string allStr in abAllDepend)
        {
            Debug.LogError("--->GetAllDependencies" + "--->" + "TestA.prefab" + ":" + allStr);
        }

        abAllDepend = abManifest.GetDirectDependencies("testa.assetbundle");

        foreach (string allStr in abAllDepend)
        {
            Debug.LogError("--->GetDirectDependencies" + "--->" + "TestA.prefab" + ":" + allStr);
        }

        abAllDepend = abManifest.GetAllDependencies("testb.assetbundle");

        foreach (string allStr in abAllDepend)
        {
            Debug.LogError("--->GetAllDependencies" + "--->" + "TestB.prefab" + ":" + allStr);
        }

        abAllDepend = abManifest.GetAllDependencies("testc.assetbundle");

        foreach (string allStr in abAllDepend)
        {
            Debug.LogError("--->GetAllDependencies" + "--->" + "TestC.prefab" + ":" + allStr);
        }

        ab.Unload(true);
    }
}
