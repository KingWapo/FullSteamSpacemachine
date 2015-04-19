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

        //print(spawnValue);

	    if (spawnValue <= 0)
        {
            spawnEnemy();
            spawnValue = -(elapsedTime - 300) * (elapsedTime + 300) / 900;
            spawnValue = Mathf.Max(20.0f, spawnValue);
        }
        else
        {
            spawnValue = PlayerVariables.FullSteamSpacemachine ? (spawnValue - 1) / 4.0f : spawnValue - 1;
        }
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
