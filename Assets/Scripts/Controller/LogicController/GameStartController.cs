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
using UnityEngine.SceneManagement;

public class GameStartController : MonoBehaviour
{
    #region Init

    void Start ()
    {
        StartGame();
	}

    private void StartGame()
    {
        GameInit();
    }

    private void GameInit()
    {
        ResourceMgr.Instance.InitNecessaryAsset();
        SceneMgr.Instance.ShowScene(GameSceneEnum.Scene_One);//进入游戏默认打开Scene_One;
    }

    #endregion

}
