using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum MoveType {
    Controller_Move,
    Keyboard_Move,
    None_Move
};

public enum ShootType {
    Controller_Shoot,
    Keyboard_Shoot,
    None_Shoot
};

public enum AimType {
    Controller_Aim,
    Mouse_Aim,
    Oculus_Aim,
    None_Aim
};

public class GameManager : MonoBehaviour {

    public GameObject playerShip;
    public GameObject projectile;
    public GameObject oculusController;
    public GameObject ocControllerInstance;

    public MoveType moveType = MoveType.Controller_Move;
    public AimType aimType = AimType.Controller_Aim;
    public ShootType shootType = ShootType.Controller_Shoot;

    public static AimType aimTypeStatic;

    private Vector3 cameraPos = new Vector3(0, 0, -10);

	// Use this for initialization
	void Start () {
        DontDestroyOnLoad(this);
	}

    public void SpawnPlayer() {
        GameObject ship = Instantiate(playerShip);

        switch (moveType) {
            case MoveType.Controller_Move:
                ship.AddComponent<Controller360Move>();
                break;
            case MoveType.Keyboard_Move:
                ship.AddComponent<KeyboardMove>();
                break;
        }

        ShootController shooter = null;

        switch (shootType) {
            case ShootType.Controller_Shoot:
                shooter = ship.AddComponent<Controller360Shoot>();
                break;
            case ShootType.Keyboard_Shoot:
                shooter = ship.AddComponent<KeyboardShoot>();
                break;
        }

        if (shooter != null) {
            shooter.projectilePre = projectile;
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
                ocControllerInstance = (GameObject)Instantiate(oculusController, cameraPos, Quaternion.identity);

                OculusAim oculus = ship.AddComponent<OculusAim>();
                oculus.ocCamera = ocControllerInstance;
                break;
        }

        aimTypeStatic = aimType;
    }
	
	// Update is called once per frame
	void Update () {
        if (Application.loadedLevelName == "Gameplay") {
            if (aimType == AimType.Oculus_Aim) {
                if (Input.GetKeyDown(KeyCode.Space)) {
                    Transform centerAnchor = ocControllerInstance.transform.FindChild("TrackingSpace").FindChild("CenterEyeAnchor").GetChild(0);

                    RaycastHit hit;
                    Ray ray = new Ray(centerAnchor.position, centerAnchor.forward);

                    Physics.Raycast(ray, out hit);

                    if (hit.transform != null) {
                        ManagerManager manager = GameObject.Find("ManagerManager").GetComponent<ManagerManager>();
                        switch (hit.transform.gameObject.name) {
                            case "Retry":
                                manager.Retry();
                                break;
                            case "MainMenu":
                                manager.MainMenu();
                                break;
                        }
                    } else {
                        print("no parent: " + hit.collider.name);
                    }
                }
            }
        }
	}

    // true if is high score
    public static bool SetHighScore(int score, string name) {
        /*List<int> scores = GetHighScores();
        List<string> names = GetScoreNames();

        for (int i = scores.Count - 1; i >= 0; i--) {
            if (score > scores[i]) {
                if (i != scores.Count - 1) {
                    scores[i + 1] = scores[i];
                    names[i + 1] = names[i];

                    scores[i] = score;
                    names[i] = name;

                    print(score + " larger than " + scores[i]);
                }
            }

            if (score <= scores[i] || i == 0) {
                for (int j = 0; j < scores.Count; j++) {
                    PlayerPrefs.SetInt("highscore" + j, scores[j]);
                    PlayerPrefs.SetString("name" + j, names[j]);
                    print("score[" + j + "]: " + scores[j]);
                }

                PlayerPrefs.Save();

                return true;
            }
        }

        return false;*/

        int newScore;
        string newName;
        int oldScore;
        string oldName;

        newScore = score;
        newName = name;

        bool newHighscore = false;

        for (int i = 0; i < 5; i++) {
            if (PlayerPrefs.HasKey("highscore" + i)) {
                if (PlayerPrefs.GetInt("highscore" + i, 0) < newScore) {
                    oldScore = PlayerPrefs.GetInt("highscore" + i, 0);
                    oldName = PlayerPrefs.GetString("name" + i, "");
                    PlayerPrefs.SetInt("highscore" + i, newScore);
                    PlayerPrefs.SetString("name" + i, newName);
                    newScore = oldScore;
                    newName = oldName;

                    newHighscore = true;
                }
            } else {
                PlayerPrefs.SetInt("highscore" + i, newScore);
                PlayerPrefs.SetString("name" + i, newName);
                newScore = 0;
                newName = "";
            }
        }
            return newHighscore;
    }

    public static List<int> GetHighScores() {
        List<int> scores = new List<int>();

        scores.Add(PlayerPrefs.GetInt("highscore0", 0));
        scores.Add(PlayerPrefs.GetInt("highscore1", 0));
        scores.Add(PlayerPrefs.GetInt("highscore2", 0));
        scores.Add(PlayerPrefs.GetInt("highscore3", 0));
        scores.Add(PlayerPrefs.GetInt("highscore4", 0));

        return scores;
    }

    public static List<string> GetScoreNames() {
        List<string> names = new List<string>();

        names.Add(PlayerPrefs.GetString("name0", "NONE"));
        names.Add(PlayerPrefs.GetString("name1", "NONE"));
        names.Add(PlayerPrefs.GetString("name2", "NONE"));
        names.Add(PlayerPrefs.GetString("name3", "NONE"));
        names.Add(PlayerPrefs.GetString("name4", "NONE"));

        return names;
    }
}
