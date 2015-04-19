using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyController : MonoBehaviour {
    // Enums to determine behavior
    public enum Path { Straight, ZigZag, Spiral, Chase, Max }
    public enum Attack { QuickStraight, SlowAim, Max }

    public Path path;
    public Attack attack;

    public GameObject ProjectilePrefab;

    public ParticleSystem peepee;

    public List<Material> mats;

    private bool dead;

    private float t;

    private float speed;
    private float originalSpeed;
    private float shotCooldown;

    private float spiralRadius;
    private Vector2 centerXY;

    private float slope;
    private float direction;
    private float length;

    private Transform target;

    private float spawnChance;

    private float spreadAngle = 1.0f;
    private int spreadShotLevel = 0;

    private int fireRateLevel = 0;

    private int basicShield = 0;

    // Debugging

	// Use this for initialization
	void Start () {
        speed = Random.Range(0.04f, 0.1f);
        originalSpeed = speed;
        spawnChance = Random.Range(0.0f, 10.0f);

        GameObject child = transform.GetChild(0).gameObject;
        Material mat = mats[Random.Range(0, mats.Count)];
        child.GetComponent<MeshRenderer>().material = mat;
        child.GetComponent<MeshRenderer>().materials[0] = mat;
	}

    private void DeterminePowerup() {
        basicShield = Random.Range(0, 2);
        //fireRateLevel = Random.Range(0, 6);
        //spreadShotLevel = Random.Range(0, 6);
    }
	
	// Update is called once per frame
	void Update () {

        if (EndGame.End) 
            OnDeath(false);

        if (!dead)
        {
            if (PlayerVariables.FullSteamSpacemachine)
            {
                speed = originalSpeed * 10;
            }
            else
            {
                speed = originalSpeed;
            }

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
        else if (other.tag == "Fullsteam")
        {
            target.GetComponent<PlayerVariables>().Bonus(30);
            OnDeath(true);
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

    public void OnDeath(bool byProj)
    {
        if (!dead) {
            if (basicShield <= 0) {
                if (byProj) {
                    target.gameObject.GetComponent<PlayerVariables>().Kill();
                }
                dead = true;
                transform.GetChild(0).gameObject.SetActive(false);
                peepee.Play();
                AudioSource source = GetComponent<AudioSource>();
                source.Play();

                if (Random.Range(0.0f, 100.0f) < EnemySpawn.PowerupSpawnFunc() && byProj)
                    spawnPowerup();
            }
        } else {
            DecrementShield();
        }
    }

    private void DecrementShield() {
        basicShield--;
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
            if (!PlayerVariables.FullSteamSpacemachine)
                pUp = (GameObject)Instantiate(Powerups.SteamPrefab);
            else
            {
                pUp = new GameObject();
                pUp.name = "DeleteMe";
            }
        }
        
        pUp.transform.position = transform.position;
        if (pUp.name == "DeleteMe")
            Destroy(pUp);
    }

    private void fire()
    {
        if (shotCooldown <= 0)
        {
            switch (attack)
            {
                case Attack.QuickStraight:
                    QuickProjectiles();
                    shotCooldown = 40 - 3 * fireRateLevel;
                    break;
                case Attack.SlowAim:
                    SlowProjectiles();
                    shotCooldown = 300 - 3 * fireRateLevel;
                    break;
            }
        }
        else
        {
            shotCooldown--;
        }
    }

    private void QuickProjectiles() {
        GameObject projectile = MakeQuickProjectile();

        switch (spreadShotLevel) {
            case 5:
                GameObject proj7 = MakeQuickProjectile();
                proj7.transform.Rotate(transform.up, spreadAngle);
                proj7.transform.Rotate(transform.right, -spreadAngle);
                GameObject proj8 = MakeQuickProjectile();
                proj8.transform.Rotate(transform.up, -spreadAngle);
                proj8.transform.Rotate(transform.right, spreadAngle);
                goto case 4;
            case 4:
                GameObject proj5 = MakeQuickProjectile();
                proj5.transform.Rotate(transform.up, spreadAngle);
                proj5.transform.Rotate(transform.right, spreadAngle);
                GameObject proj6 = MakeQuickProjectile();
                proj6.transform.Rotate(transform.up, -spreadAngle);
                proj6.transform.Rotate(transform.right, -spreadAngle);
                goto case 3;
            case 3:
                GameObject proj3 = MakeQuickProjectile();
                proj3.transform.Rotate(transform.right, spreadAngle);
                GameObject proj4 = MakeQuickProjectile();
                proj4.transform.Rotate(transform.right, -spreadAngle);
                goto case 2;
            case 2:
                GameObject proj1 = MakeQuickProjectile();
                proj1.transform.Rotate(transform.up, spreadAngle);
                GameObject proj2 = MakeQuickProjectile();
                proj2.transform.Rotate(transform.up, -spreadAngle);
                break;
        }
    }

    private void SlowProjectiles() {
        GameObject projectile = MakeSlowProjectile();

        switch (spreadShotLevel) {
            case 5:
                GameObject proj7 = MakeSlowProjectile();
                proj7.transform.Rotate(transform.up, spreadAngle);
                proj7.transform.Rotate(transform.right, -spreadAngle);
                GameObject proj8 = MakeSlowProjectile();
                proj8.transform.Rotate(transform.up, -spreadAngle);
                proj8.transform.Rotate(transform.right, spreadAngle);
                goto case 4;
            case 4:
                GameObject proj5 = MakeSlowProjectile();
                proj5.transform.Rotate(transform.up, spreadAngle);
                proj5.transform.Rotate(transform.right, spreadAngle);
                GameObject proj6 = MakeSlowProjectile();
                proj6.transform.Rotate(transform.up, -spreadAngle);
                proj6.transform.Rotate(transform.right, -spreadAngle);
                goto case 3;
            case 3:
                GameObject proj3 = MakeSlowProjectile();
                proj3.transform.Rotate(transform.right, spreadAngle);
                GameObject proj4 = MakeSlowProjectile();
                proj4.transform.Rotate(transform.right, -spreadAngle);
                goto case 2;
            case 2:
                GameObject proj1 = MakeSlowProjectile();
                proj1.transform.Rotate(transform.up, spreadAngle);
                GameObject proj2 = MakeSlowProjectile();
                proj2.transform.Rotate(transform.up, -spreadAngle);
                break;
        }
    }

    private GameObject MakeProjectile() {
        GameObject projectile = (GameObject)Instantiate(ProjectilePrefab);
        projectile.transform.position = transform.position;
        projectile.GetComponent<Projectile>().SetSpeed(.5f);
        projectile.GetComponent<Projectile>().PlayerShot = false;

        return projectile;
    }

    private GameObject MakeQuickProjectile() {
        GameObject projectile = MakeProjectile();
        projectile.transform.rotation = transform.rotation;

        return projectile;
    }

    private GameObject MakeSlowProjectile() {
        GameObject projectile = MakeProjectile();
        projectile.transform.LookAt(target);

        return projectile;
    }
}
