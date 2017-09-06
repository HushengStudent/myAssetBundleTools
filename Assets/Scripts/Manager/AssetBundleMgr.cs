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
using System;

public class AssetBundleMgr : SingletonManager<AssetBundleMgr>
{
    #region Field

    /// <summary>
    /// 加载出来的AssetBundle缓存;
    /// </summary>
    private Dictionary<string, AssetBundle> assetBundleCache = new Dictionary<string, AssetBundle>();

    /// <summary>
    /// 加载出来的AssetBundle引用计数;
    /// </summary>
    private Dictionary<string, int> assetBundleReference = new Dictionary<string, int>();

    /// <summary>
    /// 依赖关系AssetBundle;
    /// </summary>
    private AssetBundle mainAssetBundle;
    
    /// <summary>
    /// AssetBundleManifest
    /// </summary>
    private AssetBundleManifest manifest;

    /// <summary>
    /// 依赖关系AssetBundle;
    /// </summary>
    private AssetBundle MainAssetBundle
    {
        get
        {
            if (null == mainAssetBundle)
            {
                mainAssetBundle = AssetBundle.LoadFromFile(FilePathUtil.assetBundlePath + "AssetBundle");
            }
            if (mainAssetBundle == null)
            {
                Debug.LogError(string.Format("[AssetBundleMgr]Load AssetBundle {0} failure!", FilePathUtil.assetBundlePath + "AssetBundle"));
            }
            return mainAssetBundle;
        }
    }

    /// <summary>
    /// AssetBundleManifest
    /// </summary>
    private AssetBundleManifest Manifest
    {
        get
        {
            if (null == manifest && MainAssetBundle != null)
            {
                manifest = MainAssetBundle.LoadAsset("AssetBundleManifest") as AssetBundleManifest;
            }
            if (manifest == null)
            {
                Debug.LogError(string.Format("[AssetBundleMgr]Load AssetBundleManifest {0} failure!", FilePathUtil.assetBundlePath + "AssetBundle"));
            }
            return manifest;
        }
    }

    /// <summary>
    /// 正在异步加载中的AssetBundle;
    /// </summary>
    public HashSet<string> assetBundleLoading = new HashSet<string>();

    #endregion

    #region Function

    /// <summary>
    /// AssetBundle是否正在加载;
    /// </summary>
    /// <param name="path"></param>
    /// <returns></returns>
    public bool IsAssetBundleLoading(string path)
    {
        return assetBundleLoading.Contains(path);
    }

    #endregion

    #region AssetBundle Load

    /// <summary>
    /// AssetBundle同步加载LoadFromFile;
    /// </summary>
    /// <param name="path">AssetBundle文件路径</param>
    /// <returns>AssetBundle</returns>
    private AssetBundle LoadSingleAssetBundleSyn(string path)
    {
        if (string.IsNullOrEmpty(path)) return null;

        AssetBundle assetBundle = null;
        if (!assetBundleCache.ContainsKey(path))
        {
            try
            {
                assetBundle = AssetBundle.LoadFromFile(path);
                if (null == assetBundle)
                {
                    Debug.Log(string.Format("[AssetBundleMgr]Load AssetBundle {0} failure!", path));
                }
                else
                {
                    assetBundleCache[path] = assetBundle;
                    assetBundleReference[path] = 1;
                }
            }
            catch (Exception e)
            {
                Debug.LogError(e.ToString());
            }
        }
        else
        {
            assetBundle = assetBundleCache[path];
            assetBundleReference[path]++;
        }
        return assetBundle;
    }

    /// <summary>
    /// AssetBundle异步加载LoadFromFileAsync,www异步加载消耗大于LoadFromFileAsync;
    /// </summary>
    /// <param name="path">AssetBundle文件路径</param>
    /// <returns>AssetBundle</returns>
    private IEnumerator LoadSingleAssetBundleAsyn(string path, Action<AssetBundle> action)
    {
        AssetBundle assetBundle = null;
        while (IsAssetBundleLoading(path))
        {
            yield return null;
        }
        if (!assetBundleCache.ContainsKey(path))
        {
            //开始加载;
            assetBundleLoading.Add(path);
            AssetBundleCreateRequest assetBundleReq = AssetBundle.LoadFromFileAsync(path);
            while (!assetBundleReq.isDone)
            {
                yield return null;
            }
            assetBundle = assetBundleReq.assetBundle;
            if (assetBundle == null)
            {
                Debug.Log(string.Format("[AssetBundleMgr]Load AssetBundle {0} failure!", path));
            }
            else
            {
                assetBundleCache[path] = assetBundle;
                assetBundleReference[path] = 1;

            }
            //加载完毕;
            assetBundleLoading.Remove(path);
        }
        else
        {
            assetBundle = assetBundleCache[path];
            assetBundleReference[path]++;
        }
        if (action != null) action(assetBundle);
    }

    /// <summary>
    /// AssetBundle同步加载;
    /// </summary>
    /// <param name="type">资源类型</param>
    /// <param name="assetName">资源名字</param>
    /// <returns>AssetBundle</returns>
    public AssetBundle LoadAssetBundleSyn(AssetType type, string assetName)
    {
        if (type == AssetType.Non || string.IsNullOrEmpty(assetName)) return null;

        string assetBundlePath = FilePathUtil.GetAssetBundlePath(type, assetName);
        if (assetBundlePath == null) return null;
        string assetBundleName = FilePathUtil.GetAssetBundleFileName(type, assetName);

        AssetBundle assetBundle = LoadSingleAssetBundleSyn(assetBundlePath);
        if (assetBundle == null) return null;

        string[] DependentAssetBundle = Manifest.GetAllDependencies(assetBundleName);
        foreach (string tempAssetBundle in DependentAssetBundle)
        {
            string tempPtah = FilePathUtil.assetBundlePath + tempAssetBundle;
            LoadSingleAssetBundleSyn(tempPtah);
        }
        return assetBundle;
    }

    #endregion

}
