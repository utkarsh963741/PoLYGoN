using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.AI;


[RequireComponent(typeof(PlayerMotor))]

public class PlayerController : MonoBehaviour
{

    Camera cam;
    public LayerMask movementMask;
    PlayerMotor motor;
    static Animator anim;

    public Interactable focus;

    public Transform groundCheck;
    public float groundDistance=0.4f;

    //variables
            bool isGrounded;
            public float speed = 7.5f;
            public float runSpeed = 15f;
            public float jumpHeight = 3f;
            public float narspeed = 30f;
            public float interactRadius=5f;

    //testing
        // bool testvar = false;


    void Start()
    {
        cam = Camera.main;
        motor = GetComponent<PlayerMotor>();
        anim = GetComponent<Animator>();

    }

/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    void Update()
    {       
        //getting stats of player
            GameObject player = GameObject.Find("Player");
            CharacterStats playerStat = (CharacterStats) player.GetComponent(typeof(CharacterStats));
            
            
            if(!Input.GetKey("left shift"))
            {
                playerStat.stamina += (float)0.025;
                playerStat.stamina = Mathf.Clamp(playerStat.stamina,0,100);
                playerStat.staminaBar.SetValue(playerStat.stamina);
            }
            

            // if(EventSystem.current.IsPointerOverGameObject())
            //     return;
        
        
        
            //raycast

                if(Input.GetMouseButtonDown(0))                                                         
                 {
                    Ray ray =cam.ScreenPointToRay(Input.mousePosition);                                         
                    RaycastHit hit;

                    if(Physics.Raycast(ray,out hit,100,movementMask))
                    {
                        Debug.Log("We hit "+hit.collider.name+" "+ hit.point);
                        // motor.MoveToPoint(hit.point);
                        RemoveFocus();
                    }
                }


                //interaction
                if(Input.GetMouseButtonDown(1))
                {
                    Ray ray =cam.ScreenPointToRay(Input.mousePosition);                                         
                    RaycastHit hit;

                    if(Physics.Raycast(ray,out hit,100,movementMask))
                    {
                       Interactable interactable = hit.collider.GetComponent<Interactable>();
                       float distance = Vector3.Distance(hit.collider.transform.position,transform.position);
                       if(interactable != null && distance<= interactRadius)
                       {
                           SetFocus(interactable);
                       }
                    }
                }
                if(Input.GetMouseButtonDown(0))
                {
                    Ray ray =cam.ScreenPointToRay(Input.mousePosition);                                         
                    RaycastHit hit;

                    if(Physics.Raycast(ray,out hit,100,movementMask))
                    {
                       Interactable interactable = hit.collider.GetComponent<Interactable>();
                       float distance = Vector3.Distance(hit.collider.transform.position,transform.position);
                       if(interactable != null && distance<= interactRadius)
                       {
                           TriggerDetails(interactable);
                       }
                    }
                }

        //movement
                float translationForward = Input.GetAxis("Vertical") * speed * Time.deltaTime;
                float translationSideward = Input.GetAxis("Horizontal") * speed * Time.deltaTime;

                
    //testing
                // if(Input.GetKeyDown("o"))
                // {
                //      testvar= true;
                // }
                // if(Input.GetKeyDown("p"))
                // {
                //      testvar = false;
                // }

                // if(testvar)
                // {
                //     translationForward=0.2f;
                // }
                // if(!testvar)
                // {
                //     translationForward=0;
                // }


        //movementAnimation
                if(translationForward != 0 && Input.GetKey("left shift") && Input.GetKey("n") && playerStat.stamina>25) 
                {  
                     anim.SetBool("isNaruto",true);
                     anim.SetBool("isRunning",true);
                     anim.SetBool("isWalking",true);
                     speed =narspeed;
                     
                     playerStat.stamina -= (float)0.25;
                     playerStat.stamina = Mathf.Clamp(playerStat.stamina,0,int.MaxValue);
                     playerStat.staminaBar.SetValue(playerStat.stamina);
                }
                else if(translationForward != 0 && Input.GetKey("left shift") && playerStat.stamina>10)
                {    
                    anim.SetBool("isRunning", true);
                    anim.SetBool("isNaruto", false);
                    anim.SetBool("isWalking",true);
                    speed =runSpeed;

                    playerStat.stamina -= (float)0.05;
                    playerStat.stamina = Mathf.Clamp(playerStat.stamina,0,int.MaxValue);
                    playerStat.staminaBar.SetValue(playerStat.stamina);
                }
                else if(translationForward != 0)
                {   
                    anim.SetBool("isWalking", true);
                    anim.SetBool("isRunning", false);
                    anim.SetBool("isNaruto", false);
                    speed = (float) 7.5;
                } 
                else
                {
                    anim.SetBool("isWalking", false);
                    anim.SetBool("isRunning", false);
                    anim.SetBool("isNaruto", false);
                    
                }


                if(Input.GetKey("a"))
                {
                    anim.SetBool("isLeft",true);
                    anim.SetBool("isRight",false);
                }
                else if(Input.GetKey("d"))
                {
                    anim.SetBool("isRight",true);
                    anim.SetBool("isLeft",false);
                }
                else
                {
                    anim.SetBool("isLeft",false);
                    anim.SetBool("isRight",false);
                    
                }

                if(Input.GetKeyDown("f"))
                {
                    anim.Play("rightStrafe");
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

    //interactionFocus

    void SetFocus(Interactable newFocus)
    {
        if(newFocus != focus)
        {
            if(focus!=null)
                focus.OnDefocused();

            focus = newFocus;
        }
        
        newFocus.OnFocused(transform);
    }
    void RemoveFocus()
    {
        if(focus!=null)
            focus.OnDefocused();

        focus = null;
        
    }
    void TriggerDetails(Interactable newFocus)
    {
        if(newFocus != focus)
        {
            if(focus!=null)
                focus.OnDefocused();

            focus = newFocus;
        }
        newFocus.OnShowDetails();
    }

}
