using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{

    [SerializeField] float bulletSpeed;
    [SerializeField] int bulletPower;
    [SerializeField] Rigidbody2D rigidbodyREF;
    [SerializeField] public Vector2 moveDir;
    [SerializeField] public GameObject impactFX;


    // Start is called before the first frame update
    void Start()
    {
       Destroy(gameObject, 2.5f);
    }

    // Update is called once per frame
    void Update()
    {
        MoveForward();
    }

    void MoveForward()
    {
    
        rigidbodyREF.velocity = moveDir * bulletSpeed;

    }


    //Destroy the obj

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other != null)
        {

            if (other.CompareTag("Enemy"))
            {
                EnemyHealthController enemyREF = other.GetComponent<EnemyHealthController>();
                enemyREF.TakeDamage(bulletPower);
            }

            Instantiate(impactFX, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }


    private void OnBecameInvisible()
    {
       Destroy(gameObject);
    }


}
