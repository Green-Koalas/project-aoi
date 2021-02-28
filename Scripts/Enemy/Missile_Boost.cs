using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile_Boost : MonoBehaviour
{
    public float deathDelay;
    
    public void DieWithDelay() {
        Invoke("Die", deathDelay);
    }

    public void Die() {
        Destroy(gameObject);
    }
}
