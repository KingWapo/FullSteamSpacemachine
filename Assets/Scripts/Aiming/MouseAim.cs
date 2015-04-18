using UnityEngine;
using System.Collections;

public class MouseAim : AimController {

    // Update is called once per frame
    void Update() {
        float axisX = Input.GetAxis("Mouse X");
        float axisY = -Input.GetAxis("Mouse Y");

        Aim(axisX, axisY);

        if (shooter.IsShooting()) {
            shooter.StandardShoot(GetPos());
        }
    }
}
