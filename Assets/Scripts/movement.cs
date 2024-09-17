//移動程式

using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public class Playermovement : MonoBehaviour
{
    private float horizontal;
    private float speed = 8f;
    [SerializeField]private float jumpingPowe = 16f;
    private bool isFacingRight = true;

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        //取得輸入
        horizontal = Input.GetAxisRaw("Horizontal");

        if(Input.GetButtonDown("Jump") && IsGrounded())
        {
            rb.velocity = new Vector2(rb.velocity.x,jumpingPowe);
        }

        if(Input.GetButtonUp("Jump") && rb.velocity.y > 0f)
        {
            rb.velocity = new Vector2(rb.velocity.x,rb.velocity.y*0.5f);
        }

        Flip();

        
    }
    private void FixedUpdate()
    {
     rb.velocity = new Vector2(horizontal*speed,rb.velocity.y);   
    }

    //判斷是否觸地
    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position,0.2f,groundLayer);
    }
    //轉向
    private void Flip()
    {
        if(isFacingRight && horizontal < 0f || !isFacingRight && horizontal > 0f)
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }
}