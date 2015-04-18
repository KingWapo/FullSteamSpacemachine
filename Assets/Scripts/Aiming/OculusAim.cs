using UnityEngine;
using System.Collections;

public class OculusAim : AimController {

    public GameObject camera;

    // Update is called once per frame
    void Update() {
        if (shooter.IsShooting()) {
            Transform centerAnchor = camera.transform.FindChild("TrackingSpace").FindChild("CenterEyeAnchor");

            if (centerAnchor != null) {
                //print("TRANSFORM");
                shooter.OculusShoot(camera.transform);
            }
        }
    }
}
