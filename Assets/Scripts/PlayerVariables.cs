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
    Steam,
    Health
}

public class PlayerVariables : MonoBehaviour {

    public ParticleSystem Explosion;
    private bool dead;

// Score variables

    public int Health;
    public int Kills;
    public int PowerupCount;
    public int DamageTaken;
    public int DamageDealt;

    private int bonusDamage = 0;

    private Text scoreText;
    private int previousScore = 0;

// Score text
    public GameObject TextParent;
    public Text ScoreText;
    public Text KillsText;
    public Text DamageDealtText;
    public Text DamageTakenText;
    public Text PowerupsCollectedText;

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
    public GameObject FullSteam;
    public static bool FullSteamSpacemachine;
    public int SteamLevel = 0;
    private int steamTime = 0;

    // Debugging;
    public bool Debugging;

	// Use this for initialization
	void Start () {
        Cursor.visible = false;
        scoreText = GameObject.FindGameObjectWithTag("Score").GetComponent<Text>();

        TextParent = GameObject.FindGameObjectWithTag("FinalScore").gameObject;
        ScoreText = TextParent.transform.GetChild(0).gameObject.GetComponent<Text>();
        KillsText = GameObject.FindGameObjectWithTag("KillsText").GetComponent<Text>();
        DamageDealtText = GameObject.FindGameObjectWithTag("DamageDealt").GetComponent<Text>();
        DamageTakenText = GameObject.FindGameObjectWithTag("DamageTaken").GetComponent<Text>();
        PowerupsCollectedText = GameObject.FindGameObjectWithTag("PowerupsCollected").GetComponent<Text>();

        TextParent.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {

        if (EndGame.End)
            onDeath();

        if (!dead)
        {
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

            if (Health <= 0)
            {
                EndGame.End = true;
            }

            if (steamTime > 0)
            {
                steamTime--;
                if (steamTime <= 0)
                {
                    FullSteam.SetActive(false);
                    FullSteamSpacemachine = false;
                    print("Full Steam Exited");
                    transform.GetChild(0).gameObject.SetActive(true);
                }
            }
        }
	}

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Enemy")
        {
            col.gameObject.GetComponent<EnemyController>().OnDeath(false);
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

    private void onDeath()
    {
        if (!dead)
        {
            dead = true;
            transform.GetChild(0).gameObject.SetActive(false);
            Explosion.Play();
            TextParent.SetActive(true);
            scoreText.text = "";
            ScoreText.text = "" + calcScore();
            KillsText.text = "" + Kills;
            DamageDealtText.text = "" + DamageDealt;
            DamageTakenText.text = "" + DamageTaken;
            PowerupsCollectedText.text = "" + PowerupCount;
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
        if (steamTime > 0) return false;

        DamageTaken += damage;
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
        }
        return false;
    }

    public void Kill()
    {
        Kills++;
        DamageDealt += 10;
    }

    public void Bonus(int bonus)
    {
        bonusDamage += bonus;
    }

    private int calcScore()
    {
        return Kills * 169 + PowerupCount * 23 + DamageDealt * 6 + bonusDamage * 56;
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
        SteamLevel++;
        if (SteamLevel == 4)
        {
            steamTime = 1200;
            SteamLevel = 0;
            FullSteam.SetActive(true);
            FullSteamSpacemachine = true;
            print("Full Steam Activated!!");
            transform.GetChild(0).gameObject.SetActive(false);
        }
    }
}
