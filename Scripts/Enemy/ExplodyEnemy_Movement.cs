using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplodyEnemy_Movement : MonoBehaviour
{
    public float moveSpeed;
    public float baseMoveSpeed;
    public float dodgeSpeed;
    public float dodgeCooldown;
    private float timeUntilNextDodge;
    public Vector3 dir;
    public GameObject manager;

    public Vector3 border;

    private void Start() {
        //manager = GameObject.FindGameObjectWithTag("Manager");
        //moveSpeed = (int) manager.GetComponent<WaveManager>().CalculateDifficultySpeed(baseMoveSpeed);
    }

    private void Update() {
        Move();
    }


    public void Move() {
        this.transform.position += dir * Time.deltaTime * moveSpeed;
    }

    public void Dodge() {
        if(timeUntilNextDodge < Time.time){
            DodgeMove();
        }
    }

    private void DodgeMove() {
        float luck = Random.Range(0f, 1f);
        Vector3 dodgeDir = new Vector3(0, 0, 0);
        if(luck >= 0.5f) {
            dodgeDir.x = 1;
        }
        else if (luck < 0.5f) {
            dodgeDir.x = -1;
        }

        if(Mathf.Abs(this.transform.position.x) > border.x) {
            if(this.transform.position.x < 0) {
                if(dodgeDir.x < 0) dodgeDir.x = 0;
            }
            else if(this.transform.position.x > 0){
                if(dodgeDir.x > 0) dodgeDir.x = 0;
            }
        }

        this.transform.position += dodgeDir * Time.deltaTime * dodgeSpeed;
        timeUntilNextDodge = dodgeCooldown + Time.time;
    }
}
