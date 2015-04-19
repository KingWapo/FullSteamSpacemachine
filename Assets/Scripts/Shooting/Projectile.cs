using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour {

    public bool PlayerShot;

    private float origSpeed;
    private float speed;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
        
        if (PlayerVariables.FullSteamSpacemachine)
        {
            speed = origSpeed * 4;
        }
        else
        {
            speed = origSpeed;
        }

        transform.Translate(Vector3.forward * speed);
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && !PlayerShot)
        {
            if (other.gameObject.GetComponent<PlayerVariables>().TakeDamage(10))
            {
                transform.Rotate(transform.up, 180);
                PlayerShot = true;
            }
            else
            {
                Destroy(this.gameObject);
            }
        }
        else if (other.tag == "Enemy" && PlayerShot)
        {
            other.gameObject.GetComponent<EnemyController>().OnDeath(true);
            Destroy(this.gameObject);
        }
        else if (other.tag == "Deathbox")
        {
            Destroy(this.gameObject);
        }
        else if (other.tag == "Fullsteam" && !PlayerShot)
        {
            Destroy(this.gameObject);
        }
    }

    public void SetSpeed(float speedyBoy)
    {
        speed = speedyBoy;
        origSpeed = speed;
    }
}
