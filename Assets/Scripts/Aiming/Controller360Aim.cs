using UnityEngine;
using System.Collections;

public class Controller360Aim : AimController {

    // Update is called once per frame
    void Update() {
        float axisX = Input.GetAxis("360 Right Horizontal");
        float axisY = Input.GetAxis("360 Right Vertical");

        Aim(axisX, axisY);

        if (shooter.IsShooting()) {
            shooter.StandardShoot(GetPos());
        }
    }
}
