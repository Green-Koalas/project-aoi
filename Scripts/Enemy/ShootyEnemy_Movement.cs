using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootyEnemy_Movement : MonoBehaviour
{
    public float moveSpeed;
    public float baseMoveSpeed;
    public Vector3 dir;
    public GameObject manager;
    public Vector3 border;
    public bool secondPhase;

    private void Start() {
        //manager = GameObject.FindGameObjectWithTag("Manager");
        //moveSpeed = (int) manager.GetComponent<WaveManager>().CalculateDifficultySpeed(baseMoveSpeed);
    }

    private void Update() {
        if(secondPhase) SecondPhase();
        else if(!secondPhase)   Move();
    }

    private void SecondPhase() {
        if(Mathf.Abs(this.transform.position.x) > border.x) {
            if(this.transform.position.x < 0) {
                if(dir.x < 0) dir.x = 1;
            }
            else if(this.transform.position.x > 0){
                if(dir.x > 0) dir.x = -1;
            }
        }
        Move();
    }

    public void Move() {
        this.transform.position += dir * Time.deltaTime * moveSpeed;
    }

}
