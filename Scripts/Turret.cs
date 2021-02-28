using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    public Transform target;
    public GameObject manager;
	private Rigidbody2D rb;
    public GameObject gun;
    public float rotateSpeed = 200f;

    private void Start() {
        manager = GameObject.FindGameObjectWithTag("Manager");
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update() {
        if(manager.GetComponent<WaveManager>().enemies.Count > 0)   target = GetClosestEnemy(manager.GetComponent<WaveManager>().enemies);
        else if (manager.GetComponent<WaveManager>().enemies.Count == 0) target = null;
        if(!target) {
            StopShooting();
            ResetDir();
        }
        if(target)  {
            RotateTowardsTarget();
            Shoot();
        }
    }

    private void ResetDir() {
		float rotateAmount = Vector3.Cross(new Vector3(0,1,0), transform.up).z;
		rb.angularVelocity = -rotateAmount * rotateSpeed;
    }

    private void RotateTowardsTarget() {
        if (manager.GetComponent<WaveManager>().enemies.Count == 0) return;
        Vector2 direction = (Vector2)target.position - rb.position;
		direction.Normalize();
		float rotateAmount = Vector3.Cross(direction, transform.up).z;
		rb.angularVelocity = -rotateAmount * rotateSpeed;
    }

    private void Shoot() {
        gun.GetComponent<Turret_Shooting>().allowedToShoot = true;
    }
    private void StopShooting() {
        gun.GetComponent<Turret_Shooting>().allowedToShoot = false;
    }

    Transform GetClosestEnemy(List<GameObject> target)
    {
        Transform tMin = null;
        float minDist = Mathf.Infinity;
        Vector3 currentPos = transform.position;
        foreach (GameObject t in target)
        {
            if(t == null) continue;
            float dist = Vector3.Distance(t.transform.position, currentPos);
            if (dist < minDist)
            {
                tMin = t.transform;
                minDist = dist;
            }
        }
        return tMin;
    }
}
