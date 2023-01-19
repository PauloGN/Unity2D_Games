using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class PlayerController : MonoBehaviour
{

    public Rigidbody2D rigidbodyREF;
    private bool isOnGround;

    //Player properties
    [SerializeField] private float moveSpeed;
    [SerializeField] private float jumpForce;

    //Ground and Jump control
    [SerializeField] private Transform groundPoint;
    [SerializeField] private LayerMask layerGround;

    //Animation Section
    [SerializeField] private Animator amim;

    // Start is called before the first frame update
    void Start()
    {
        
        Assert.IsNotNull(rigidbodyREF, "Rigidbody2D is null");



    }

    // Update is called once per frame
    void Update()
    {

        CharacterMovement();
        Jump();
    }

    private void CharacterMovement()
    {
        
        float moveXSpeed = Input.GetAxisRaw("Horizontal") * moveSpeed;
        rigidbodyREF.velocity = new Vector2( moveXSpeed ,rigidbodyREF.velocity.y);
        
        float currentVelocity = rigidbodyREF.velocity.x;
        //handle direction change
        if(currentVelocity < 0)
        {
            transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);
        }
        else if(currentVelocity > 0)
        {
            transform.localScale = Vector3.one;
        }

        //Move sideways
        amim.SetFloat("Speed_Param", Mathf.Abs(currentVelocity));

    }

    private void Jump()
    {
        //checking if on the ground
        isOnGround = Physics2D.OverlapCircle(groundPoint.position, .2f, layerGround);
        amim.SetBool("IsOnGround_Param", isOnGround);

        //Jumping
        if (Input.GetButtonDown("Jump") && isOnGround)
        {
            isOnGround = false;

            rigidbodyREF.velocity = new Vector2(rigidbodyREF.velocity.x, jumpForce);
        }

    }

}
