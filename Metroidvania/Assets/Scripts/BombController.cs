using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombController : MonoBehaviour
{

    [SerializeField] float timeToExplode = .5f;
    [SerializeField] GameObject explosion;

    [SerializeField] float blastRange;
    [SerializeField] LayerMask destructibleLayer;
    [SerializeField] int bombPower;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
     
        timeToExplode-= Time.deltaTime;

        if (timeToExplode < 0)
        {
            if (explosion)
            {
                Instantiate(explosion, transform.position, transform.rotation);
            }

            Destroy(gameObject);


          Collider2D[] objsToDamage =  Physics2D.OverlapCircleAll(transform.position, blastRange, destructibleLayer);

            if (objsToDamage.Length > 0)
            {
                foreach (var obj in objsToDamage)
                {
                    if (obj.CompareTag("Enemy"))
                    {
                        obj.GetComponent<EnemyHealthController>().TakeDamage(bombPower);
                    }
                    else
                    {
                        Destroy(obj.gameObject);
                    }
                }
            }
        }

    }
}
