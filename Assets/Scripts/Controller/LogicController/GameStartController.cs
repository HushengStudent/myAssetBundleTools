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

public class GameStartController : MonoBehaviour {

	// Use this for initialization
	void Start ()
    {
        DontDestroyOnLoad(this.gameObject);
        StartGame();
	}

    private void StartGame()
    {
        GameInit();
    }

    private void GameInit()
    {
        ResourceMgr.Instance.InitNecessaryAsset();
    }

    void OnGUI()
    {
        if (GUILayout.Button("Show Scene_One", GUILayout.Width(200f), GUILayout.Height(50f)))
        {
            SceneMgr.Instance.ShowScene("Scene_One");
        }
    }
}
