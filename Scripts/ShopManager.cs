using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
    public int playerScore;
    public Text score;
    public GameObject shop;
    public GameObject player;
    public GameObject Base;
    public List<GameObject> turrets;

    private void Update() {
        score.text = playerScore.ToString();
    }

    public void Heal() {
        if(playerScore >= 200 && player.GetComponent<PlayerBase>().health < 5) {
            player.GetComponent<PlayerBase>().Heal(1);
            playerScore -= 200;
        }
    }

    public void HealBase() {
        if(playerScore >= 100 && Base.GetComponent<Base>().health < 10) {
            Base.GetComponent<Base>().Heal(1);
            playerScore -= 100;
        }
    }

    public void UpgradeFireRate() {
        if(playerScore >= 300) {
            var yes = false;
            foreach (Transform child in player.transform)
            {
                Debug.Log(child.gameObject);
                if(child.gameObject.GetComponent<Weapon_Shooting>()) {
                    if(child.GetComponent<Weapon_Shooting>().fireRate > 0.25f) {
                        child.GetComponent<Weapon_Shooting>().fireRate -= 0.05f;
                        yes = true;
                    }
                }
            }
        if(yes) playerScore -= 300;
        }
    }

    public void BuyTurret() {
        if(playerScore >= 500) {
            bool bought = false;
            foreach(GameObject turret in turrets) {
                if(!turret.activeSelf && !bought){
                    turret.SetActive(true);
                    bought = true;
                }
            }
            if(bought)  playerScore -= 500;
        }
    }

    public void UpgradeSpeed() {
        if(playerScore >= 300) {
            player.GetComponent<Player_Movement>().moveSpeed += 1;
            playerScore -= 300;
        }
    }
}
