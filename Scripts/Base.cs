using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Base : MonoBehaviour
{
    public int health;
    public Player_HealthBar baseBar;
    public GameObject manager;

    public void GetHit(int damage) {
        health -= damage;
        if(health <= 0) Die();
        GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraShake>().ShakeCam();
        baseBar.ChangeSprite(health);
    }

    public void Heal(int val) {
        if(health < 10)  health += val;
        baseBar.ChangeSprite(health);
    }

    void Die() {
        Debug.Log("You Lost");
        manager.GetComponent<MenuManager>().lost = true;
    }
}
