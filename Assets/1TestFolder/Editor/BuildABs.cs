using UnityEngine;
using System.Collections;
using UnityEditor;
using System;
using System.Collections.Generic;
using UnityEditor.SceneManagement;

/// <summary>
/// 打包测试脚本;
/// </summary>
public static class BuildABs
{
    public static string outPutPath = "Assets/../AssetBundle-Test";

    [MenuItem("AssetBundleTest/AssetBundle Build Test")]
    public static void BuildTest()
    {
        AssetBundleBuild build = new AssetBundleBuild();
        build.assetBundleName = "cubea.assetbundle";
        List<string> assetName = new List<string>();
        assetName.Add("Assets/1TestFolder/AssetBundleSrc/Prefab/CubeA.prefab");
        build.assetNames = assetName.ToArray();

        //AssetBundleBuild build2 = new AssetBundleBuild();
        //build2.assetBundleName = "capsulec.assetbundle";
        //List<string> assetName2 = new List<string>();
        //assetName2.Add("Assets/1TestFolder/AssetBundleSrc/Prefab/CapsuleC.prefab");
        //build2.assetNames = assetName2.ToArray();

        List<AssetBundleBuild> buildList = new List<AssetBundleBuild>();
        buildList.Add(build);
        //buildList.Add(build2);

        BuildPipeline.BuildAssetBundles(outPutPath, buildList.ToArray(),BuildDefine.options);
        //BuildPipeline.BuildAssetBundles(outPutPath,BuildDefine.options);
        AssetDatabase.Refresh();

        EditorUtility.UnloadUnusedAssetsImmediate();
    }

    [MenuItem("AssetBundleTest/AssetBundle Manifest Test")]
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
