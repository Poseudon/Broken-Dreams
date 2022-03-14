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

    private bool grounded, hitDetect;
    private bool moving = true;
    private bool fallhilfe = false;
    private bool ichbintot = false;
    private bool zielflug = false;
    private bool wallTooSteep = false;
    //needed to exclude unwanted things from boxcast
    private int mask = 1;
    private float temp;
    private float maxSpeedChange = 0f;
    public float wallAngle = 0.6f;
    private Vector3 velocity, desiredVelocity;
    private RaycastHit hit;
    private Transform playerInputSpace;
    private Rigidbody body;
    private Kaesespiessbehavior spiessscript;
    private Animator anim;


    private void Awake()
    {
        // give body and anim there counterparts
        body = GetComponent<Rigidbody>();
        anim = GameObject.Find("Susi").GetComponent<Animator>();
        // set Playerinputspace to cam.transform -> if player moves with 'W' its always away from Cam
        playerInputSpace = Camera.main.transform;
        spiessscript = FindObjectOfType<Kaesespiessbehavior>();
    }

    void Update()
    {
        if (ichbintot == false)
        {
            // Boxcast for detecting ground and angle of ground
            hitDetect = Physics.BoxCast(this.transform.position + new Vector3(0f, 1f, 0f), new Vector3(0.2f, 0.3f, 0.2f), this.transform.TransformDirection(Vector3.down), out hit, Quaternion.identity, Mathf.Infinity, mask, QueryTriggerInteraction.Ignore);
            if (hitDetect)
            {
                // Checkin if wall is too steep to run on based on the Y-Value from hit.normal
                if (hit.normal.y <= wallAngle)
                {
                    wallTooSteep = true;
                }
                else
                {
                    wallTooSteep = false;
                }

                // if the hit.distance is below a certain value character counts as grounded and can therefor jump
                if (hit.distance < 0.8f)
                {
                    // plays the landing sound on landing
                    if(!grounded)
                    {
                        FindObjectOfType<AudioManager>().Play("Jump");
                    }
                    grounded = true;

                    // animator bools
                    anim.SetBool("amboden", true);
                    anim.SetBool("fallen", false);
                    anim.SetBool("sprungfallen", false);
                }
                else
                {
                    grounded = false;

                    // animator bools
                    anim.SetBool("amboden", false);
                }
            }

            Vector2 playerInput;
            playerInput.x = Input.GetAxis("Horizontal");
            playerInput.y = Input.GetAxis("Vertical");
            playerInput = Vector2.ClampMagnitude(playerInput, 1f);


            // Animation stuff
            if (Input.GetAxisRaw("Vertical") != 0 || Input.GetAxis("Horizontal") != 0)
            {
                anim.SetBool("laufen", true);
            }
            else
            {
                anim.SetBool("laufen", false);
            }
 
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

                // makes player movement dependant on cam orientation
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
                if ((movementDirection != Vector3.zero) && moving)
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
            // get velocity from rigidbody
            velocity = body.velocity;

            // if wall is too steep maxspeedchange is set to a 1/100 of the normal value, so moving up walls is not possible
            if (wallTooSteep)
            {
                maxSpeedChange = maxAcceleration * Time.deltaTime * 0.01f;
            }
            else
            {
                maxSpeedChange = maxAcceleration * Time.deltaTime;
            }

            //chanching velocity towards desired velocity and then assigning it back to the rigidbody
            velocity.x = Mathf.MoveTowards(velocity.x, desiredVelocity.x, maxSpeedChange);
            velocity.z = Mathf.MoveTowards(velocity.z, desiredVelocity.z, maxSpeedChange);
            body.velocity = velocity;
        }
        else if (!grounded && moving)
        {
            velocity = body.velocity;
            
            // if probably not needed, may cause bugs if removed so it stays
            if (wallTooSteep)
            {
                maxSpeedChange = maxAcceleration * Time.deltaTime * airModifier * 0.01f;
            }
            else
            {
                maxSpeedChange = maxAcceleration * Time.deltaTime * airModifier;
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