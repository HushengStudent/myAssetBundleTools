/***************************************************************************************
* Name: IAssetLoader.cs
* Function:Asset加载接口;
* 
* Version     Date                Name                            Change Content
* ────────────────────────────────────────────────────────────────────────────────
* V1.0.0    20170907    http://blog.csdn.net/husheng0
* 
* Copyright (c). All rights reserved.
* 
***************************************************************************************/

using UnityEngine;
using System.Collections;
using Object = UnityEngine.Object;

/// <summary>
/// 资源加载接口,主要区别是否实例化;
/// </summary>
public interface IAssetLoader<T> where T : Object
{
    T GetAsset(T t);
}

public class AssetLoader<T> : IAssetLoader<T> where T : Object
{
    public T GetAsset(T t)
    {
        return t;
    }
}

public class ResLoader<T> : IAssetLoader<T> where T : Object
{
    public T GetAsset(T t)
    {
        if (t == null) return null;
        return Object.Instantiate(t) as T;
    }
}
