using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicBullet : MonoBehaviour
{
    public GameObject Owner;
    public float bulletSpeed;
    public bool moveDown;

    public int damage;

    public float lifeTime;
    private float timeLeftToLive;

    private void Start() {
        timeLeftToLive = lifeTime + Time.time;
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<EnemyBase>()) {
            if(other != Owner ) Die();
            other.gameObject.GetComponent<EnemyBase>().GetHit(damage);
        }
    }

    private void Update() {
        Vector3 movement = new Vector3(0, 1, 0);
        if(moveDown)    this.transform.position += movement * Time.deltaTime * bulletSpeed * -1;
        else    this.transform.position += transform.up * Time.deltaTime * bulletSpeed;

        if(timeLeftToLive <= Time.time) {
            Die();
        }
    }

    public void Die() {
        Destroy(gameObject);
    }
}
