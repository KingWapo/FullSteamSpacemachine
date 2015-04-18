using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour {

    public float Speed;
    public bool PlayerShot;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        transform.Translate(Vector3.forward * Speed);
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
            other.gameObject.GetComponent<EnemyController>().OnDeath();
            Destroy(this.gameObject);
        }
        else if (other.tag == "Deathbox")
        {
            Destroy(this.gameObject);
        }
    }
}
