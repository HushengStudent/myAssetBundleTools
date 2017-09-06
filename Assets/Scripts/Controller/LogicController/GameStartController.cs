using UnityEngine;
using System.Collections;

public class GameStartController : MonoBehaviour {

	// Use this for initialization
	void Start () {

        AssetBundleMgr.Instance.LoadAssetBundleSyn(AssetType.Prefab, "Scene_One");
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
