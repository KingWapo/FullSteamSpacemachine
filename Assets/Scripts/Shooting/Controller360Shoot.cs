using UnityEngine;
using System.Collections;

public class Controller360Shoot : ShootController {

    void Update() {
        Debug.DrawRay(ray.origin, ray.direction * 100, Color.cyan, 6, false);
    }

    public override bool IsShooting() {
        return Input.GetAxis("360 Trigger Right") > .1f;
    }
}
