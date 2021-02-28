using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlashyText : MonoBehaviour
{
       public float timer;
       void Update()
       {
        timer = timer + Time.deltaTime;
         if(timer >= 0.5)
        {
             GetComponent<Text>().enabled = false;
        }
        if(timer >= 1)
        {
             GetComponent<Text>().enabled = true;
             timer = 0;
        }
       }
}
