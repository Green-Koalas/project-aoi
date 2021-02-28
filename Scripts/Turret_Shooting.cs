using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret_Shooting : MonoBehaviour
{
    public GameObject bullet;
    public GameObject muzzleFlash;
    public Transform bulletSpawnPoint;
    public float fireRate;
    public float baseFireRate;
    private float timeUntilNextShot;
    public bool recoil;
    public float recoilAmount;
    public float recoilDelay;
    public bool allowedToShoot;
    public GameObject turretBase;
    public GameObject manager;
    public AudioSource shotsfx;


    private void Start() {
        //manager = GameObject.FindGameObjectWithTag("Manager");
        //fireRate = (int) manager.GetComponent<WaveManager>().CalculateDifficultySpeed(baseFireRate);
    }

    private void Update() {
        if(timeUntilNextShot <= Time.time && allowedToShoot) {
            StartCoroutine (ShootIenum());
            timeUntilNextShot = fireRate + Time.time;
        }
    }

    IEnumerator ShootIenum() {
        yield return new WaitForSeconds(0.5f);
        Shoot();
    }

    public void Shoot() {
        GameObject bulletPrefab = Instantiate(bullet, bulletSpawnPoint.position, Quaternion.Euler(turretBase.transform.eulerAngles));
        Instantiate(muzzleFlash, bulletSpawnPoint.position, Quaternion.Euler(turretBase.transform.eulerAngles));
        if(recoil)  Recoil();
        bulletPrefab.GetComponent<BasicBullet>().Owner = this.gameObject;
        shotsfx.Play();
    }

    void Recoil() {
        Vector3 pos = this.transform.position;
        pos.y -= recoilAmount;
        this.transform.position = pos;
        Invoke("RecoilReset", recoilDelay);
    }

    void RecoilReset() {
        Vector3 pos = this.transform.position;
        pos.y += recoilAmount;
        this.transform.position = pos;
    }
}
