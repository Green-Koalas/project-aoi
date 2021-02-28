using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : MonoBehaviour
{
    public int health;
    public int baseHealth;
    public int damage;
    public int points;
    public GameObject manager;
    public bool boss;
    public bool secondPhase;
    public GameObject explosion;
    public AudioSource hitsfx;

    private void Start() {
        manager = GameObject.FindGameObjectWithTag("Manager");
        //health = (int) manager.GetComponent<WaveManager>().CalculateDifficultyHealth(baseHealth);
    }

    public void GetHit(int damage) {
        health -= damage;
        if(health <= 0) {
            if(!boss)   Die();
            if(boss && secondPhase) Die();
            else if(boss && !secondPhase) {
                SecondPhaseStart();
            }
        }
        hitsfx.Play();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.GetComponent<PlayerBase>()) {
            other.GetComponent<PlayerBase>().GetHit(damage);
            Die();
        }
        if(other.GetComponent<Base>()) {
            other.GetComponent<Base>().GetHit(damage);
            DieBase();
        }
    }

    void Die() {
        Destroy(gameObject);
        Instantiate(explosion, this.transform.position, Quaternion.identity);
        manager.GetComponent<ShopManager>().playerScore += points;
    }

    void DieBase() {
        Destroy(gameObject);
        Instantiate(explosion, this.transform.position, Quaternion.identity);
    }

    public void SecondPhaseStart() {
        Instantiate(explosion, this.transform.position, Quaternion.identity);
        health = baseHealth;
        Destroy(this.GetComponentInChildren<BossPlate>().gameObject);
        secondPhase = true;
        Debug.Log("SECOND PHASE");
        this.GetComponent<ShootyEnemy_Movement>().secondPhase = true;
        this.GetComponent<BossEnemy_Shooting>().secondPhase = true;
        this.GetComponent<ShootyEnemy_Movement>().dir = new Vector3(1, 0, 0);;
    }

    public void Stop() {
        Invoke("ActuallyStop", 2.1f);
    }

    private void ActuallyStop() {
        this.GetComponent<ShootyEnemy_Movement>().dir = new Vector3(0, 0, 0);
    }
}
