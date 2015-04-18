using UnityEngine;
using System.Collections;

public enum MoveType {
    Controller_Move,
    Keyboard_Move,
    Joystick_Move
};

public enum AimType {
    Controller_Aim,
    Mouse_Aim,
    Oculus_Aim
};

public enum ShootType {
    Controller_Shoot,
    Keyboard_Shoot,
    Button_Shoot
};

public class GameManager : MonoBehaviour {

    public GameObject playerShip;
    public GameObject oculusController;

    public MoveType moveType = MoveType.Controller_Move;
    public AimType aimType = AimType.Controller_Aim;
    public ShootType shootType = ShootType.Controller_Shoot;

    private Vector3 cameraPos = new Vector3(0, 0, -10);

	// Use this for initialization
	void Start () {
        GameObject ship = Instantiate(playerShip);

        switch (moveType) {
            case MoveType.Controller_Move:
                ship.AddComponent<Controller360Move>();
                break;
            case MoveType.Keyboard_Move:
                ship.AddComponent<KeyboardMove>();
                break;
            case MoveType.Joystick_Move:
                ship.AddComponent<JoystickMove>();
                break;
        }

        switch (aimType) {
            case AimType.Controller_Aim:
                ship.AddComponent<Controller360Aim>();
                break;
            case AimType.Mouse_Aim:
                ship.AddComponent<MouseAim>();
                break;
            case AimType.Oculus_Aim:
                Camera.main.enabled = false;
                Instantiate(oculusController, cameraPos, Quaternion.identity);

                ship.AddComponent<OculusAim>();
                break;
        }

        switch (shootType) {
            case ShootType.Controller_Shoot:
                break;
            case ShootType.Keyboard_Shoot:
                break;
            case ShootType.Button_Shoot:
                break;
        }
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
