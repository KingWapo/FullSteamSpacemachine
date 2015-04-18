using UnityEngine;
using System.Collections;

public class EndGame : MonoBehaviour {

    public static bool End = false;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Alpha9)) End = true;
	}
}
