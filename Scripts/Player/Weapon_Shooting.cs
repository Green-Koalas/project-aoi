using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon_Shooting : MonoBehaviour
{
    public GameObject bullet;
    public GameObject muzzleFlash;
    public Transform bulletSpawnPoint;
    public float fireRate;
    private float timeUntilNextShot;
    public bool recoil;
    public float recoilAmount;
    public float recoilDelay;

    public AudioSource shootingsfx;

    private void Update() {
        if(Input.GetKey("space") && timeUntilNextShot <= Time.time) {
            Shoot();
            timeUntilNextShot = fireRate + Time.time;
        }
    }

    void Shoot() {
        GameObject bulletPrefab = Instantiate(bullet, bulletSpawnPoint.position, Quaternion.identity);
        Instantiate(muzzleFlash, bulletSpawnPoint.position, Quaternion.identity);
        if(recoil)  Recoil();
        bulletPrefab.GetComponent<BasicBullet>().Owner = this.gameObject;
        shootingsfx.Play();
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
