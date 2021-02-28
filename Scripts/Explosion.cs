using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    public AudioSource explosionsfx;

    private void Start() {
        Invoke("Die", 0.5f);
        GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraShake>().ShakeCam();
        explosionsfx.Play();
    }

    void Die() {
        Destroy(gameObject);
    }
}
