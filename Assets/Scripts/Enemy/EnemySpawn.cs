using UnityEngine;
using System.Collections;

public class EnemySpawn : MonoBehaviour {

    public static float elapsedTime;

    public GameObject EnemyPrefab;

    private float spawnValue = 100;

	// Use this for initialization
	void Start () {
        elapsedTime = 0;
	}
	
	// Update is called once per frame
	void Update () {

        elapsedTime += Time.deltaTime;

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
        float num = Random.Range(0, (int)EnemyController.Path.Max);
        enemy.GetComponent<EnemyController>().SetPath((EnemyController.Path)num);
        num = Random.Range(0, (int)EnemyController.Attack.Max);
        enemy.GetComponent<EnemyController>().attack = (EnemyController.Attack)num;
    }

    public static float PowerupSpawnFunc()
    {
        float y = (elapsedTime - 300) * (elapsedTime - 300) / 1800.0f;
        y = Mathf.Max(5.0f, y);
        return y;
    }
}
