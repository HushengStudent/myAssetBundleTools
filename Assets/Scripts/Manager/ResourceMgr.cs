/***************************************************************************************
* Name: ResourceMgr.cs
* Function:资源管理;
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
using System;
using Object = UnityEngine.Object;

public class ResourceMgr : SingletonManager<ResourceMgr>
{

    #region Function

    /// <summary>
    /// 创建资源加载器;
    /// </summary>
    /// <typeparam name="T">ctrl</typeparam>
    /// <param name="assetType">资源类型</param>
    /// <returns>资源加载器;</returns>
    private IAssetLoader<T> CreateLoader<T>(AssetType assetType)where T : Object
    {
        if (assetType == AssetType.Prefab) return new AssetLoader<T>();
        return new ResLoader<T>();
    }

    #endregion

    #region Asset Init

    public void InitNecessaryAsset()
    {
        //Shader初始化;
        AssetBundle shaderAssetBundle = AssetBundleMgr.instance.LoadShaderAssetBundle();
        if (shaderAssetBundle != null)
        {
            shaderAssetBundle.LoadAllAssets();
            Shader.WarmupAllShaders();
            Debug.Log("[ResourceMgr]Load Shader and WarmupAllShaders Success!");
        }
        else
        {
            Debug.LogError("[ResourceMgr]Load Shader and WarmupAllShaders failure!");
        }
        AssetBundleMgr.instance.UnloadMirroring(AssetType.Shader, "Shader");
    }
    #endregion

    #region Resources Load

    /// <summary>
    /// Resource同步加载;
    /// </summary>
    /// <typeparam name="T">ctrl</typeparam>
    /// <param name="type">资源类型</param>
    /// <param name="assetName">资源名字</param>
    /// <returns>ctrl</returns>
    public T LoadResourceSync<T>(AssetType type, string assetName) where T : Object
    {
        string path = FilePathUtil.GetResourcePath(type, assetName);
        IAssetLoader<T> loader = CreateLoader<T>(type);
        if (path != null)
        {
            T ctrl = Resources.Load<T>(path);
            if (ctrl != null)
            {
                return loader.GetAsset(ctrl);
            }
        }
        return null;
    }

    /// <summary>
    /// Resource异步加载;
    /// </summary>
    /// <typeparam name="T">ctrl</typeparam>
    /// <param name="type">资源类型</param>
    /// <param name="assetName">资源名字</param>
    /// <returns></returns>
    public IEnumerator LoadResourceAsync<T>(AssetType type, string assetName) where T : Object
    {
        IEnumerator itor = LoadResourceAsync<T>(type, assetName, null, null);
        while (itor.MoveNext())
        {
            yield return null;
        }
    }

    /// <summary>
    /// Resource异步加载;
    /// </summary>
    /// <typeparam name="T">ctrl</typeparam>
    /// <param name="type">资源类型</param>
    /// <param name="assetName">资源名字</param>
    /// <param name="action">资源回调</param>
    /// <returns></returns>
    public IEnumerator LoadResourceAsync<T>(AssetType type, string assetName, Action<T> action) where T : Object
    {
        IEnumerator itor = LoadResourceAsync<T>(type, assetName, action, null);
        while (itor.MoveNext())
        {
            yield return null;
        }
    }

    /// <summary>
    /// Resource异步加载;
    /// </summary>
    /// <typeparam name="T">ctrl</typeparam>
    /// <param name="type">资源类型</param>
    /// <param name="assetName">资源名字</param>
    /// <param name="action">资源回调</param>
    /// <param name="progress">进度回调</param>
    /// <returns></returns>
    public IEnumerator LoadResourceAsync<T>(AssetType type, string assetName, Action<T> action, Action<float> progress) where T : Object
    {
        string path = FilePathUtil.GetResourcePath(type, assetName);
        IAssetLoader<T> loader = CreateLoader<T>(type);

        T ctrl = null;
        if (path != null)
        {
            ResourceRequest request = Resources.LoadAsync<T>(path);
            while (request.progress < 0.99)
            {
                if (progress != null) progress(request.progress);
                yield return null;
            }
            while (!request.isDone)
            {
                yield return null;
            }
            ctrl = loader.GetAsset(request.asset as T);
        }
        if (action != null)
        {
            action(ctrl);
        }
    }
    #endregion

    #region AssetBundle Load

    /// <summary>
    /// Asset sync load from AssetBundle;
    /// </summary>
    /// <typeparam name="T">ctrl</typeparam>
    /// <param name="type">资源类型</param>
    /// <param name="assetName">资源名字</param>
    /// <returns>ctrl</returns>
    public T LoadAssetFromAssetBundleSync<T>(AssetType type, string assetName) where T : Object
    {
        T ctrl = null;
        IAssetLoader<T> loader = CreateLoader<T>(type);

        AssetBundle assetBundle = AssetBundleMgr.instance.LoadAssetBundleSync(type, assetName);
        if (assetBundle != null)
        {
            ctrl = loader.GetAsset(assetBundle.LoadAsset<T>(assetName) as T);
        }
        return ctrl;
    }

    /// <summary>
    /// Asset async load from AssetBundle;
    /// </summary>
    /// <typeparam name="T">ctrl</typeparam>
    /// <param name="type">资源类型</param>
    /// <param name="assetName">资源名字</param>
    /// <param name="action">资源回调</param>
    /// <param name="progress">progress回调</param>
    /// <returns></returns>
    public IEnumerator LoadAssetFromAssetBundleAsync<T>(AssetType type, string assetName, Action<T> action, Action<float> progress) where T : Object
    {
        T ctrl = null;
        AssetBundle assetBundle = null;
        IAssetLoader<T> loader = CreateLoader<T>(type);

        IEnumerator itor = AssetBundleMgr.instance.LoadAssetBundleAsync(type, assetName, 
            ab => 
            {
                assetBundle = ab;
            }, 
            null);
        while (itor.MoveNext())
        {
            yield return null;
        }

        AssetBundleRequest request = assetBundle.LoadAssetAsync<T>(assetName);
        while (request.progress < 0.99)
        {
            if (progress != null)
                progress(request.progress);
            yield return null;
        }
        while (!request.isDone)
        {
            yield return null;
        }
        ctrl = loader.GetAsset(request.asset as T);
        if (action != null)
            action(ctrl);
    }

    #endregion

}
