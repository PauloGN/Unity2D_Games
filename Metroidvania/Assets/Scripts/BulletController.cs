using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{

    [SerializeField] float bulletSpeed;
    [SerializeField] Rigidbody2D rigidbodyREF;
    [SerializeField] public Vector2 moveDir;


    // Start is called before the first frame update
    void Start()
    {
        
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision != null)
        {
            Destroy(gameObject);
        }
    }


    private void OnBecameInvisible()
    {
       Destroy(gameObject);
    }


}
