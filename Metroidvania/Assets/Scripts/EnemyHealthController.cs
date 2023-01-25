using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthController : MonoBehaviour
{

    [SerializeField] int totalHealth = 3;
    [SerializeField] GameObject deathFX;


    public void TakeDamage(int dmg)
    {
        totalHealth-= dmg;

        if (totalHealth <=0)
        {
            if(deathFX != null)
            {
                Instantiate(deathFX, transform.position, transform.rotation);
            }

            Destroy(gameObject);
        }
    }

}
