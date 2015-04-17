using UnityEngine;
using System.Collections;

public class MoveController : MonoBehaviour {

    public float speed;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    // -1 to 1 for each axis
    // call this to move character
    public void Move(float axisX, float axisY) {
        Vector3 oldPos = transform.position;

        float posX = oldPos.x;
        float posY = oldPos.y;
        float posZ = oldPos.z;

        float newX = Mathf.Lerp(posX, posX + axisX * speed, Time.deltaTime);
        float newY = Mathf.Lerp(posY, posY + axisY * speed, Time.deltaTime);

        Vector3 newPos = new Vector3(newX, newY, oldPos.z);
        transform.position = newPos;
    }
}
