using UnityEngine;
using System.Collections;

public class AimController : MonoBehaviour {

    public float sensitivityX = 1.0f;
    public float sensitivityY = 1.0f;

    private float axisMod = 1000.0f;
    private float edgeBuffer = 50.0f;

    private float posX = Screen.width / 2.0f;
    private float posY = Screen.height / 2.0f;

    private float sightSize = 32.0f;

    private Sprite crosshair;

    // Use this for initialization
    void Start() {
        crosshair = (Sprite)Resources.LoadAssetAtPath("Assets/Imports/Crosshair.png", typeof(Sprite));
    }

    // -1 to 1 for each axis
    // call this to aim
    public void Aim(float axisX, float axisY) {
        axisX *= axisMod;
        axisY *= axisMod;

        float newX = Mathf.Lerp(posX, posX + axisX * sensitivityX, Time.deltaTime);
        float newY = Mathf.Lerp(posY, posY + axisY * sensitivityY, Time.deltaTime);

        posX = Mathf.Clamp(newX, edgeBuffer, Screen.width - edgeBuffer);
        posY = Mathf.Clamp(newY, edgeBuffer, Screen.height - edgeBuffer);
    }

    public Vector2 GetPos() {
        return new Vector2(posX, posY);
    }

    void OnGUI() {
        GUI.Label(new Rect(posX - sightSize / 2, posY - sightSize / 2, sightSize, sightSize), crosshair.texture);
    }
}
