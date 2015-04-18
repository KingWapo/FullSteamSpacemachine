using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour {

    private float speed;

	// Use this for initialization
	void Start () {
        speed = Random.Range(0.01f, 0.05f);
	}
	
	// Update is called once per frame
	void Update () {
        transform.Translate(Vector3.forward * speed);
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Deathbox")
        {
            Destroy(this.gameObject);
        }
    }

    private void fire()
    {

    }
}
