using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HighScoreManager : MonoBehaviour {

    public Text Name;

	// Use this for initialization
	void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {
        if (Menu.IsOculus)
        {
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetAxis("360 Trigger Right") > .1f)
            {
                Transform centerAnchor = GameObject.FindGameObjectWithTag("Tracking").transform.FindChild("CenterEyeAnchor").GetChild(0);

                RaycastHit hit;
                Ray ray = new Ray(centerAnchor.position, centerAnchor.forward);

                Physics.Raycast(ray, out hit);

                if (hit.collider != null)
                {
                    if (hit.collider.gameObject.tag == "Button")
                    {
                        Button(hit.collider.gameObject.GetComponent<Button>());
                    }
                }
            }
        }
	}

    public void Button(Button pressed)
    {
        if (pressed.name == "Del")
        {
            if (Name.text.Length > 0)
            {
                Name.text = Name.text.Substring(0, Name.text.Length - 1);
            }
        }
        else if (pressed.name == "End")
        {
            if (Name.text.Length < 3)
            {
                for (int i = Name.text.Length; i < 3; i++)
                {
                    Name.text += "_";
                }
            }
            int score = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerVariables>().calcScore();
            GameManager.SetHighScore(score, Name.text);
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerVariables>().setRegUI();
        }
        else
        {
            if (Name.text.Length < 3)
            {
                Name.text += pressed.name;
            }
            else
            {
                print(pressed.name);
                Name.text = Name.text.Substring(0, 2) + pressed.name;
            }
        }
    }
}
