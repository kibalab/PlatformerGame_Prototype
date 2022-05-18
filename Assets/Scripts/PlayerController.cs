using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Animator animator;
    public Rigidbody2D rigidbody;
    public bool onGround = false;
    public bool isJumping = false;

    public float JumpPower = 100;
    public float MoveSpeed = 2;
    
    private Dictionary<List<KeyCode>, Action> KeyEvents;
    private float velocity;
    
    void Start()
    {
        KeyEvents = new Dictionary<List<KeyCode>, Action>()
        {
            {new List<KeyCode>(){KeyCode.D, KeyCode.RightArrow}, toRight},
            {new List<KeyCode>(){KeyCode.A, KeyCode.LeftArrow}, toLeft},
            {new List<KeyCode>(){KeyCode.W, KeyCode.Space, KeyCode.UpArrow}, Jump}
        };

        rigidbody = ComponentUtill.SetComponent<Rigidbody2D>(gameObject);
        animator = ComponentUtill.SetComponent<Animator>(gameObject);
    }

    void Update()
    {
        foreach (var KeyEvent in KeyEvents)
        {
            foreach (var key in KeyEvent.Key)
            {
                if (Input.GetKey(key))
                {
                    KeyEvent.Value.Invoke();
                }
            }
        }
        
        animator.SetFloat("Velocity", velocity);
        velocity = 0;
    }

    private void FixedUpdate()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.TransformDirection(Vector3.down), 0.55f,
            1 << LayerMask.NameToLayer("Platform"));
        Debug.DrawLine(transform.position, hit.point, Color.red);

        onGround = hit;
        
        if (!onGround && isJumping || onGround && isJumping)
        {
            animator.SetBool("Jump", false);
            isJumping = false;
        }

        var fallowPos = Vector2.Lerp(Camera.main.gameObject.transform.position, gameObject.transform.position, 1 * Time.deltaTime);
        Camera.main.gameObject.transform.position = new Vector3(Camera.main.gameObject.transform.position.x,fallowPos.y, Camera.main.gameObject.transform.position.z);
        
        
    }

    public void toRight()
    {
        transform.position += Vector3.right * MoveSpeed * Time.deltaTime;

        velocity = 1;
    }
    public void toLeft()
    {
        transform.position += Vector3.left * MoveSpeed * Time.deltaTime;
        
        velocity = -1;  
    }
    public void Jump()
    {
        if(!onGround || isJumping) return;
        rigidbody.velocity = Vector2.zero;
        
        rigidbody.AddForce(Vector2.up * JumpPower + new Vector2(velocity * MoveSpeed / 2, 0), ForceMode2D.Impulse);
        animator.SetBool("Jump", true);
        isJumping = true;
    }
}
