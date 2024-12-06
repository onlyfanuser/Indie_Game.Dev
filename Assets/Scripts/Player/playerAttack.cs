using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public CharacterController2D controller;
    float horizontalMove = 0f;
    public float runSpeed = 40f; 
    bool jump = false;
    bool Crouch = false;
    public Animator animator;
        public float triggertime = 0.00001f;
    private bool istriggered = false;
    private float triggerT = 0f;
    public float moveDistance = 200f;
    float movespeed =5f;
    


    void Update()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;
        animator.SetFloat("Speed",Mathf.Abs(horizontalMove));
        if(Input.GetButtonDown("Jump"))
        {
            jump = true;
            animator.SetBool("IsJumping",true);
        }
       
         if(Input.GetKey(KeyCode.A))
        {
            //移動速度→movespeed(5f)
             transform.Translate(-movespeed*Time.deltaTime,0f,0f);
             GetComponent<SpriteRenderer>().flipX=true;
             GetComponent<Animator>().SetBool("run",true);
        }
        else if(Input.GetKey(KeyCode.D))
        {
             transform.Translate(movespeed*Time.deltaTime,0f,0f);
             GetComponent<SpriteRenderer>().flipX=false;
             GetComponent<Animator>().SetBool("run",true);
            
        }
         else if(Input.GetKey(KeyCode.UpArrow))
      {
        transform.Translate(0, movespeed * Time.deltaTime, 0);
      }
        else
        {
            GetComponent<Animator>().SetBool("run",false);
        }
        
        if(Input.GetMouseButton(0)&&!Input.GetKey(KeyCode.A)&&!Input.GetKey(KeyCode.D))
        {
            GetComponent<Animator>().SetBool("attack2",true);
        }
       
        else
        {
            GetComponent<Animator>().SetBool("attack2",false);
        }
          if(Input.GetMouseButton(0)&&Input.GetKey(KeyCode.D))
        {  
            istriggered = true;
            triggerT= Time.time;
          
            
        }
        else if(Input.GetMouseButton(0)&&Input.GetKey(KeyCode.A))
        {
              istriggered = true;
            triggerT= Time.time;
            
        }      
         else if(Input.GetMouseButton(0)&&!jump)
         {
             GetComponent<Animator>().SetBool("attack3",true);
             while(jump)
             {
                GetComponent<Animator>().SetBool("attack3ground",true);
                GetComponent<Animator>().SetBool("attack3",false);
             }
         }
       
        
       if(istriggered)
        {
            if(Time.time-triggerT<=triggertime)
            {
                 GetComponent<Animator>().SetBool("attack",true);
                   if(Input.GetMouseButton(0)&&Input.GetKey(KeyCode.D))
        {
            transform.Translate(transform.right * moveDistance*Time.deltaTime*13);
       
            
            istriggered = true;
            triggerT= Time.time;
           
            
        }
        else if(Input.GetMouseButton(0)&&Input.GetKey(KeyCode.A))
        {
             transform.Translate(-transform.right * moveDistance*Time.deltaTime*13);
              
            
              istriggered = true;
            triggerT= Time.time;
            
               
            }
            else
            {
                  GetComponent<Animator>().SetBool("attack",false);
            }
        }
       

   
        
        

    }
    }

    public void OnLanding ()
    {
        animator.SetBool("IsJumping",false);
    }
    public void OnCrouching (bool isCrouching)
	{
		animator.SetBool("IsCrouching", isCrouching);
	}
    void FixedUpdate ()
    {
        controller.Move(horizontalMove * Time.fixedDeltaTime,Crouch,jump);
        jump = false;
    }

}
