using UnityEngine;
using System.Collections;

public class PowerupBehavior : MonoBehaviour {

    public Powerup PowerupType;

    private float threshold = 10.0f;
    private GameObject player;

	// Use this for initialization
	void Start () {
        transform.rotation = Quaternion.Euler(new Vector3(0, 180, 0));
        player = GameObject.FindGameObjectWithTag("Player");
	}
	
	// Update is called once per frame
	void Update () {
        float distance = Vector3.Distance(transform.position, player.transform.position);

        Vector3 bonus = Vector3.zero;

        if (distance < threshold)
        {
            Vector3 dir = player.transform.position - transform.position;
            dir.Normalize();
            bonus = dir * 2.0f / distance;
        }

        Vector3 pos = transform.forward * 0.1f;

        pos += bonus;

        transform.Translate(pos);
	}
}
