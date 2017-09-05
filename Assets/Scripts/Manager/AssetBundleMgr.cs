/***************************************************************************************
* Name: AssetBundleMgr.cs
* Function:AssetBundle加载管理;
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
using System.Collections.Generic;

public class AssetBundleMgr : SingletonManager<AssetBundleMgr>
{
    /// <summary>
    /// 加载出来的AssetBundle缓存;
    /// </summary>
    private Dictionary<string, AssetBundle> assetBundleCache = new Dictionary<string, AssetBundle>();

    /// <summary>
    /// 加载出来的AssetBundle引用计数;
    /// </summary>
    private Dictionary<string, int> assetBundleReference = new Dictionary<string, int>();

    public void AssetBundleMainManifestInit()
    {
        AssetBundle assetBundle = AssetBundle.LoadFromFile(FilePathUtil.assetBundlePath+"AssetBundle");
        if (assetBundle != null)
        {

        }
        else
        {
            Debug.LogError(string.Format("[AssetBundleMgr]Load AssetBundle {0} failure", FilePathUtil.assetBundlePath+"AssetBundle"));
        }
    }

    //public AssetBundle LoadAssetBundleSyn(string path)
    //{

    //}
}
