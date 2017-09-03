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
        if (GUILayout.Button("Create B", GUILayout.Width(300f), GUILayout.Height(35f)))
        {
            //click button

            GameObject.Instantiate(testB);
        }
    }
}
