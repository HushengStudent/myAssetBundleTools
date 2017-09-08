/***************************************************************************************
* Name: SceneMgr.cs
* Function:场景管理;
* 
* Version     Date                Name                            Change Content
* ────────────────────────────────────────────────────────────────────────────────
* V1.0.0    20170905    http://blog.csdn.net/husheng0
* 
* Copyright (c). All rights reserved.
* 
***************************************************************************************/

using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using Object = UnityEngine.Object;
using System;

public class SceneMgr : MonoSingletonMgr<SceneMgr>
{
    private LoadingController loadingCtrl = null;

    private float progress;

    private float Progress
    {
        get { return progress; }
        set
        {
            progress = value;
            loadingCtrl.SetProgress(progress);
        }
    }

    public void ShowScene(string sceneName)
    {
        if (string.IsNullOrEmpty(sceneName)) return;

        if (loadingCtrl == null)
        {
            Object tempObject = ResourceMgr.Instance.LoadAssetFromAssetBundleSync(AssetType.Prefab, "Tool_Loading");
            loadingCtrl = ResourceMgr.Instance.GetAssetCtrl<LoadingController>(tempObject);
            DontDestroyOnLoad(loadingCtrl);
        }
        loadingCtrl.ShowLoading();

        
        Resources.UnloadUnusedAssets();

        ResourceMgr.Instance.LoadAssetFromAssetBundleSync(AssetType.Prefab, "Scene_One");

        loadingCtrl.HideLoading();
    }
}
