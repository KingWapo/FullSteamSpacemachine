using UnityEngine;
using System.Collections;

public class Controller360Shoot : ShootController {

    public override bool IsShooting() {
        return Input.GetAxis("360 Trigger Right") > .1f;
    }
}
