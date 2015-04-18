using UnityEngine;
using System.Collections;

public class Powerups : MonoBehaviour {

    public static GameObject LaserStrengthPrefab;
    public static GameObject SpreadShotPrefab;
    public static GameObject FireRatePrefab;
    public static GameObject BasicShieldPrefab;
    public static GameObject MirrorShieldPrefab;
    public static GameObject InvincibilityPrefab;
    public static GameObject SteamPrefab;

    public GameObject LaserStrPrefab;
    public GameObject SprdShotPrefab;
    public GameObject FireRtPrefab;
    public GameObject BasicShldPrefab;
    public GameObject MirrorShldPrefab;
    public GameObject InvPrefab;
    public GameObject StmPrefab;

	// Use this for initialization
	void Start () {
        LaserStrengthPrefab = LaserStrPrefab;
        SpreadShotPrefab = SprdShotPrefab;
        FireRatePrefab = FireRtPrefab;
        BasicShieldPrefab = BasicShldPrefab;
        MirrorShieldPrefab = MirrorShldPrefab;
        InvincibilityPrefab = InvPrefab;
        SteamPrefab = StmPrefab;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
