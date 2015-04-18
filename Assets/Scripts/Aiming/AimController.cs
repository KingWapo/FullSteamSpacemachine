using UnityEngine;
using System.Collections;

public class AimController : MonoBehaviour {

    public Texture crossheir;

    public float sensitivityX = 1.0f;
    public float sensitivityY = 1.0f;

    private float axisMod = 1000.0f;
    private float edgeBuffer = 50.0f;

    private float posX = Screen.width / 2.0f;
    private float posY = Screen.height / 2.0f;

    private float sightSize = 32.0f;

    // -1 to 1 for each axis
    // call this to aim
    public void Aim(float axisX, float axisY) {
        axisX *= axisMod;
        axisY *= axisMod;

        float newX = Mathf.Lerp(posX, posX + axisX * sensitivityX, Time.deltaTime);
        float newY = Mathf.Lerp(posY, posY + axisY * sensitivityY, Time.deltaTime);

        Vector2 newPos = new Vector2(newX, newY);

        posX = Mathf.Clamp(newPos.x, edgeBuffer, Screen.width - edgeBuffer);
        posY = Mathf.Clamp(newPos.y, edgeBuffer, Screen.height - edgeBuffer);
    }

    void OnGUI() {
        GUI.Label(new Rect(posX - sightSize / 2, posY - sightSize / 2, sightSize, sightSize), crossheir);
    }
}
