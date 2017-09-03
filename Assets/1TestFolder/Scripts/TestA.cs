using UnityEngine;
using System.Collections;

/// <summary>
/// testA
/// </summary>
public class TestA : MonoBehaviour {

    [SerializeField]
    private GameObject testB;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnGUI()
    {
        if (GUILayout.Button("Create B", GUILayout.Width(100f), GUILayout.Height(35f)))
        {
            //click button
            GameObject obj = Instantiate(testB);
        }
        if (GUILayout.Button("Create C", GUILayout.Width(100f), GUILayout.Height(35f)))
        {
            //click button
        }
    }
}
