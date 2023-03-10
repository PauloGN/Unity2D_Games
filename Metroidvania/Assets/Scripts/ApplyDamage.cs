using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApplyDamage : MonoBehaviour
{

    [SerializeField] int damagePower = 1;
    [SerializeField] GameObject explosiveFX;
    [SerializeField] bool destroyOnDamage = false;


    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            DealDamage();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            DealDamage();
        }
    }


    private void DealDamage()
    {
        PlayerHealthController.instance.DamagePlayer(damagePower);

        if (destroyOnDamage)
        {
            if(explosiveFX != null)
            {
                Instantiate(explosiveFX, transform.position, transform.rotation);
            }

            Destroy(gameObject);
        }
    }


}
