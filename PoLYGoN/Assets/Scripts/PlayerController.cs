using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


[RequireComponent(typeof(PlayerMotor))]



public class PlayerController : MonoBehaviour
{

    Camera cam;
    public LayerMask movementMask;
    PlayerMotor motor;
    static Animator anim;
    public Rigidbody rb;

    public Transform groundCheck;
    public float groundDistance=0.4f;


    //variables
            public float speed = 3.5f;
            bool isGrounded;
            public float jumpHeight = 3f;


    void Start()
    {
        cam = Camera.main;
        motor = GetComponent<PlayerMotor>();
        anim = GetComponent<Animator>();

    }

/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    void Update()
    {   //raycast
                if(Input.GetMouseButtonDown(0))                                                         
                 {
                    Ray ray =cam.ScreenPointToRay(Input.mousePosition);                                         
                    RaycastHit hit;

                    if(Physics.Raycast(ray,out hit,100,movementMask))
                    {
                        Debug.Log("We hit "+hit.collider.name+" "+ hit.point);
                        //motor.MoveToPoint(hit.point);
                    }
                }

        //movement
                float translationForward = Input.GetAxis("Vertical") * speed * Time.deltaTime;
                float translationSideward = Input.GetAxis("Horizontal") * speed * Time.deltaTime;

               
                
                

        //movementAnimation
                if(translationForward != 0 && Input.GetKey("left shift") && Input.GetKey("n")) 
                {  
                     anim.SetBool("isNaruto",true);
                     anim.SetBool("isRunning",true);
                     anim.SetBool("isWalking",false);
                     speed =25f;
                }
                else if(translationForward != 0 && Input.GetKey("left shift"))
                {    
                    anim.SetBool("isRunning", true);
                    anim.SetBool("isNaruto", false);
                    speed =10f;
                }
                else if(translationForward != 0)
                {   
                    anim.SetBool("isWalking", true);
                    anim.SetBool("isRunning", false);
                    speed =3.5f;
                } 
                else
                {
                    anim.SetBool("isWalking", false);
                    anim.SetBool("isRunning", false);
                    anim.SetBool("isNaruto", false);
                    
                }

                


        transform.Translate(translationSideward,0,translationForward);
                

            //jump

                isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, movementMask);
                    
                    if(Input.GetKeyDown("space") && isGrounded && !Input.GetKey("n"))
                    {    
                        anim.Play("jump",-1,0f);
                    }


            //dance
                if(Input.GetKeyDown("'"))
                {
                    anim.Play("breakdance",-1,0f);
                }
                if(Input.GetKeyDown(";"))
                {
                    anim.Play("spin",-1,0f);
                    Debug.Log(isGrounded);
                }

    }

///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

}
