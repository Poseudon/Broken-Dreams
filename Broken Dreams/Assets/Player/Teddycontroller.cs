using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Teddycontroller : MonoBehaviour
{
    [SerializeField, Range(0f, 100f)]
    float maxSpeed = 10f;

    [SerializeField, Range(0f, 100f)]
    float maxAcceleration = 10f;

    [SerializeField, Range(0f, 0.5f)]
    float airModifier = 0.2f;


    private Transform playerInputSpace;

    private Vector3 velocity, desiredVelocity;
    private Rigidbody body;
    public bool grounded;
    private bool moving = true;

    public Collider Teddystartcollider;
    public Collider Teddystandcolider;
    public GameObject E_Tasteliegen;
    private Musefallebehavior Mausefallescript;



    //temp und anim für Animation

    private Animator anim;
    private GameObject Player;
     private bool firstencounter = true;
    private float distanz;
    private float mindistanz = 6f;
    public float CooldownDuration = 8.5f;
    private bool ichbintot;
  

    private void Awake()
    {
        body = GetComponent<Rigidbody>();
        anim = GameObject.Find("Teddyrigged").GetComponent<Animator>();
        Player = GameObject.Find("Player 1");
       playerInputSpace = Camera.main.transform;
        Mausefallescript = GameObject.FindObjectOfType<Musefallebehavior>();
      

    }

    void Update()
    {

        if (ichbintot == false)
        {
            if (Mausefallescript.stopYouViolatedTheLaw == false)
            {
                if (firstencounter == true && distanz < mindistanz)
                {
                    anim.SetBool("erwachen", true);
                    Destroy(Teddystartcollider);
                    Teddystandcolider.enabled = true;
                    Destroy(E_Tasteliegen);

                    StartCoroutine(StartCooldown());
                }





                if (firstencounter == false)
                {


                    Vector2 playerInput;
                    playerInput.x = Input.GetAxis("Horizontal");
                    playerInput.y = Input.GetAxis("Vertical");
                    playerInput = Vector2.ClampMagnitude(playerInput, 1f);

                    //Animation
                    distanz = Vector3.Distance(Player.transform.position, gameObject.transform.position);

                    if (Input.GetAxisRaw("Vertical") != 0 || Input.GetAxis("Horizontal") != 0)
                    {
                        anim.SetBool("laufen", true);
                    }
                    else
                    {
                        anim.SetBool("laufen", false);
                    }






                    //bis hier




                    if (playerInputSpace)
                    {
                        Vector3 forward = playerInputSpace.forward;
                        forward.y = 0f;
                        forward.Normalize();
                        Vector3 right = playerInputSpace.right;
                        right.y = 0f;
                        right.Normalize();
                        desiredVelocity = (forward * playerInput.y + right * playerInput.x) * maxSpeed;
                    }
                    else
                    {
                        desiredVelocity = new Vector3(playerInput.x, 0f, playerInput.y) * maxSpeed;
                    }

                    Vector3 movementDirection = desiredVelocity;
                    movementDirection.Normalize();

                    if (movementDirection != Vector3.zero && moving)
                    {
                        transform.forward = movementDirection;
                    }
                }
            }
        }
       
        
    }
  
    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "Floor")
        {
            grounded = true;


            //Animation
            anim.SetBool("amboden", true);
         
            anim.SetBool("Idle", true);

        }
       
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Floor")
        {
            grounded = false;


            //Animation
            anim.SetBool("amboden", false);


        }
    }

    private void FixedUpdate()
    {
        if (grounded && moving)
        {
            velocity = body.velocity;
            float maxSpeedChange = maxAcceleration * Time.deltaTime;
            velocity.x = Mathf.MoveTowards(velocity.x, desiredVelocity.x, maxSpeedChange);
            velocity.z = Mathf.MoveTowards(velocity.z, desiredVelocity.z, maxSpeedChange);
            body.velocity = velocity;
        }
        else if (!grounded && moving)
        {
            velocity = body.velocity;
            float maxSpeedChange = maxAcceleration * Time.deltaTime * airModifier;
            velocity.x = Mathf.MoveTowards(velocity.x, desiredVelocity.x, maxSpeedChange);
            velocity.z = Mathf.MoveTowards(velocity.z, desiredVelocity.z, maxSpeedChange);
            body.velocity = velocity;
        }

    }

    public IEnumerator StartCooldown()
    {
       
        yield return new WaitForSeconds(CooldownDuration);
        firstencounter = false;
       
    }

    public void Move()
    {
        moving = true;
    }

    public void Freeze()
    {
        moving = false;
    }

    public void TotTeddy()
    {
        ichbintot = true;
        body.isKinematic = true;
    }
    public void LebenTeddy()
    {
        ichbintot = false;
        body.isKinematic = false;
        
    }

}