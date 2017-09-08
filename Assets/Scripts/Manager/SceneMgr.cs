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
using System.Collections.Generic;

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
    /// <summary>
    /// 当前场景;
    /// </summary>
    private GameObject currentScene = null;

    /// <summary>
    /// SceneMgr资源回收器;
    /// </summary>
    private RecycleAssetContainer container = new RecycleAssetContainer();

    public void ShowScene(GameSceneEnum scene)
    {
        string sceneName = scene.ToString();
        if (string.IsNullOrEmpty(sceneName)) return;
        //加载Loading界面;
        if (loadingCtrl == null)
        {
            Object tempObject = ResourceMgr.Instance.LoadAssetFromAssetBundleSync(AssetType.Prefab, "Tool_Loading");
            loadingCtrl = ResourceMgr.Instance.GetAssetCtrl<LoadingController>(tempObject);
            DontDestroyOnLoad(loadingCtrl);
        }
        loadingCtrl.ShowLoading();
        if (currentScene != null)
        {
            GameObject.DestroyImmediate(currentScene);
            container.UnloadAsset();
        }
        //卸载无用资源;
        Resources.UnloadUnusedAssets();

        CoroutineMgr.Instance.StartCoroutine(ResourceMgr.Instance.LoadAssetFromAssetBundleAsync(AssetType.Prefab,sceneName,
            go =>
            {
                if (go == null) 
                {
                    ShowScene(GameSceneEnum.Scene_One);
                    Debug.Log(string.Format("[SceneMgr]Load Scene {0} failure!", sceneName));
                    return;
                }
                currentScene = go as GameObject;
                loadingCtrl.HideLoading();
                Debug.Log(string.Format("[SceneMgr]Load Scene {0} Success!", sceneName));
            },
            progressValue =>
            {
                Progress = progressValue;
            }
            ));
    }
}

/// <summary>
/// 游戏场景枚举;
/// </summary>
public enum GameSceneEnum
{
    Non       = 0,
    Scene_One = 1,
    Scene_Two = 2
}
