﻿using UnityEngine;
using System.Collections;

public class Controller360Shoot : ShootController {
	
	// Update is called once per frame
	void Update () {
        float trigger = Input.GetAxis("360 Trigger Right");

        if (trigger > .1f) {
            StandardShoot();
        }
	}
}