using UnityEngine;
using System.Collections;

public class JoystickMove : MoveController {
	
	// Update is called once per frame
	void Update () {
        float axisX = Input.GetAxis("Joystick Horizontal");
        float axisY = Input.GetAxis("Joystick Vertical");

        Move(axisX, axisY);
	}
}
