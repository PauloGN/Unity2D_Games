using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    [SerializeField] float maxSpeed = 5.0f;
    //Ground and Jump control
    [SerializeField] private Transform groundPoint;
    [SerializeField] private LayerMask layerGround;
    [SerializeField] private float jumpForce = 10.0f;

    //Controllers and references
    Rigidbody2D rbREF;
    private float movementSpeed;
    private bool bIsfacingRight = true;
    private bool bIsOnGround = true;
    private bool bJump = false;
    private bool bDoubleJump;


    // Start is called before the first frame update
    void Start()
    {
        rbREF= GetComponent<Rigidbody2D>();
        movementSpeed = maxSpeed;
    }

    // Update is called once per frame
    void Update()
    {

        JumpAndDoubleJumpCheck();

    }

    private void FixedUpdate()
    {
        PlayerMovement();
        Jump();
    }


    #region Movement animation and flip
    private void PlayerMovement()
    {

        //Move right and left
        float h = Input.GetAxisRaw("Horizontal");
        float v = rbREF.velocity.y;

        rbREF.velocity = new Vector2(h * movementSpeed, v);

        //when to flip the prite based on the direction

        if (h < 0 && bIsfacingRight)
        {
            Flip();
        }else if(h > 0 && !bIsfacingRight)
        {
            Flip();
        }
    }

    //Flip player sprite
    private void Flip()
    {
        bIsfacingRight = !bIsfacingRight;

        Vector3 myScale = transform.localScale;
        myScale.x *= -1;
        transform.localScale = myScale;
    }

    private void GroundCheck()
    {
        //checking if on the ground
        bIsOnGround = Physics2D.OverlapCircle(groundPoint.position, .2f, layerGround);
    }


    private void JumpAndDoubleJumpCheck()
    {

        GroundCheck();

        if (bIsOnGround)
        {
            bDoubleJump = false;
        }

        if (Input.GetButtonDown("Jump") && (bIsOnGround || !bDoubleJump))
        {
            bJump = true;

            if(!bDoubleJump && !bIsOnGround)
            {
                bDoubleJump = true;
            }
        }
    }


    private void Jump()
    {

        if (bJump)
        {
            rbREF.velocity = Vector2.zero;
            rbREF.velocity = Vector2.up * jumpForce;
            bJump= false;
        }
    
    }

    #endregion



}


