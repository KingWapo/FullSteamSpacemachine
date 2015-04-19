using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public enum BtnType {
    Selected, Allowed, Unavailable
};

public class Menu : MonoBehaviour {
    public bool isOculus = false;

    public GameObject mainPanel;
    public GameObject playPanel;
    public GameObject scorePanel;
    public GameObject creditPanel;

    public Button controller360;
    public Button mouseKeyboard;

    public Button startGame;

    public GameManager gameManager;

    public Text[] boardNames;
    public Text[] boardScores;

    public Color btn_selected = Color.green;
    public Color btn_unavailable = new Color(.75f, .75f, .75f);
    public Color btn_allowed = Color.white;

    private GameObject ocController;

    private Ray ray;

	// Use this for initialization
	void Start () {
        if (isOculus) {
            ocController = (GameObject)Instantiate(gameManager.oculusController, Camera.main.transform.position, Camera.main.transform.rotation);
            Camera.main.enabled = false;
        }

        gameManager.moveType = MoveType.None_Move;
        gameManager.aimType = AimType.None_Aim;
        gameManager.shootType = ShootType.None_Shoot;
	}
	
	// Update is called once per frame
	void Update () {
        /*
        if (Input.GetKeyDown(KeyCode.Space)) {
            if (GameManager.SetHighScore(Random.Range(0, 500), "TEST")) {
                print("NEW HIGH SCORE");
                Leaderboards();
            } else {
                print("no score");
            }
        }

        if (Input.GetKeyDown(KeyCode.Backspace)) {
            PlayerPrefs.DeleteAll();
            Leaderboards();
        }
        */
        //print("Test");
        Debug.DrawRay(ray.origin, ray.direction * 100, Color.blue, 6, false);

        if (isOculus) {
            if (Input.GetKeyDown(KeyCode.Space)) {
                if (ocController != null) {
                    Transform centerAnchor = ocController.transform.FindChild("TrackingSpace").FindChild("CenterEyeAnchor").GetChild(0);

                    RaycastHit hit;
                    ray = new Ray(centerAnchor.position, centerAnchor.forward);

                    Physics.Raycast(ray, out hit);

                    if (hit.collider != null) {
                        print("hit name: " + hit.transform.parent.name);
                        switch (hit.transform.parent.name) {
                            case "Play":
                                PlayGame();
                                break;
                            case "High Scores":
                                Leaderboards();
                                break;
                            case "Credits":
                                Credits();
                                break;
                            case "Exit":
                                Exit();
                                break;
                            case "Start Game":
                                StartGame();
                                break;
                            case "Main Menu":
                                Home();
                                break;
                            case "Controller":
                                Controller();
                                break;
                            case "MouseKeyboard":
                                MouseKeyboard();
                                break;
                        }
                    }
                }
            }
        }
	}

    public void PlayGame() {
        print("PLAY GAME");
        mainPanel.SetActive(false);
        playPanel.SetActive(true);

        UpdateButton(controller360, BtnType.Allowed);
        UpdateButton(mouseKeyboard, BtnType.Allowed);
        UpdateButton(startGame, BtnType.Unavailable);
    }

    public void Leaderboards() {
        mainPanel.SetActive(false);
        scorePanel.SetActive(true);

        List<string> names = GameManager.GetScoreNames();
        List<int> scores = GameManager.GetHighScores();

        for (int i = 0; i < boardNames.Length; i++) {
            boardNames[i].text = names[i];
            boardScores[i].text = scores[i] + "";
        }
    }

    public void Credits() {
        mainPanel.SetActive(false);
        creditPanel.SetActive(true);
    }

    public void Exit() {
        Application.Quit();
    }

    public void Home() {
        mainPanel.SetActive(true);
        playPanel.SetActive(false);
        scorePanel.SetActive(false);
        creditPanel.SetActive(false);
    }

    public void Controller() {
        UpdateButton(startGame, BtnType.Allowed);
        if (isOculus) {
            gameManager.aimType = AimType.Oculus_Aim;
        } else {
            gameManager.aimType = AimType.Controller_Aim;
        }
        gameManager.moveType = MoveType.Controller_Move;
        gameManager.shootType = ShootType.Controller_Shoot;
    }

    public void MouseKeyboard() {
        UpdateButton(startGame, BtnType.Allowed);
        if (isOculus) {
            gameManager.aimType = AimType.Oculus_Aim;
        } else {
            gameManager.aimType = AimType.Mouse_Aim;
        }
        gameManager.moveType = MoveType.Keyboard_Move;
        gameManager.shootType = ShootType.Keyboard_Shoot;
    }

    public void StartGame() {
        Application.LoadLevel("Gameplay");
    }

    private void UpdateButton(Button btn, BtnType btnType) {
        switch (btnType) {
            case BtnType.Selected:
                btn.interactable = true;
                btn.image.color = btn_selected;
                break;
            case BtnType.Allowed:
                btn.interactable = true;
                btn.image.color = btn_allowed;
                break;
            case BtnType.Unavailable:
                btn.interactable = false;
                btn.image.color = btn_unavailable;
                break;
        }
    }
}
