using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public enum Powerup
{
    LaserStrength,
    SpreadShot,
    FireRate,
    BasicShield,
    MirrorShield,
    Invincibility,
    Steam
}

public class PlayerVariables : MonoBehaviour {

// Score variables

    public int Health;
    public int Kills;
    public int PowerupCount;
    public int DamageTaken;
    public int DamageDealt;

    private Text scoreText;
    private int previousScore = 0;

// Powerup info

    // Offensive
    public int LaserStrengthLevel = 1;
    public int SpreadShotLevel = 1;
    public int FireRateLevel = 1;
        
    // Defensive
    public int BasicShields = 0;
    private int mirrorTime = 120;
    private int invTime = 600;

    // Full Steam
    public int SteamLevel = 0;

    // Debugging;
    public bool Debugging;

	// Use this for initialization
	void Start () {
        scoreText = GameObject.FindGameObjectWithTag("Score").GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
        if (Debugging)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1)) AddPowerup(Powerup.LaserStrength);
            if (Input.GetKeyDown(KeyCode.Alpha2)) AddPowerup(Powerup.SpreadShot);
            if (Input.GetKeyDown(KeyCode.Alpha3)) AddPowerup(Powerup.FireRate);
            if (Input.GetKeyDown(KeyCode.Alpha4)) AddPowerup(Powerup.BasicShield);
            if (Input.GetKeyDown(KeyCode.Alpha5)) AddPowerup(Powerup.MirrorShield);
            if (Input.GetKeyDown(KeyCode.Alpha6)) AddPowerup(Powerup.Invincibility);
            if (Input.GetKeyDown(KeyCode.Alpha7)) AddPowerup(Powerup.Steam);
        }

        updateTimedPowerups();

        if (previousScore < calcScore())
        {
            previousScore = (int)Mathf.Lerp(previousScore, calcScore(), Time.deltaTime);
        }
        scoreText.text = "Score: " + previousScore;
	}

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Enemy")
        {
            col.gameObject.GetComponent<EnemyController>().OnDeath();
            TakeDamage(30);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Powerup")
        {
            AddPowerup(other.gameObject.GetComponent<PowerupBehavior>().PowerupType);
            Destroy(other.gameObject);
        }
    }

    private void updateTimedPowerups()
    {
        if (invTime > 0)
        {
            invTime--;
        }
        if (mirrorTime > 0)
        {
            mirrorTime--;
        }
    }

    public bool TakeDamage(int damage)
    {
        if (invTime > 0)
        {

        }
        else if (mirrorTime > 0)
        {
            return true;
        }
        else if (BasicShields > 0)
        {
            BasicShields--;
        }
        else
        {
            Health -= damage;
            DamageTaken += damage;
        }
        return false;
    }

    public void Kill()
    {
        Kills++;
        DamageDealt += 10;
    }

    private int calcScore()
    {
        return Kills * 169 + PowerupCount * 23 + DamageDealt * 6;
    }

    private void AddPowerup(Powerup type)
    {
        switch(type)
        {
            case Powerup.LaserStrength:
                AddLaserStrength();
                break;
            case Powerup.SpreadShot:
                AddSpreadShot();
                break;
            case Powerup.FireRate:
                AddFireRate();
                break;
            case Powerup.BasicShield:
                AddBasicShield();
                break;
            case Powerup.MirrorShield:
                AddMirrorShield();
                break;
            case Powerup.Invincibility:
                AddInvincibility();
                break;
            case Powerup.Steam:
                AddSteam();
                break;
        }
        PowerupCount++;
    }

    private void AddLaserStrength()
    {
        if(LaserStrengthLevel < 5)
        {
            LaserStrengthLevel++;
        }
    }

    private void AddSpreadShot()
    {
        if (SpreadShotLevel < 5)
        {
            SpreadShotLevel++;
        }
    }

    private void AddFireRate()
    {
        if (FireRateLevel < 5)
        {
            FireRateLevel++;
        }
    }

    private void AddBasicShield()
    {
        BasicShields++;
    }

    private void AddMirrorShield()
    {
        mirrorTime = 120;
    }

    private void AddInvincibility()
    {
        invTime = 600;
    }

    private void AddSteam()
    {
        if (SteamLevel < 4)
        {
            SteamLevel++;
        }
    }
}
