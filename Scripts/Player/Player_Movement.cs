using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Movement : MonoBehaviour
{
    public float moveSpeed;

    public Vector3 border;

    private void Update() {
        Move();
    }
    
    void Move() {
        Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0f);
        if(Mathf.Abs(this.transform.position.x) > border.x) {
            if(this.transform.position.x < 0) {
                if(movement.x < 0) movement.x = 0;
            }
            else if(this.transform.position.x > 0){
                if(movement.x > 0) movement.x = 0;
            }
        }

        if(Mathf.Abs(this.transform.position.y) > border.y) {
            if(this.transform.position.y < 0) {
                if(movement.y < 0) movement.y = 0;
            }
        }

        if(Mathf.Abs(this.transform.position.y) > border.z) {
            if(this.transform.position.y > 0) {
                if(movement.y > 0) movement.y = 0;
            }
        }

        this.transform.position += movement * Time.deltaTime * moveSpeed;
    }
}
