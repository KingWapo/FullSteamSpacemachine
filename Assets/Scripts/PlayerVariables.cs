using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

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
    public GameObject NewHighScore;

    private bool highScore;

// Powerup info

    // Offensive
    public int LaserStrengthLevel = 1;
    public int SpreadShotLevel = 1;
    public int FireRateLevel = 1;
        
    // Defensive
	public GameObject BasicShieldObj;
	private float ShieldPulseTime = 0;
	public float MaxPulseTime;
	private bool PulseShield = false;
	private bool PulseShieldDown = false;

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
        if (Application.loadedLevelName == "Gameplay") {
            Cursor.visible = false;
			BasicShieldObj.SetActive(false);
			BasicShieldObj.GetComponent<Renderer>().material.SetFloat("BaseGlow", 2.0f);
            scoreText = GameObject.FindGameObjectWithTag("Score").GetComponent<Text>();

            TextParent = GameObject.FindGameObjectWithTag("FinalScore").gameObject;
            ScoreText = TextParent.transform.GetChild(0).gameObject.GetComponent<Text>();
            KillsText = GameObject.FindGameObjectWithTag("KillsText").GetComponent<Text>();
            DamageDealtText = GameObject.FindGameObjectWithTag("DamageDealt").GetComponent<Text>();
            DamageTakenText = GameObject.FindGameObjectWithTag("DamageTaken").GetComponent<Text>();
            PowerupsCollectedText = GameObject.FindGameObjectWithTag("PowerupsCollected").GetComponent<Text>();
            NewHighScore = GameObject.FindGameObjectWithTag("NewHighScore");
            NewHighScore.SetActive(false);

            TextParent.SetActive(false);
        }
	}
	
	// Update is called once per frame
	void Update () {

        if (EndGame.End)
            onDeath();

        if (!dead)
        {
			if(PulseShield) {
				ModulateShield();
			}
			if(PulseShieldDown) {
				ModulateShieldDown();
			}
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

            if (Application.loadedLevelName == "Gameplay") {
                scoreText.text = "Score: " + previousScore;
            }

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

	private void ModulateShield() {
		if(ShieldPulseTime < .5) {
			ShieldPulseTime += Time.deltaTime;
			float glow = (Mathf.Sin(ShieldPulseTime*Mathf.PI*2))+2;
			BasicShieldObj.GetComponent<Renderer>().material.SetFloat("_BaseGlow", glow);
		} else {
			PulseShield = false;
		}
	}

	private void ModulateShieldDown() {
		if(ShieldPulseTime < 2) {
			ShieldPulseTime += Time.deltaTime;
			float glow = (Mathf.Sin(ShieldPulseTime*Mathf.PI*3+(Mathf.PI/2)))+1f;
			BasicShieldObj.GetComponent<Renderer>().material.SetFloat("_BaseGlow", glow);
		} else {
			PulseShieldDown = false;
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
            setUI();
        }
    }

    private void setUI()
    {
        List<int> deseScores = GameManager.GetHighScores();
        if (calcScore() > deseScores[deseScores.Count - 1])
        {
            NewHighScore.SetActive(true);
            highScore = true;
            setHighScoreEntry();
        }
        else
        {
            setRegUI();
        }
    }

    public void setRegUI()
    {
        NewHighScore.SetActive(false);
        TextParent.SetActive(true);
        scoreText.text = "";
        ScoreText.text = "" + calcScore();
        KillsText.text = "" + Kills;
        DamageDealtText.text = "" + DamageDealt;
        DamageTakenText.text = "" + DamageTaken;
        PowerupsCollectedText.text = "" + PowerupCount;
    }

    private void setHighScoreEntry()
    {
        
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
			if(BasicShields == 0) {
				BasicShieldObj.SetActive(false);
			}
			ShieldPulseTime = 0;
			PulseShield = false;
			PulseShieldDown = true;
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

    public int calcScore()
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
            case Powerup.Health:
                AddHealth();
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
		if(BasicShields == 1) {
			BasicShieldObj.SetActive(true);
		} else {
			ShieldPulseTime = 0;
			PulseShieldDown = false;
			PulseShield = true;
		}
    }

    private void AddMirrorShield()
    {
        mirrorTime = 120;
    }

    private void AddInvincibility()
    {
        invTime = 600;
    }

    private void AddHealth()
    {
        float percent = Random.Range(0.0f, 100.0f);
        if (percent <= 50)
        {
            Health += 10;
        }
        else if (percent <= 80)
        {
            Health += 20;
        }
        else if (percent <= 99)
        {
            Health += 30;
        }
        else
        {
            Health += 100;
        }
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
