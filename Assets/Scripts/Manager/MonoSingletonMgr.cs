/***************************************************************************************
* Name: MonoSingletonMgr.cs
* Function:单例;
* 
* Version     Date                Name                            Change Content
* ────────────────────────────────────────────────────────────────────────────────
* V1.0.0    20170908    http://blog.csdn.net/husheng0
* 
* Copyright (c). All rights reserved.
* 
***************************************************************************************/

using UnityEngine;
using System.Collections;

/// <summary>
/// 单例管理类的实现，继承Monobehaviour;
/// 该类会在Hierarchy下创建"___MonoSingleton"并把所有的继承MonoSingletonManager的脚本添加到"___MonoSingleton"上;
/// </summary>
/// <typeparam name="T"></typeparam>
public class MonoSingletonMgr<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T instance = null;

    public static T Instance
    {
        get
        {
            if (null == instance)
            {
                GameObject go = GameObject.Find("___MonoSingleton");
                if (null == go)
                {
                    go = new GameObject("___MonoSingleton");
                    DontDestroyOnLoad(go);
                }
                instance = go.AddComponent<T>();

            }
            return instance;
        }
    }

    private void OnApplicationQuit()
    {
        GameObject go = GameObject.Find("___MonoSingleton");
        if (go)
        {
            GameObject.DestroyImmediate(go);
        }
    }

}

