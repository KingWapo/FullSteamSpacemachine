using UnityEngine;
using System.Collections;

public class OculusAim : AimController {

    public GameObject ocCamera;

    // Update is called once per frame
    void Update() {
        if (shooter.IsShooting()) {
            Transform centerAnchor = ocCamera.transform.FindChild("TrackingSpace").FindChild("CenterEyeAnchor").GetChild(0);
            //print(ocCamera);

            if (centerAnchor != null) {
                //print(centerAnchor);
                shooter.OculusShoot(centerAnchor);
            }
        }
    }
}
