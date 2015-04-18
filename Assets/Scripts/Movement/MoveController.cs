using UnityEngine;
using System.Collections;

public class MoveController : MonoBehaviour {

    public float speed = 5;

    private float boundX = 7.0f;
    private float boundY = 4.0f;

    // -1 to 1 for each axis
    // call this to move character
    public void Move(float axisX, float axisY) {
        Vector3 oldPos = transform.position;

        float posX = oldPos.x;
        float posY = oldPos.y;

        float newX = Mathf.Clamp(Mathf.Lerp(posX, posX + axisX * speed, Time.deltaTime), -boundX, boundX);
        float newY = Mathf.Clamp(Mathf.Lerp(posY, posY + axisY * speed, Time.deltaTime), -boundY, boundY);

        Vector3 newPos = new Vector3(newX, newY, oldPos.z);
        transform.position = newPos;
    }
}
