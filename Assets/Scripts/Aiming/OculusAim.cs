using UnityEngine;
using System.Collections;

public class OculusAim : AimController {

    public GameObject camera;

    // Update is called once per frame
    void Update() {
        if (shooter.IsShooting()) {
            Transform centerAnchor = camera.transform.FindChild("TrackingSpace").FindChild("CenterEyeAnchor").GetChild(0);
            print(camera);

            if (centerAnchor != null) {
                print(centerAnchor);
                shooter.OculusShoot(centerAnchor);
            }
        }
    }
}
