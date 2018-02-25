using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public float maxSpeed = 5f;
    public float acceleration = 20f;
    public float jumpForce = 150f;
    public LayerMask whatIsGround;

    [HideInInspector]
    public bool itemInteraction = false,isFlying = true,isStunned=false;

    private float feetRadius = 0.1f;
    private GameObject feet;
    private Rigidbody2D _rigidBody;
    private bool isFacingRight = true;
    private Animator anim;


    void Awake()
    {
        feet = transform.Find("FeetCollider").gameObject;
        _rigidBody = gameObject.GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }
    void FixedUpdate()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(feet.transform.position, feetRadius, whatIsGround);
        if (colliders.Length != 0)//its feet are intersecting with ground
        {
            isFlying = false;
        }
        if (!isFlying && _rigidBody.velocity.magnitude < 0.7)
        {
            anim.SetInteger("State", 0);
        }


    }
    void Update()
    {

        if (itemInteraction || isStunned)
        {
            return;
        }
        Move();

    }
    void Move()
    {

        if (Input.GetKey(KeyCode.RightArrow))
        {
            if (!isFacingRight)
            {
                Flip();
                isFacingRight = true;
            }
            if (!isFlying)
            {
                anim.SetInteger("State", 1);
            }
            Vector2 newVelocity = new Vector2(0, _rigidBody.velocity.y);
            newVelocity.x = (Mathf.Clamp(_rigidBody.velocity.x + acceleration * Time.deltaTime, 0f, maxSpeed));
            _rigidBody.velocity = newVelocity;

        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            if (isFacingRight)
            {
                Flip();
                isFacingRight = false;
            }
            if (!isFlying)
            {
                anim.SetInteger("State", 1);
            }
            Vector2 newVelocity = new Vector2(0, _rigidBody.velocity.y);
            newVelocity.x = (Mathf.Clamp(_rigidBody.velocity.x - acceleration * Time.deltaTime, -maxSpeed, 0f));
            _rigidBody.velocity = newVelocity;
        }
        if (Input.GetKey(KeyCode.UpArrow) && isFlying == false)
        {
            anim.SetInteger("State", 2);
            isFlying = true;
            transform.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, jumpForce));
        }
    }
    void Flip()
    {
        Vector3 newScale = transform.localScale;
        newScale.x = -newScale.x;
        transform.localScale = newScale;
    }
}
