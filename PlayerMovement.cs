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


    //declare references in Start()
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
        //create a new Vector2 and multiplies x to speed up the horizontal moving speed of the regidbody,
        //assign the vector2 as the regidbody velocity.
        Vector2 playerVelocity = new Vector2(moveInput.x * runSpeed,myRigid.velocity.y);
        myRigid.velocity = playerVelocity;
        //epislon is the smallest float the isn't 0 
        //I'll just pretend it is 0
        //the code below means [if the horizontal speed is not zero] which means [if it is moving] then play the isRunning animation.
        bool playerHorizontalSpeed = Mathf.Abs(myRigid.velocity.x) > Mathf.Epsilon;
        myAnimator.SetBool("isRunning",playerHorizontalSpeed);
    }
    
    
    void OnJump(InputValue value)  //OnJUmp is automatically recognized by unity, which will run when the jump button was pressed
    {
    //if it is pressed and also the capsulecollider of the player is touching Groundlayer(is on ground)
    //then add velocity to y so it can jump ONCE 
        if (value.isPressed && myCapsuleCollider.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
            myRigid.velocity += new Vector2(0f,jumpSpeed);
        }
    }

    void FlipSprite()
    {
        // if the player is moving, change the way it faces based on the velocity(if velocity is positive,face right, vice versa
        //Mathf.Sign() Returns the sign of f.
        //Return value is 1 when f is positive or zero, -1 when f is negative.
        
        
        bool playerHorizontalSpeed = Mathf.Abs(myRigid.velocity.x)>Mathf.Epsilon;
        if (playerHorizontalSpeed)
        {
            transform.localScale = new Vector2(Mathf.Sign(myRigid.velocity.x), 1f);
        }
       
    }
}
