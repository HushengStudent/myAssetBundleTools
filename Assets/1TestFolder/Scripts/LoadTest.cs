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

    AssetBundle a;

    AssetBundle b;

    AssetBundle c;

    AssetBundleManifest abManifest;

    private List<string> abDic = new List<string>();

	// Use this for initialization
	void Start () 
    {
        ab = AssetBundle.LoadFromFile(outPutPath + "/AssetBundle-Test");
        abManifest = ab.LoadAsset("AssetBundleManifest") as AssetBundleManifest;
        LoadAB();
	}
	
	// Update is called once per frame
	void Update () {

	}

    private void LoadAB()
    {
        
        a = AssetBundle.LoadFromFile(outPutPath + "/" + "cubea.assetbundle");
        //b = AssetBundle.LoadFromFile(outPutPath + "/" + "sphereb.assetbundle");
        //c = AssetBundle.LoadFromFile(outPutPath + "/" + "capsulec.assetbundle");

    }

    void OnGUI()
    {
        if (GUILayout.Button("Create A", GUILayout.Width(100f), GUILayout.Height(35f)))
        {
            //click button
            GameObject cubea = a.LoadAsset("CubeA") as GameObject;

            if (cubea != null) GameObject.Instantiate(cubea);
        }
        if (GUILayout.Button("Create B", GUILayout.Width(100f), GUILayout.Height(35f)))
        {
            //click button
            GameObject cubea = b.LoadAsset("SphereB") as GameObject;

            if (cubea != null) GameObject.Instantiate(cubea);
        }
        if (GUILayout.Button("Create C", GUILayout.Width(100f), GUILayout.Height(35f)))
        {
            //click button
            GameObject cubea = c.LoadAsset("CapsuleC") as GameObject;

            if (cubea != null) GameObject.Instantiate(cubea);
        }
        if (GUILayout.Button("Unload A", GUILayout.Width(100f), GUILayout.Height(35f)))
        {
            //click button
            a.Unload(false);
        }
        if (GUILayout.Button("Unload B", GUILayout.Width(100f), GUILayout.Height(35f)))
        {
            b.Unload(false);

        }
        if (GUILayout.Button("Unload C", GUILayout.Width(100f), GUILayout.Height(35f)))
        {
            c.Unload(false);
          
        }
    }
}
