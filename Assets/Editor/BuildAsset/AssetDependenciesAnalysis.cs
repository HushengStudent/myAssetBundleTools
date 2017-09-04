/***************************************************************************************
* Name: AssetDependenciesAnalysis.cs
* Function:资源依赖关系分析;
* 
* Version     Date                Name                            Change Content
* ────────────────────────────────────────────────────────────────────────────────
* V1.0.0    20170903    http://blog.csdn.net/husheng0
* 
* Copyright (c). All rights reserved.
* 
***************************************************************************************/

using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class AssetDependenciesAnalysis
{
    /// <summary>
    /// 打包路径下的全部资源;
    /// </summary>
    public HashSet<AssetNode> allAsset = new HashSet<AssetNode>();

    /// <summary>
    /// 
    /// </summary>
    public HashSet<AssetNode> independenceAsset = new HashSet<AssetNode>();
}

/// <summary>
/// 打包分析资源节点信息;
/// </summary>
public class AssetNode
{
    /// <summary>
    /// 资源类型;
    /// </summary>
    public AssetType type;
    /// <summary>
    /// 资源名字;
    /// </summary>
    public string assetName;
    /// <summary>
    /// 资源路径;
    /// </summary>
    public string assetPath;
    /// <summary>
    /// 被依赖的全部资源节点信息;
    /// </summary>
    public HashSet<AssetNode> parentDependentAssets;
    /// <summary>
    /// 依赖的全部资源节点信息;
    /// </summary>
    public HashSet<AssetNode> sonDependentAssets;

}
