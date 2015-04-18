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

    private float spawnChance;

    // Debugging

	// Use this for initialization
	void Start () {
        speed = Random.Range(0.04f, 0.1f);
        spawnChance = Random.Range(0.0f, 10.0f);
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
                length = Random.Range(0.3f, 5.0f);
                break;
        }
    }

    public void OnDeath()
    {
        if (!dead)
        {
            target.gameObject.GetComponent<PlayerVariables>().Kill();
            dead = true;
            GetComponent<MeshRenderer>().enabled = false;
            peepee.Play();
            if (Random.Range(0.0f, 100.0f) < EnemySpawn.PowerupSpawnFunc())
                spawnPowerup();
        }
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
        t = (t + speed * 0.1f) % 6.283185307179586476925286766559f;
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

        t += direction * speed * 0.4f;

        Vector3 pos = transform.position + transform.forward * speed;
        pos.x = centerXY.x + t;
        pos.y = centerXY.y + slope * t;

        transform.position = pos;
    }
    
    private void spawnPowerup()
    {
        GameObject pUp;

        float percent = Random.Range(0.0f, 100.0f);
        if (percent < 14) // 14% chance of laser strength.
        {
            pUp = (GameObject)Instantiate(Powerups.LaserStrengthPrefab);
        }
        else if (percent < 27) // 13% chance of spread shot.
        {
            pUp = (GameObject)Instantiate(Powerups.SpreadShotPrefab);
        }
        else if (percent < 40) // 13% chance of fire rate.
        {
            pUp = (GameObject)Instantiate(Powerups.FireRatePrefab);
        }
        else if (percent < 70) // 30% chance of basic shield.
        {
            pUp = (GameObject)Instantiate(Powerups.BasicShieldPrefab);
        }
        else if (percent < 80) // 10% chance of mirror shield.
        {
            pUp = (GameObject)Instantiate(Powerups.MirrorShieldPrefab);
        }
        else if (percent < 86) // 6% chance of invincibility.
        {
            pUp = (GameObject)Instantiate(Powerups.InvincibilityPrefab);
        }
        else // 14 % chance of steam.
        {
            pUp = (GameObject)Instantiate(Powerups.SteamPrefab);
        }

        pUp.transform.position = transform.position;
        print(pUp.GetComponent<PowerupBehavior>().PowerupType);
    }

    private void fire()
    {
        if (shotCooldown <= 0)
        {
            GameObject projectile = (GameObject)Instantiate(ProjectilePrefab);
            projectile.transform.position = transform.position;
            projectile.GetComponent<Projectile>().PlayerShot = false;
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
