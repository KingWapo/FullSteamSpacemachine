using UnityEngine;
using System.Collections;

public class MoveController : MonoBehaviour {

    public float speed = 5;

    private float boundX = 7.0f;
    private float boundY = 4.0f;

    private float rotBound = 20.0f;

    private Animation animation;

    // Use this for initialization
    void Start() {
        animation = gameObject.GetComponentInChildren<Animation>();
    }

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

        float rotX = transform.rotation.eulerAngles.x;
        rotX = rotX > 180.0f ? rotX - 360.0f : rotX;

        if (Mathf.Abs(Mathf.Abs(newY) - boundY) > .1f && Mathf.Abs(axisY) > .1f) {
            rotX = Mathf.Clamp(rotX - axisY * 2.0f, -rotBound, rotBound);
        } else {
            if (Mathf.Abs(rotX) > .01f) {
                rotX -= Mathf.Sign(rotX);
            }
        }

        float rotZ = transform.rotation.eulerAngles.z;
        rotZ = rotZ > 180.0f ? rotZ - 360.0f : rotZ;

        if (Mathf.Abs(Mathf.Abs(newX) - boundX) > .1f && Mathf.Abs(axisX) > .1f) {
            /*if (animation.isPlaying) {
                animation.Play("Move_Right_Start");
            }

            animation.PlayQueued("Move_Right", QueueMode.CompleteOthers);*/

            rotZ = Mathf.Clamp(rotZ - axisX * 2.0f, -rotBound, rotBound);
        } else {
            if (Mathf.Abs(rotZ) > .01f) {
                rotZ -= Mathf.Sign(rotZ);
            }
        }

        transform.rotation = Quaternion.Euler(new Vector3(rotX, 0, rotZ));
    }
}
