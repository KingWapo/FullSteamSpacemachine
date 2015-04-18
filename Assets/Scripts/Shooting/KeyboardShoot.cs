using UnityEngine;
using System.Collections;

public class KeyboardShoot : ShootController {
	
    public override bool IsShooting() {
        return Input.GetKey(KeyCode.Space);
    }
}
