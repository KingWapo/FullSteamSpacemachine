﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public enum BtnType {
    Selected, Allowed, Unallowed, Unavailable
};

public class Menu : MonoBehaviour {

    public GameObject mainPanel;
    public GameObject playPanel;
    public GameObject controlPanel;
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

    public GameManager gameManager;

    public Color btn_selected = Color.green;
    public Color btn_unavailable = Color.red;
    public Color btn_allowed = Color.white;
    public Color btn_unallowed = Color.gray;

	// Use this for initialization
	void Start () {
        gameManager.moveType = MoveType.None_Move;
        gameManager.aimType = AimType.None_Aim;
        gameManager.shootType = ShootType.None_Shoot;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void PlayGame() {
        print("PLAY GAME");
        mainPanel.SetActive(false);
        playPanel.SetActive(true);

        ResetControls();
    }

    public void Controls() {
        mainPanel.SetActive(false);
        controlPanel.SetActive(true);
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
        controlPanel.SetActive(false);
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
                break;
            case 'C':
                gameManager.aimType = AimType.Mouse_Aim;
                UpdateAimButtons(BtnType.Unallowed, BtnType.Unallowed, BtnType.Selected);
                UpdateMoveButtons(BtnType.Unallowed, BtnType.Unallowed, BtnType.Allowed);
                break;
            case 'D':
                gameManager.moveType = MoveType.Joystick_Move;
                UpdateMoveButtons(BtnType.Selected, BtnType.Unallowed, BtnType.Unallowed);
                UpdateShootButtons(BtnType.Allowed, BtnType.Unallowed, BtnType.Unallowed);
                break;
            case 'E':
                gameManager.moveType = MoveType.Controller_Move;
                UpdateMoveButtons(BtnType.Unallowed, BtnType.Selected, BtnType.Unallowed);
                UpdateShootButtons(BtnType.Unallowed, BtnType.Allowed, BtnType.Unallowed);
                break;
            case 'F':
                gameManager.moveType = MoveType.Keyboard_Move;
                UpdateMoveButtons(BtnType.Unallowed, BtnType.Unallowed, BtnType.Selected);
                UpdateShootButtons(BtnType.Unallowed, BtnType.Unallowed, BtnType.Allowed);
                break;
            case 'G':
                gameManager.shootType = ShootType.Button_Shoot;
                UpdateShootButtons(BtnType.Selected, BtnType.Unallowed, BtnType.Unallowed);
                break;
            case 'H':
                gameManager.shootType = ShootType.Controller_Shoot;
                UpdateShootButtons(BtnType.Unallowed, BtnType.Selected, BtnType.Unallowed);
                break;
            case 'I':
                gameManager.shootType = ShootType.Keyboard_Shoot;
                UpdateShootButtons(BtnType.Unallowed, BtnType.Unallowed, BtnType.Selected);
                break;
            case 'R':
                UpdateAimButtons(BtnType.Allowed, BtnType.Allowed, BtnType.Allowed);
                UpdateMoveButtons(BtnType.Unallowed, BtnType.Unallowed, BtnType.Unallowed);
                UpdateShootButtons(BtnType.Unallowed, BtnType.Unallowed, BtnType.Unallowed);
                break;
        }
    }

    private void UpdateAimButtons(BtnType a_oc, BtnType a_co, BtnType a_mo) {
        if (Ovr.Hmd.Detect() > 0) {
            UpdateButton(aim_oculus, a_oc);
        } else {
            UpdateButton(aim_oculus, BtnType.Unavailable);
        }
        UpdateButton(aim_controller, a_co);
        UpdateButton(aim_mouse, a_mo);
    }

    private void UpdateMoveButtons(BtnType m_jo, BtnType m_co, BtnType m_ke) {
        if (Ovr.Hmd.Detect() > 0) {
            UpdateButton(move_joystick, m_jo);
        } else {
            UpdateButton(move_joystick, BtnType.Unavailable);
        }
        UpdateButton(move_controller, m_co);
        UpdateButton(move_keyboard, m_ke);
    }

    private void UpdateShootButtons(BtnType s_bu, BtnType s_co, BtnType s_ke) {
        if (Ovr.Hmd.Detect() > 0) {
            UpdateButton(shoot_button, s_bu);
        } else {
            UpdateButton(shoot_button, BtnType.Unavailable);
        }
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