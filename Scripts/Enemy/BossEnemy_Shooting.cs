using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEnemy_Shooting : MonoBehaviour
{
    public GameObject missile;
    public float missileActivationDelay;
    public GameObject missilePrefab;
    public float fireRate;
    private float timeUntilNextShot;
    public bool secondPhase;

    private void Start() {
        
    }

    private void Update() {
        if(!missile && !secondPhase) {
            Vector3 missileSpawnLoc = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z);
            missileSpawnLoc.y -= 0.9f;
            GameObject missileVar = Instantiate(missilePrefab, missileSpawnLoc, Quaternion.identity);
            missile = missileVar;
            missileVar.GetComponent<Missile>().Owner = this.gameObject;
            missile.transform.parent = this.transform;
            Invoke("Shoot", missileActivationDelay);
        }
        else if(secondPhase){
            if(timeUntilNextShot <= Time.time) {
                StartCoroutine (ShootIenum());
                timeUntilNextShot = fireRate + Time.time;
            }
        }
    }

    IEnumerator ShootIenum() {
        yield return new WaitForSeconds(fireRate);
        Vector3 missileSpawnLoc = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z);
        missileSpawnLoc.y -= 0.9f;
        GameObject missileVar = Instantiate(missilePrefab, missileSpawnLoc, Quaternion.identity);
        missile = missileVar;
        missileVar.GetComponent<Missile>().Owner = this.gameObject;
        missile.transform.parent = this.transform;
        InvokeShoot();
    }

    void InvokeShoot() {
        Invoke("Shoot", missileActivationDelay);
    }

    void Shoot() {
        missile.GetComponent<Missile>().shot = true;
    }
}
