using UnityEngine;
using System.Collections;
using UnityEditor;
using System;

public static class BuildABs
{
    private static string outPutPath = "Assets/_TestFolder/AssetBundles"; 

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
        string[] abDirectDepend;
        foreach (string temp in abs)
        {
            Debug.LogError("--->assetbundle:" + temp);
            abAllDepend = abManifest.GetAllDependencies(temp);
            foreach (string allStr in abAllDepend)
            {
                Debug.LogError("--->GetAllDependencies"+"--->"+temp+":" + allStr);
            }
            abDirectDepend = abManifest.GetDirectDependencies(temp);
            foreach (string directStr in abAllDepend)
            {
                Debug.LogError("--->GetDirectDependencies" + "--->" + temp + ":" + directStr);
            }
        }
    }
}
