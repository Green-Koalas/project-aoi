using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossPlate : MonoBehaviour
{
    public float speed;

    private void Start() {
        
    } 

    public void FlyOff() {
        Vector3 direction = new Vector3(0,0,Random.Range(-1f, 1f));
        direction.Normalize();
        Vector3 newVelocity = speed * direction * Time.deltaTime;
        transform.GetComponent<Rigidbody2D>().velocity = newVelocity;
    }
}
