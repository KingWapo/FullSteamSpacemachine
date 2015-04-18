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
    private float spiralRadius;
    private float theta;
    private Vector2 centerXY;
    private Transform target;

    // Debugging
    private Vector3 forward;

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

        pathing();

        fire();
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Deathbox")
        {
            Destroy(this.gameObject);
        }
    }

    public void SetPath(Path myPath)
    {
        path = myPath;

        switch (myPath)
        {
            case Path.Straight:
                break;
            case Path.Spiral:
                spiralRadius = Random.Range(1.0f, 10.0f);
                theta = 0;
                centerXY = new Vector2(transform.position.x, transform.position.y);
                break;
        }
    }

    private void pathing()
    {
        switch(path)
        {
            case Path.Straight:
                transform.Translate(Vector3.forward * speed);
                break;
            case Path.Spiral:
                spiralPath();
                break;
        }
    }

    private void spiralPath()
    {
        theta = (theta + speed * 0.75f) % 6.283185307179586476925286766559f;
        forward = Vector3.forward;
        Vector3 pos = transform.position + transform.forward * speed;
        pos.x = centerXY.x + spiralRadius * Mathf.Cos(theta);
        pos.y = centerXY.y + spiralRadius * Mathf.Sin(theta);
        /*
        float x = centerXY.x + Mathf.Cos(theta);
        float y = centerXY.y + Mathf.Sin(theta);
        float z = transform.position.z + (Vector3.forward * speed).z;

        Vector3 pos = new Vector3(x, y, -z);*/

        transform.transform.position = pos;
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
