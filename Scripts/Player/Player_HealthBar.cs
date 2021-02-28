using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player_HealthBar : MonoBehaviour
{
    public List<Sprite> sprites;

    public void ChangeSprite(int imgNr)
    {
        switch (imgNr)
        {
            case 0:
                GetComponent<Image>().sprite = sprites[imgNr];
                break;
            case 1:
                GetComponent<Image>().sprite = sprites[imgNr];
                break;
            case 2:
                GetComponent<Image>().sprite = sprites[imgNr];
                break;
            case 3:
                GetComponent<Image>().sprite = sprites[imgNr];
                break;
            case 4:
                GetComponent<Image>().sprite = sprites[imgNr];
                break;
            case 5:
                GetComponent<Image>().sprite = sprites[imgNr];
                break;
            case 6:
                GetComponent<Image>().sprite = sprites[imgNr];
                break;
            case 7:
                GetComponent<Image>().sprite = sprites[imgNr];
                break;
            case 8:
                GetComponent<Image>().sprite = sprites[imgNr];
                break;
            case 9:
                GetComponent<Image>().sprite = sprites[imgNr];
                break;
            case 10:
                GetComponent<Image>().sprite = sprites[imgNr];
                break;
            case 11:
                GetComponent<Image>().sprite = sprites[imgNr];
                break;
            default:
                Debug.Log("Error");
                break;
        }
    }
}
