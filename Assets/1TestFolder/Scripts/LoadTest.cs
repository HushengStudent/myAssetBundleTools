using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// 加载测试脚本;
/// error:注意对PrefabA脚本中引用PrefabB的同时,在PrefabB脚本中也同时引用PrefabA.
/// </summary>
public class LoadTest : MonoBehaviour {

    private string outPutPath = "Assets/../AssetBundle-Test";

    AssetBundle ab;

    AssetBundleManifest abManifest;

    private List<string> abDic = new List<string>();

	// Use this for initialization
	void Start () 
    {
        ab = AssetBundle.LoadFromFile(outPutPath + "/AssetBundle-Test");
        abManifest = ab.LoadAsset("AssetBundleManifest") as AssetBundleManifest;

        AssetBundle assetbundle = LoadAB("cubea.assetbundle");

        GameObject cubea = assetbundle.LoadAsset("CubeA") as GameObject;

        if (cubea != null) GameObject.Instantiate(cubea);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    private AssetBundle LoadAB(string abName)
    {
        if (abName == null) return null;

        string[] abDirectDepend;

        abDirectDepend = abManifest.GetDirectDependencies(abName);

        AssetBundle assetbundle = AssetBundle.LoadFromFile(outPutPath + "/" + abName);

        if (ab == null) Debug.LogError("[LoadAB]Load AssetBundle: " + abName + " failure!");

        Debug.LogError("[LoadAB]Load AssetBundle: " + abName + " success!");

        foreach (string str in abDirectDepend)
        {
            LoadAB(str);
        }

        return assetbundle;
    }

    void OnGUI()
    {
        
    }
}
