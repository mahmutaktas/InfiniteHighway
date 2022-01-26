using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            FindObjectOfType<AudioManager>().PlaySound("CoinPickup");
            PlayerManager.numberOfCoins += 1;
            Destroy(gameObject);
        }
    }
}
