using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    [SerializeField] int healAmount;
    [SerializeField] GameObject healFX;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision != null && collision.tag == "Player")
        {
            PlayerHealthController.instance.HealPlayer(healAmount);
            if(healFX != null)
            {
                Instantiate(healFX, transform.position, Quaternion.identity);
            }
            Destroy(gameObject);
        }
    }



}
