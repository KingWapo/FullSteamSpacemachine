using UnityEngine;
using System.Collections;

public class KeyboardMove : MonoBehaviour {

    private MoveController moveController;

	// Use this for initialization
	void Start () {
        moveController = GetComponent<MoveController>();
	}
	
	// Update is called once per frame
	void Update () {
        float axisX = Input.GetAxis("horizontal");
        float axisY = Input.GetAxis("vertical");

        moveController.Move(axisX, axisY);
	}
}
