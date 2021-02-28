using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Missile : MonoBehaviour
{
    public GameObject Owner;
    public float speed;
    public float baseSpeed;
    public int damage;
    public GameObject target;
    public bool shot;
    public ParticleSystem missileBoost;
    public float boostDelay;
    public GameObject explosion;
    public GameObject manager;


	public float rotateSpeed = 200f;

	private Rigidbody2D rb;

    private void Start() {
        //manager = GameObject.FindGameObjectWithTag("Manager");
        target = GameObject.FindGameObjectWithTag("Player");
        rb = GetComponent<Rigidbody2D>();
        //speed = (int) manager.GetComponent<WaveManager>().CalculateDifficultySpeed(baseSpeed);
    }

	void FixedUpdate () {
        if(shot) {
            MoveAndRotateTowardsPlayer();
            this.transform.parent = null;
            Invoke("StartMissileBoost", boostDelay);
        }
	}

    private void MoveAndRotateTowardsPlayer() {
		Vector2 direction = (Vector2)target.transform.position - rb.position;
		direction.Normalize();
		float rotateAmount = Vector3.Cross(direction, transform.up).z;
		rb.angularVelocity = rotateAmount * rotateSpeed;
		rb.velocity = -transform.up * speed;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<PlayerBase>()) {
            if(other != Owner ) Die();
            other.gameObject.GetComponent<PlayerBase>().GetHit(damage);
        }

        if (other.gameObject.GetComponent<BasicBullet>()) {
            if(other != Owner ) Die();
            other.gameObject.GetComponent<BasicBullet>().Die();
        }

        if (other.gameObject.GetComponent<Missile>()) {
            if(other != Owner ) Die();
            other.gameObject.GetComponent<Missile>().Die();
        }
    }

    private void StartMissileBoost() {
        var em = missileBoost.emission;
        em.enabled = true;
    }

    void Die() {
        missileBoost.Stop();
        missileBoost.transform.parent = null;
        Vector3 scaleVr = new Vector3(1, 1, 1);
        missileBoost.gameObject.transform.localScale = scaleVr;
        missileBoost.GetComponent<Missile_Boost>().DieWithDelay();
        GameObject explosionVar = Instantiate(explosion, this.transform.position, Quaternion.identity);
        explosionVar.transform.localScale = new Vector3(2,2,1);
        Destroy(gameObject);
    }
}
