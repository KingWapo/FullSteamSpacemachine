using UnityEngine;
using System.Collections;

public class KeyboardMove : MoveController {

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
        float axisX = Input.GetAxis("Horizontal");
        float axisY = Input.GetAxis("Vertical");

        Move(axisX, axisY);
	}
}
