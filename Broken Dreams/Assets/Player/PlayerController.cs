using UnityEngine;


public class PlayerController : MonoBehaviour
{
    [SerializeField, Range(0f, 100f)]
    float maxSpeed = 10f;

    [SerializeField, Range(0f, 10f)]
    float jumpHeight = 5f;

    [SerializeField, Range(0f, 100f)]
    float maxAcceleration = 10f;

    [SerializeField, Range(0f, 0.5f)]
    float airModifier = 0.2f;


    public float wallAngle = 0.6f;

    bool wallTooSteep = false;
    float maxSpeedChange = 0f;
    private Transform playerInputSpace;
    private Vector3 velocity, desiredVelocity;
    private Rigidbody body;
    private bool grounded;
    private bool moving = true;

    
    private float temp;
    private Animator anim;
    private bool fallhilfe = false;
    private bool ichbintot=false;
    private bool zielflug=false;
    private Kaesespiessbehavior spiessscript;
    


    // FIX
    private bool hitDetect;
    RaycastHit hit;
    int mask = 1;




    private void Awake()
    {
        body = GetComponent<Rigidbody>();
        anim = GameObject.Find("Susi").GetComponent<Animator>();
        
        playerInputSpace = Camera.main.transform;
        spiessscript = FindObjectOfType<Kaesespiessbehavior>();
    }

    // Drawing the BoxCast
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        //Check if there has been a hit yet
        if (hitDetect)
        {
            //Draw a Ray forward from GameObject toward the hit
            Gizmos.DrawRay(this.transform.position + new Vector3(0f, 1f, 0f), this.transform.TransformDirection(Vector3.down) * hit.distance);
            //Draw a cube that extends to where the hit exists
            Gizmos.DrawWireCube(this.transform.position + new Vector3(0f, 1f, 0f) + this.transform.TransformDirection(Vector3.down) * hit.distance, transform.localScale);
        }
    }

    void Update()
    {
        if (ichbintot == false)
        {
            hitDetect = Physics.BoxCast(this.transform.position + new Vector3(0f, 1f, 0f), new Vector3(0.2f, 0.3f, 0.2f), this.transform.TransformDirection(Vector3.down), out hit, Quaternion.identity, Mathf.Infinity, mask, QueryTriggerInteraction.Ignore);
            if (hitDetect)
            {
                //ebug.Log(hit.normal);
                if (hit.normal.y <= wallAngle)
                {
                    wallTooSteep = true;
                }
                else
                {
                    wallTooSteep = false;
                }


                if (hit.distance < 0.8f) //&& hit.normal.y >= 0.5f
                {
                    if(!grounded)
                    {
                        FindObjectOfType<AudioManager>().Play("Jump");
                    }
                    grounded = true;
                    anim.SetBool("amboden", true);
                    anim.SetBool("fallen", false);
                    anim.SetBool("sprungfallen", false);
                }
                else
                {
                    grounded = false;
                    anim.SetBool("amboden", false);
                }
            }

            Vector2 playerInput;
            playerInput.x = Input.GetAxis("Horizontal");
            playerInput.y = Input.GetAxis("Vertical");
            playerInput = Vector2.ClampMagnitude(playerInput, 1f);


            //Animation
            if (Input.GetAxisRaw("Vertical") != 0 || Input.GetAxis("Horizontal") != 0)
                {
                    anim.SetBool("laufen", true);
                }
                else
                {
                    anim.SetBool("laufen", false);
                }
                //bug?
                if (body.velocity.y < 0 && body.velocity.y > -2.3f)
                {

                    fallhilfe = true;
                }

                if (fallhilfe == true)
                {
                    anim.SetBool("sprungfallen", true);
                    anim.SetBool("springen", false);
                    fallhilfe = false;

                }
                
            if (zielflug == false)
            {

                if (Input.GetButtonDown("Jump") && moving)
                {
                    Jump();
                }

                // changes the direction where the player runs based on the Cam Transform
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

                // Makes Player always look in the movement direction
                Vector3 movementDirection = desiredVelocity;
                movementDirection.Normalize();
                if (movementDirection != Vector3.zero && moving)
                {
                    transform.forward = movementDirection;
                }
            }
        }
    }

    // changes PayerVelocity depending on grounded status
    private void FixedUpdate()
    {
        if (grounded && moving)
        {
            velocity = body.velocity;


            if (wallTooSteep)
            {
                maxSpeedChange = maxAcceleration * Time.deltaTime * 0.01f;
            }
            else
            {
                maxSpeedChange = maxAcceleration * Time.deltaTime;
            }

            //maxSpeedChange = maxAcceleration * Time.deltaTime;
            velocity.x = Mathf.MoveTowards(velocity.x, desiredVelocity.x, maxSpeedChange);
            velocity.z = Mathf.MoveTowards(velocity.z, desiredVelocity.z, maxSpeedChange);
            body.velocity = velocity;
        }
        else if (!grounded && moving)
        {
            velocity = body.velocity;

            if (wallTooSteep)
            {
                maxSpeedChange = maxAcceleration * Time.deltaTime * airModifier *0.01f;
            }
            else
            {
                maxSpeedChange = maxAcceleration * Time.deltaTime;
            }

            // maxSpeedChange = maxAcceleration * Time.deltaTime * airModifier;
            velocity.x = Mathf.MoveTowards(velocity.x, desiredVelocity.x, maxSpeedChange);
            velocity.z = Mathf.MoveTowards(velocity.z, desiredVelocity.z, maxSpeedChange);
            body.velocity = velocity;
        }
    }

    private void Jump()
    {
        if (grounded && !wallTooSteep)
        {
            velocity.y = 0f;
            velocity.y = Mathf.Sqrt(-2f * Physics.gravity.y * jumpHeight);
            body.AddForce(velocity, ForceMode.Impulse);

            //Animation
            anim.SetBool("springen", true);
            anim.SetBool("laufen", false);
            temp = transform.position.y;
            fallhilfe = true;
        }
    }

    public void Move()
    {
        moving = true;
    }

    public void Freeze()
    {
        moving = false;
    }


    //Steffen setzt bei tod durch maus 5 sek das update script aus, auslöser in Mausbehavior
    public void Tot()
    {
        ichbintot = true;
        body.isKinematic = true;
    }
    public void Leben()
    {
        ichbintot = false;
        body.isKinematic = false;
       
    }

}