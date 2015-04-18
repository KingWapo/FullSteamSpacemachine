using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour {

    // Enums to determine behavior
    public enum Path { Straight, ZigZag, Spiral, Chase }
    public enum Attack { QuickStraight, SlowAim }

    public Path path;
    public Attack attack;

    public GameObject ProjectilePrefab;

    private float speed;
    private float shotCooldown;
    private Transform target;

	// Use this for initialization
	void Start () {
        speed = Random.Range(0.01f, 0.05f);
	}
	
	// Update is called once per frame
	void Update () {
        if (!target)
        {
            target = GameObject.FindGameObjectWithTag("Player").transform;
        }

        transform.Translate(Vector3.forward * speed);
        fire();
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Deathbox")
        {
            Destroy(this.gameObject);
        }
    }

    private void fire()
    {
        if (shotCooldown <= 0)
        {
            GameObject projectile = (GameObject)Instantiate(ProjectilePrefab);
            projectile.transform.position = transform.position;
            switch (attack)
            {
                case Attack.QuickStraight:
                    projectile.transform.rotation = transform.rotation;
                    projectile.GetComponent<Projectile>().Speed = .5f;
                    shotCooldown = 40;
                    break;
                case Attack.SlowAim:
                    projectile.transform.LookAt(target);
                    projectile.GetComponent<Projectile>().Speed = .5f;
                    shotCooldown = 300;
                    break;
            }
        }
        else
        {
            shotCooldown--;
        }
    }
}
