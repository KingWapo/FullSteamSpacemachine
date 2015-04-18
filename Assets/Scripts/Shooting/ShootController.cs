using UnityEngine;
using System.Collections;

public class ShootController : MonoBehaviour {

    private AimController aimController;

    public GameObject projectilePre;

	// Use this for initialization
	void Start () {
        aimController = GetComponent<AimController>();
	}

    public void StandardShoot() {
        Vector2 pos = aimController.GetPos();

        Ray ray = Camera.main.ScreenPointToRay(new Vector3(pos.x, pos.y, 0));
        RaycastHit hit;
        
        Physics.Raycast(ray, out hit, 500.0f);

        if (hit.collider != null) {
            if (hit.collider.tag == "Enemy") {
                print("hit name: " + hit.collider.name);
            }
            Vector3 target = hit.collider.transform.position;
            target.y = -target.y;

            Fire(target);
        } else {
            Fire(new Vector3(pos.x, -pos.y, transform.position.z + 100.0f));
        }
    }

    public void OculusShoot(Ray ray) {

    }

    private void Fire(Vector3 destination) {
        GameObject projectile = (GameObject)Instantiate(projectilePre);
        projectile.transform.position = transform.position;
        Vector3 direction = (destination - projectile.transform.position).normalized;
        projectile.transform.rotation = Quaternion.LookRotation(direction, Vector3.up);
        projectile.GetComponent<Projectile>().Speed = .5f;
    }
}
