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
using System.Collections.Generic;
using System.Text.RegularExpressions;

public class ResourceMgr : SingletonManager<ResourceMgr>
{

    #region Function

    /// <summary>
    /// 创建资源加载器;
    /// </summary>
    /// <typeparam name="T">ctrl</typeparam>
    /// <param name="assetType">资源类型</param>
    /// <returns>资源加载器;</returns>
    private IAssetLoader<T> CreateLoader<T>(AssetType assetType) where T : Object
    {
        if (assetType == AssetType.Prefab) return new ResLoader<T>();
        return new AssetLoader<T>();
    }


    /// <summary>
    /// AssetBundle不能直接加载获得脚本;
    /// </summary>
    /// <typeparam name="T">ctrl</typeparam>
    /// <param name="tempObject">Object</param>
    /// <returns>ctrl</returns>
    public T GetAssetCtrl<T>(Object tempObject) where T : Object
    {
        T ctrl = null;
        GameObject go = tempObject as GameObject;
        if (go != null)
        {
            ctrl = go.GetComponent<T>();
        }
        return ctrl;
    }

    #endregion

    #region Asset Init

    public void InitNecessaryAsset()
    {
        //Shader初始化;
        AssetBundle shaderAssetBundle = AssetBundleMgr.Instance.LoadShaderAssetBundle();
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
        //AssetBundleMgr.Instance.UnloadMirroring(AssetType.Shader, "Shader");
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
        Debug.LogError(string.Format("[ResourceMgr]LoadResourceSync Load Asset {0} failure!", assetName + "." + type.ToString()));
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
        else
        {
            Debug.LogError(string.Format("[ResourceMgr]LoadResourceAsync Load Asset {0} failure!", assetName + "." + type.ToString()));
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
    public Object LoadAssetFromAssetBundleSync(AssetType type, string assetName)
    {
        Object ctrl = null;
        IAssetLoader<Object> loader = CreateLoader<Object>(type);

        AssetBundle assetBundle = AssetBundleMgr.Instance.LoadAssetBundleSync(type, assetName);
        if (assetBundle != null)
        {
            Object tempObject = assetBundle.LoadAsset(assetName);
            ctrl = loader.GetAsset(tempObject);
        }
        if(ctrl== null)
            Debug.LogError(string.Format("[ResourceMgr]LoadAssetFromAssetBundleSync Load Asset {0} failure!", assetName + "." + type.ToString()));
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
    public IEnumerator LoadAssetFromAssetBundleAsync(AssetType type, string assetName, Action<Object> action, Action<float> progress)
    {
        Object ctrl = null;
        AssetBundle assetBundle = null;
        IAssetLoader<Object> loader = CreateLoader<Object>(type);

        IEnumerator itor = AssetBundleMgr.Instance.LoadAssetBundleAsync(type, assetName, 
            ab => 
            {
                assetBundle = ab;
            }, 
            null);
        while (itor.MoveNext())
        {
            yield return null;
        }

        AssetBundleRequest request = assetBundle.LoadAssetAsync(assetName);
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
        ctrl = loader.GetAsset(request.asset);
        if (ctrl == null)
            Debug.LogError(string.Format("[ResourceMgr]LoadAssetFromAssetBundleSync Load Asset {0} failure!", assetName + "." + type.ToString()));
        if (action != null)
            action(ctrl);
    }

    #endregion

}

/// <summary>
/// 待回收资源;
/// </summary>
public class RecycleAssetContainer
{
    private Dictionary<string, AssetType> RecycleDic = new Dictionary<string, AssetType>();

    /// <summary>
    /// 添加待回收资源;
    /// </summary>
    /// <param name="type">资源类型</param>
    /// <param name="assetName">资源名字</param>
    /// <returns>添加是否成功;</returns>
    public bool AddAsset(AssetType type, string assetName)
    {
        string key = assetName + "." + type;
        if (RecycleDic.ContainsKey(key))
        {
            Debug.LogWarning("[ResourceMgr]RecycleAssetContainer AddAsset is exist!");
            return false;
        }
        RecycleDic[key] = type;
        return true;
    }

    /// <summary>
    /// 回收资源;
    /// </summary>
    public void UnloadAsset()
    {
        foreach (var temp in RecycleDic)
        {
            string[] nameArg = Regex.Split(temp.Key, ".");
            if (nameArg.Length>0) AssetBundleMgr.Instance.UnloadAsset(temp.Value, nameArg[0]);
        }
        RecycleDic.Clear();
    }
}
