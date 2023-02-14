using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class EnemyFlyingController : MonoBehaviour
{

    [SerializeField] float rangeToStartChase;
    [SerializeField] float moveSpeed;
    [SerializeField] float turnSpeed;
    [SerializeField] Animator amim;


    private Transform playerTransform;
    private bool isChasing;
    private Vector3 InitialPos;
    private Quaternion InitialRot;
    private Rigidbody2D rigidbody2;

    // Start is called before the first frame update
    void Start()
    {
        InitialPos = transform.position;
        InitialRot = transform.rotation;
        playerTransform = FindObjectOfType<PlayerController>().transform;
        rigidbody2= GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        ChasePlayer();
    }

    public void ChasePlayer()
    {

       // amim.SetFloat("Speed_Param" ,Mathf.Abs(rigidbody2.velocity.x + rigidbody2.velocity.y) * 10);

        if (!isChasing)
        {

            if (Vector3.Distance(transform.position, playerTransform.position) < rangeToStartChase)
            {
                isChasing= true;
            }
            else
            {
                isChasing = false;
                GoToInitialPosition();
            }
        }
        else
        {
            if (playerTransform.gameObject.activeSelf)
            {
                Vector3 direction = transform.position - playerTransform.position;
                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                Quaternion targetRotarion = Quaternion.AngleAxis(angle, Vector3.forward);

                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotarion, turnSpeed * Time.deltaTime);

                transform.position = Vector3.MoveTowards(transform.position, playerTransform.position, moveSpeed * Time.deltaTime);
                //transform.position += -transform.right * moveSpeed * Time.deltaTime;

            }
            else
            {
                isChasing = false;
                GoToInitialPosition();
            }
        }
    }

    void GoToInitialPosition()
    {
        transform.position = Vector3.MoveTowards(transform.position, InitialPos, moveSpeed * Time.deltaTime);
        transform.rotation = Quaternion.Slerp(transform.rotation, InitialRot, turnSpeed * Time.deltaTime);
    }

}
