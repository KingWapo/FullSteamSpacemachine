using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HudController : MonoBehaviour {

    public Material HealthOn;
    public Material HealthOff;
    public Material SteamOn;
    public Material SteamOff;

    public List<GameObject> Health;
    public List<GameObject> FullSteam;

    private PlayerVariables playerVars;

	// Use this for initialization
	void Start () {
        playerVars = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerVariables>();
	}
	
	// Update is called once per frame
	void Update () {
        if (playerVars)
        {
            int health = playerVars.Health / 10;

            for (int i = 0; i < Health.Count; i++)
            {
                if (health > i) // Light should be on
                {
                    Health[i].GetComponent<MeshRenderer>().material = HealthOn;
                }
                else
                {
                    Health[i].GetComponent<MeshRenderer>().material = HealthOff;
                }
            }

            int steam = playerVars.SteamLevel;

            for (int i = 0; i < FullSteam.Count; i++)
            {
                if (steam > i)
                {
                    FullSteam[i].GetComponent<MeshRenderer>().material = SteamOn;
                }
                else
                {
                    FullSteam[i].GetComponent<MeshRenderer>().material = SteamOff;
                }
            }
        }
        else
        {
            playerVars = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerVariables>();
        }
	}
}
