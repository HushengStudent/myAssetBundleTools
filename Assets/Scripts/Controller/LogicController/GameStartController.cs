/***************************************************************************************
* Name: GameStartController.cs
* Function:程序入口;
* 
* Version     Date                Name                            Change Content
* ────────────────────────────────────────────────────────────────────────────────
* V1.0.0    20170906    http://blog.csdn.net/husheng0
* 
* Copyright (c). All rights reserved.
* 
***************************************************************************************/

using UnityEngine;
using System.Collections;

public class GameStartController : MonoBehaviour {

	// Use this for initialization
	void Start () {

        AssetBundleMgr.Instance.LoadAssetBundleSync(AssetType.Prefab, "Scene_One");
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
