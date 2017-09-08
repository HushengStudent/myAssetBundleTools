/***************************************************************************************
* Name: CoroutineMgr.cs
* Function:协程管理;
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
/// 协程管理类;实现单例;
/// </summary>
public class CoroutineMgr : MonoSingletonMgr<CoroutineMgr>//协程管理类需要继承MonoBehavior
{
    /// <summary>
    /// Start a Coroutine
    /// </summary>
    /// <param name="cor">Coroutine</param>
    /// <returns></returns>
    public Coroutine StartCoroutine(IEnumerator cor)
    {
        return ((MonoBehaviour)this).StartCoroutine(cor);
    }

    /// <summary>
    /// Close a Coroutine
    /// </summary>
    /// <param name="cor">Coroutine</param>
    public void StopCoroutine(IEnumerator cor)
    {
        ((MonoBehaviour)this).StopCoroutine(cor);
    }
}
