using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour {
    // Enums to determine behavior
    public enum Path { Straight, ZigZag, Spiral, Chase, Max }
    public enum Attack { QuickStraight, SlowAim, Max }

    public Path path;
    public Attack attack;

    public GameObject ProjectilePrefab;

    public ParticleSystem peepee;

    private bool dead;

    private float t;

    private float speed;
    private float shotCooldown;

    private float spiralRadius;
    private Vector2 centerXY;

    private float slope;
    private float direction;
    private float length;

    private Transform target;

    // Debugging

	// Use this for initialization
	void Start () {
        speed = Random.Range(0.01f, 0.05f);
	}
	
	// Update is called once per frame
	void Update () {
        if (!dead)
        {
            if (!target)
            {
                target = GameObject.FindGameObjectWithTag("Player").transform;
            }

            pathing();

            fire();
        }
        else
        {
            if (!peepee.isPlaying)
            {
                Destroy(this.gameObject);
            }
        }

        if (Input.GetKeyDown(KeyCode.Y)) OnDeath();
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
            case Path.Spiral:
                spiralRadius = Random.Range(1.0f, 10.0f);
                t = 0;
                centerXY = new Vector2(transform.position.x, transform.position.y);
                break;
            case Path.ZigZag:
                centerXY = new Vector2(transform.position.x, transform.position.y);
                t = 0;
                direction = 1;
                float rotation = Random.Range(0.0f, 2 * Mathf.PI);
                float rise = Mathf.Sin(rotation);
                float run = Mathf.Cos(rotation);
                slope = rise / run;
                length = Random.Range(1.0f, 10.0f);
                break;
        }
    }

    public void OnDeath()
    {
        dead = true;
        GetComponent<MeshRenderer>().enabled = false;
        peepee.Play();
    }

    private void pathing()
    {
        switch(path)
        {
            case Path.Chase:
                chasePath();
                break;
            case Path.Straight:
                transform.Translate(Vector3.forward * speed);
                break;
            case Path.Spiral:
                spiralPath();
                break;
            case Path.ZigZag:
                zigZagPath();
                break;
        }
    }

    private void chasePath()
    {
        transform.LookAt(target);
        transform.Translate(Vector3.forward * speed * 4);
    }

    private void spiralPath()
    {
        t = (t + speed * 0.75f) % 6.283185307179586476925286766559f;
        Vector3 pos = transform.position + transform.forward * speed;
        pos.x = centerXY.x + spiralRadius * Mathf.Cos(t);
        pos.y = centerXY.y + spiralRadius * Mathf.Sin(t);

        transform.position = pos;
    }

    private void zigZagPath()
    {
        if (Mathf.Abs(t) >= length)
        {
            direction *= -1;
        }

        t += direction * speed;

        Vector3 pos = transform.position + transform.forward * speed;
        pos.x = centerXY.x + t;
        pos.y = centerXY.y + slope * t;

        transform.position = pos;
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
