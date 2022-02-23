using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    Vector2 moveInput;
    Rigidbody2D myRigid;
    [SerializeField] float runSpeed = 10f;
    [SerializeField] float jumpSpeed = 10f;
    CapsuleCollider2D myCapsuleCollider;
    Animator myAnimator;



    void Start()
    {
        myRigid = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        myCapsuleCollider = GetComponent<CapsuleCollider2D>();
    }

    
    void Update()
    {
        Run();
        FlipSprite();
    }
    void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
        Debug.Log(moveInput);
    }
    void Run()
    {
        Vector2 playerVelocity = new Vector2(moveInput.x * runSpeed,myRigid.velocity.y);
        myRigid.velocity = playerVelocity;

        bool playerHorizontalSpeed = Mathf.Abs(myRigid.velocity.x) > Mathf.Epsilon;
        myAnimator.SetBool("isRunning",playerHorizontalSpeed);
    }
    void OnJump(InputValue value)
    {
        if (value.isPressed && myCapsuleCollider.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
            myRigid.velocity += new Vector2(0f,jumpSpeed);
        }
    }

    void FlipSprite()
    {
        bool playerHorizontalSpeed = Mathf.Abs(myRigid.velocity.x)>Mathf.Epsilon;

        if (playerHorizontalSpeed)
        {
            transform.localScale = new Vector2(Mathf.Sign(myRigid.velocity.x), 1f);
        }
       
    }
}
