using UnityEngine;
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

// Powerup info

    // Offensive
    public int LaserStrengthLevel = 1;
    public int SpreadShotLevel = 1;
    public int FireRateLevel = 1;
        
    // Defensive
    public int BasicShields = 0;
    public int MirrorShields = 0;
    public bool Invincible = false;

    // Full Steam
    public int SteamLevel = 0;

    // Debugging;
    public bool Debugging;

	// Use this for initialization
	void Start () {
	
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
	}

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Enemy")
        {
            col.gameObject.GetComponent<EnemyController>().OnDeath();
            TakeDamage(30);
        }
    }

    public bool TakeDamage(int damage)
    {
        if (Invincible)
        {

        }
        else if (MirrorShields > 0)
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
        MirrorShields++;
    }

    private void AddInvincibility()
    {
        Invincible = true;
    }

    private void AddSteam()
    {
        if (SteamLevel < 4)
        {
            SteamLevel++;
        }
    }
}
