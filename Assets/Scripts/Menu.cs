using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public enum BtnType {
    Selected, Allowed, Unallowed, Unavailable
};

public class Menu : MonoBehaviour {

    public GameObject mainPanel;
    public GameObject playPanel;
    public GameObject scorePanel;
    public GameObject creditPanel;

    public Button aim_oculus;
    public Button aim_controller;
    public Button aim_mouse;

    public Button move_joystick;
    public Button move_controller;
    public Button move_keyboard;

    public Button shoot_button;
    public Button shoot_controller;
    public Button shoot_keyboard;

    public Button startGame;

    public GameManager gameManager;

    public Text[] boardNames;
    public Text[] boardScores;

    public Color btn_selected = Color.green;
    public Color btn_unavailable = Color.red;
    public Color btn_allowed = Color.white;
    public Color btn_unallowed = new Color(.75f, .75f, .75f);

	// Use this for initialization
	void Start () {
        gameManager.moveType = MoveType.None_Move;
        gameManager.aimType = AimType.None_Aim;
        gameManager.shootType = ShootType.None_Shoot;
	}
	
	// Update is called once per frame
	void Update () {
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
	}

    public void PlayGame() {
        print("PLAY GAME");
        mainPanel.SetActive(false);
        playPanel.SetActive(true);

        ResetControls();
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

    public void Aim_Oculus() {
        SetControl('A');
    }

    public void Aim_Controller() {
        SetControl('B');
    }

    public void Aim_Mouse() {
        SetControl('C');
    }

    public void Move_Joystick() {
        SetControl('D');
    }

    public void Move_Controller() {
        SetControl('E');
    }

    public void Move_Keyboard() {
        SetControl('F');
    }

    public void Shoot_Button() {
        SetControl('G');
    }

    public void Shoot_Controller() {
        SetControl('H');
    }

    public void Shoot_Keyboard() {
        SetControl('I');
    }

    public void StartGame() {
        Application.LoadLevel("Gameplay");
    }

    public void ResetControls() {
        SetControl('R');
    }

    private void SetControl(char button) {
        switch (button) {
            case 'A':
                gameManager.aimType = AimType.Oculus_Aim;
                UpdateAimButtons(BtnType.Selected, BtnType.Unallowed, BtnType.Unallowed);
                UpdateMoveButtons(BtnType.Allowed, BtnType.Allowed, BtnType.Allowed);
                break;
            case 'B':
                gameManager.aimType = AimType.Controller_Aim;
                UpdateAimButtons(BtnType.Unallowed, BtnType.Selected, BtnType.Unallowed);
                UpdateMoveButtons(BtnType.Unallowed, BtnType.Allowed, BtnType.Unallowed);
                goto case 'E';
            case 'C':
                gameManager.aimType = AimType.Mouse_Aim;
                UpdateAimButtons(BtnType.Unallowed, BtnType.Unallowed, BtnType.Selected);
                UpdateMoveButtons(BtnType.Unallowed, BtnType.Unallowed, BtnType.Allowed);
                goto case 'F';
            case 'D':
                gameManager.moveType = MoveType.Joystick_Move;
                UpdateMoveButtons(BtnType.Selected, BtnType.Unallowed, BtnType.Unallowed);
                UpdateShootButtons(BtnType.Allowed, BtnType.Unallowed, BtnType.Unallowed);
                goto case 'G';
            case 'E':
                gameManager.moveType = MoveType.Controller_Move;
                UpdateMoveButtons(BtnType.Unallowed, BtnType.Selected, BtnType.Unallowed);
                UpdateShootButtons(BtnType.Unallowed, BtnType.Allowed, BtnType.Unallowed);
                goto case 'H';
            case 'F':
                gameManager.moveType = MoveType.Keyboard_Move;
                UpdateMoveButtons(BtnType.Unallowed, BtnType.Unallowed, BtnType.Selected);
                UpdateShootButtons(BtnType.Unallowed, BtnType.Unallowed, BtnType.Allowed);
                goto case 'I';
            case 'G':
                gameManager.shootType = ShootType.Button_Shoot;
                UpdateShootButtons(BtnType.Selected, BtnType.Unallowed, BtnType.Unallowed);
                goto case 'S';
            case 'H':
                gameManager.shootType = ShootType.Controller_Shoot;
                UpdateShootButtons(BtnType.Unallowed, BtnType.Selected, BtnType.Unallowed);
                goto case 'S';
            case 'I':
                gameManager.shootType = ShootType.Keyboard_Shoot;
                UpdateShootButtons(BtnType.Unallowed, BtnType.Unallowed, BtnType.Selected);
                goto case 'S';
            case 'R':
                UpdateAimButtons(BtnType.Allowed, BtnType.Allowed, BtnType.Allowed);
                UpdateMoveButtons(BtnType.Unallowed, BtnType.Unallowed, BtnType.Unallowed);
                UpdateShootButtons(BtnType.Unallowed, BtnType.Unallowed, BtnType.Unallowed);
                startGame.interactable = false;
                break;
            case 'S':
                startGame.interactable = true;
                break;
        }
    }

    private void UpdateAimButtons(BtnType a_oc, BtnType a_co, BtnType a_mo) {
        UpdateButton(aim_oculus, a_oc);
        UpdateButton(aim_controller, a_co);
        UpdateButton(aim_mouse, a_mo);
    }

    private void UpdateMoveButtons(BtnType m_jo, BtnType m_co, BtnType m_ke) {
        UpdateButton(move_joystick, m_jo);
        UpdateButton(move_controller, m_co);
        UpdateButton(move_keyboard, m_ke);
    }

    private void UpdateShootButtons(BtnType s_bu, BtnType s_co, BtnType s_ke) {
        UpdateButton(shoot_button, s_bu);
        UpdateButton(shoot_controller, s_co);
        UpdateButton(shoot_keyboard, s_ke);
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
            case BtnType.Unallowed:
                btn.interactable = false;
                btn.image.color = btn_unallowed;
                break;
            case BtnType.Unavailable:
                btn.interactable = false;
                btn.image.color = btn_unavailable;
                break;
        }
    }
}
