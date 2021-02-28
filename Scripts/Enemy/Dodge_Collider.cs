using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dodge_Collider : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.GetComponent<BasicBullet>()) {
            this.transform.parent.gameObject.GetComponent<ExplodyEnemy_Movement>().Dodge();
        }
    }
}
