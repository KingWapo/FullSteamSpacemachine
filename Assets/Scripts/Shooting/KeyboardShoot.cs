using UnityEngine;
using System.Collections;

public class KeyboardShoot : ShootController {
	
	// Update is called once per frame
	void Update () {
        bool space = Input.GetKey(KeyCode.Space);

        if (space) {
            StandardShoot();
        }
	}
}
