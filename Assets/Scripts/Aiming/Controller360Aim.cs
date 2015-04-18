using UnityEngine;
using System.Collections;

public class Controller360Aim : AimController {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        float axisX = Input.GetAxis("360 Right Horizontal");
        float axisY = Input.GetAxis("360 Right Vertical");

        Aim(axisX, axisY);
	}
}
