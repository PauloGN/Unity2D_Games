using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class PlayerController : MonoBehaviour
{

    public Rigidbody2D rigidbodyREF;
    private bool isOnGround;

    [SerializeField] private float moveSpeed;
    [SerializeField] private float jumpForce;
    [SerializeField] private Transform groundPoint;
    [SerializeField] private LayerMask layerGround;

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
    }

    private void Jump()
    {

        isOnGround = Physics2D.OverlapCircle(groundPoint.position, .2f, layerGround);

        if (Input.GetButtonDown("Jump") && isOnGround)
        {
            isOnGround = false;

            rigidbodyREF.velocity = new Vector2(rigidbodyREF.velocity.x, jumpForce);
        }

    }

}
