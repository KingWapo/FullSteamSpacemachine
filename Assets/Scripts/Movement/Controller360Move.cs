﻿using UnityEngine;
using System.Collections;

public class Controller360Move : MoveController {
	
	// Update is called once per frame
    void Update() {
        float axisX = Input.GetAxis("360 Left Horizontal");
        float axisY = Input.GetAxis("360 Left Vertical");

        Move(axisX, axisY);
	}
}
