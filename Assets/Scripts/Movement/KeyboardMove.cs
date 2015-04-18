using UnityEngine;
using System.Collections;

public class KeyboardMove : MoveController {
	
	// Update is called once per frame
	void Update () {
        float axisX = Input.GetAxis("Horizontal");
        float axisY = Input.GetAxis("Vertical");

        Move(axisX, axisY);
	}
}
