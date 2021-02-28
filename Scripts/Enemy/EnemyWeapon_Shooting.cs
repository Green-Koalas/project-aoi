using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWeapon_Shooting : MonoBehaviour
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
    public GameObject manager;
    public AudioSource shotsfx;


    private void Start() {
        //manager = GameObject.FindGameObjectWithTag("Manager");
        //fireRate = (int) manager.GetComponent<WaveManager>().CalculateDifficultySpeed(baseFireRate);
    }

    private void Update() {
        if(timeUntilNextShot <= Time.time) {
            StartCoroutine (ShootIenum());
            timeUntilNextShot = fireRate + Time.time;
        }
    }

    IEnumerator ShootIenum() {
        yield return new WaitForSeconds(fireRate);
        Shoot();
    }

    public void Shoot() {
        GameObject bulletPrefab = Instantiate(bullet, bulletSpawnPoint.position, Quaternion.identity);
        Instantiate(muzzleFlash, bulletSpawnPoint.position, Quaternion.identity);
        if(recoil)  Recoil();
        bulletPrefab.GetComponent<EnemyBullet>().Owner = this.gameObject;
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
