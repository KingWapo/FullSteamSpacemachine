using UnityEngine;
using System.Collections;

public class ShootController : MonoBehaviour {

    private AimController aimController;

    private Transform target;

    public GameObject projectilePre;

    protected bool Debugging = false;

    // Keep track of powerups
    private PlayerVariables pVariables;

    // Spreadshot powerup
    private float spreadAngle = 1.0f;

    // Firerate powerup
    private int cooldownMax = 18;
    private int cooldown = 0;

    // Debugging
    protected Ray ray;
    protected RaycastHit hit;

	// Use this for initialization
	void Start () {
        aimController = GetComponent<AimController>();
        pVariables = GetComponent<PlayerVariables>();
	}

    public void StandardShoot() {
        Vector2 pos = aimController.GetPos();

        ray = Camera.main.ScreenPointToRay(new Vector3(pos.x, pos.y, 0));
        ray.direction = new Vector3(ray.direction.x, ray.direction.y * -1, ray.direction.z);
        //RaycastHit hit;
        
        Physics.Raycast(ray, out hit, 500.0f);


        if (hit.collider != null) {
            target = hit.collider.transform;
            Fire();
        } else {
            GameObject temp = new GameObject();
            temp.transform.position = ray.origin + ray.direction * 100;
            target = temp.transform;
            Fire();
            Destroy(temp);
        }
    }

    public void OculusShoot(Ray ray) {

    }

    private void Fire() {
        if (cooldown <= 0)
        {
            produceProjectile();

            switch (pVariables.SpreadShotLevel)
            {
                case 5:
                    GameObject proj7 = produceProjectile();
                    proj7.transform.Rotate(transform.up, spreadAngle);
                    proj7.transform.Rotate(transform.right, -spreadAngle);
                    GameObject proj8 = produceProjectile();
                    proj8.transform.Rotate(transform.up, -spreadAngle);
                    proj8.transform.Rotate(transform.right, spreadAngle);
                    goto case 4;
                case 4:
                    GameObject proj5 = produceProjectile();
                    proj5.transform.Rotate(transform.up, spreadAngle);
                    proj5.transform.Rotate(transform.right, spreadAngle);
                    GameObject proj6 = produceProjectile();
                    proj6.transform.Rotate(transform.up, -spreadAngle);
                    proj6.transform.Rotate(transform.right, -spreadAngle);
                    goto case 3;
                case 3:
                    GameObject proj3 = produceProjectile();
                    proj3.transform.Rotate(transform.right, spreadAngle);
                    GameObject proj4 = produceProjectile();
                    proj4.transform.Rotate(transform.right, -spreadAngle);
                    goto case 2;
                case 2:
                    GameObject proj1 = produceProjectile();
                    proj1.transform.Rotate(transform.up, spreadAngle);
                    GameObject proj2 = produceProjectile();
                    proj2.transform.Rotate(transform.up, -spreadAngle);
                    break;
            }

            cooldown = cooldownMax - 3 * pVariables.FireRateLevel;
        }
        else
        {
            cooldown--;
        }
    }

    private GameObject produceProjectile()
    {
        GameObject projectile = (GameObject)Instantiate(projectilePre);
        projectile.transform.position = transform.position;
        projectile.transform.LookAt(target);
        projectile.GetComponent<Projectile>().Speed = 1f;
        projectile.GetComponent<Projectile>().PlayerShot = true;
        return projectile;
    }
}
