using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyPatroller : MonoBehaviour
{
    //properties
    [SerializeField] Transform[] patrolPoints = null;
    [SerializeField] float moveSpeed;
    [SerializeField] float jumpForce;
    [SerializeField] float waitAtPoint;
    [SerializeField] Rigidbody2D rigidbodyREF;

    //Controllers
    private float waitcounter;
    private int currentPointIndex;
    private Vector2 moveForward;
    private Transform[] poitsDestroyerArray;

    //Amimation Section
    [SerializeField] Animator amim;


    // Start is called before the first frame update
    void Start()
    {
        //define how long the enemy needs to wait before go to the next point
        waitcounter = waitAtPoint;

        //deatach points from the enemy body
        DeatachButKeepOntrack();
    }

    // Update is called once per frame
    void Update()
    {

        EnemyMovement();

    }


    void EnemyMovement()
    {

        //Enemy is far from the point to patrol
        if(Mathf.Abs(transform.position.x - patrolPoints[currentPointIndex].position.x) > .2f)
        {
            //walking from left to right
            if (transform.position.x < patrolPoints[currentPointIndex].position.x)
            {
                moveForward = new Vector2(moveSpeed, rigidbodyREF.velocity.y);
                rigidbodyREF.velocity = moveForward;
                transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);
            }
            else//walking from right to left
            {
                moveForward = new Vector2(-moveSpeed, rigidbodyREF.velocity.y);
                rigidbodyREF.velocity = moveForward;
                transform.localScale = Vector3.one;
            }

            //Jump
            if (transform.position.y < patrolPoints[currentPointIndex].position.y && rigidbodyREF.position.y < 1.0f)
            {
                rigidbodyREF.velocity = new Vector2(rigidbodyREF.velocity.x, jumpForce);
            }



        }
        else//get at the point
        {
            rigidbodyREF.velocity = new Vector2(0.0f, rigidbodyREF.velocity.y);
           
            //wait for a moment before change direction
            waitcounter -= Time.deltaTime;
            if (waitcounter <= 0.0f)
            {
                waitcounter= waitAtPoint;

                //go to next point
                currentPointIndex++;
                if(currentPointIndex >= patrolPoints.Length)
                {
                    currentPointIndex= 0;
                }

            }
        }//else

        //animation
        amim.SetFloat("Speed_Param", Mathf.Abs(rigidbodyREF.velocity.x));
    }


    void DeatachButKeepOntrack()
    {

        int i = 0;

        poitsDestroyerArray = new Transform[patrolPoints.Length];

        foreach (var point in patrolPoints)
        {
            point.SetParent(null);

            poitsDestroyerArray[i] = point;
            i++;
        }
    }

    //destroy deatached points if needed
    void DestroyDetached()
    {
        foreach (var item in poitsDestroyerArray)
        {
            Destroy(item.gameObject);
        }
    }


}
