﻿using UnityEngine;
using System.Collections;

public class ManagerManager : MonoBehaviour {

    public GameObject gameManagerPre;

	// Use this for initialization
	void Start () {
        GameObject manager = GameObject.Find("GameManager");

        if (manager == null) {
            manager = (GameObject)Instantiate(gameManagerPre, new Vector3(), Quaternion.identity);
        }

        manager.GetComponent<GameManager>().SpawnPlayer();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
