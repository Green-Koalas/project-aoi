using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public Animator camAnim;

    public void ShakeCam() {
        camAnim.SetTrigger("Shake");
    }
}
