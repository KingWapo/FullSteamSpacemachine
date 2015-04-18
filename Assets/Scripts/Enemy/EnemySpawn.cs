using UnityEngine;
using System.Collections;

public class EnemySpawn : MonoBehaviour {

    public GameObject EnemyPrefab;

    private float spawnValue = 100;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        
	    if (spawnSystem() <= 0)
        {
            spawnEnemy();
            spawnValue = 100;
        }
	}

    // Returns a value based off of an equation to
    // determine if it is time to spawn an enemy or not
    private float spawnSystem()
    {
        spawnValue--;

        return spawnValue;
    }

    private void spawnEnemy()
    {
        GameObject enemy = (GameObject)Instantiate(EnemyPrefab);
        Vector3 pos = transform.position;
        float x = Random.Range(-20.0f, 20.0f);
        float y = Random.Range(-20.0f, 20.0f);
        float z = Random.Range(-20.0f, 20.0f);
        pos += new Vector3(x, y, z);
        enemy.transform.position = pos;
        enemy.transform.rotation = transform.rotation;
        enemy.GetComponent<EnemyController>().SetPath(EnemyController.Path.Spiral);
        enemy.GetComponent<EnemyController>().attack = EnemyController.Attack.SlowAim;
    }
}
