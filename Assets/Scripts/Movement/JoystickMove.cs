using UnityEngine;
using System.Collections;

public class JoystickMove : MoveController {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        float axisX = Input.GetAxis("Joystick Horizontal");
        float axisY = Input.GetAxis("Joystick Vertical");

        Move(axisX, axisY);
	}
}
