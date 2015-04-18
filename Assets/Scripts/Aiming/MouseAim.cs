using UnityEngine;
using System.Collections;

public class MouseAim : AimController {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
    void Update() {
        float axisX = Input.GetAxis("Mouse X");
        float axisY = -Input.GetAxis("Mouse Y");

        Aim(axisX, axisY);
	}
}
