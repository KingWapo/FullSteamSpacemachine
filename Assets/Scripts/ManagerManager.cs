using UnityEngine;
using System.Collections;

public class ManagerManager : MonoBehaviour {

    public GameObject gameManagerPre;

	// Use this for initialization
	void Start () {
        if (GameObject.Find("GameManager") == null) {
            Instantiate(gameManagerPre, new Vector3(), Quaternion.identity);
        }
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
