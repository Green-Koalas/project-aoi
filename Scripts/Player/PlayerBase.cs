using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBase : MonoBehaviour
{
    public int health; 

    [Space]

    public Player_HealthBar healthBar;

    public AudioSource hitsfx;

    public GameObject manager;
    public GameObject explosion;
    public Animator playerAnim;

    public void GetHit(int damage) {
        health -= damage;
        if(health <= 0) Die();
        if(health < 0) health = 0;
        healthBar.ChangeSprite(health);
        hitsfx.Play();
        GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraShake>().ShakeCam();
    }

    public void Heal(int val) {
        if(health < 5)  health += val;
        healthBar.ChangeSprite(health);
    }

    void Die() {
        Debug.Log("You Died");
        Instantiate(explosion, this.transform.position, Quaternion.identity);
        manager.GetComponent<MenuManager>().lost = true;
        Destroy(gameObject);
    }


}
