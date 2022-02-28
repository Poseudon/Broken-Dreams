using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kaesespiessbehavior : MonoBehaviour
{

    private GameObject Kaeseeins;
    private GameObject Kaesezwei;
    private float Mindistanz = 3f;
    public Musefallebehavior Fallenscript;
    private PickUp pickupscript;
    private bool kaeseinderhand;
    public bool abflugsbereit;
    private GameObject Player;
    private PlayerController playerscript;
    private Rigidbody playebody;
    private GameObject pos;
    private float zaehler=0;
    private bool peins = false;
    private bool pzwei= false;
   private new Vector3 temp;
    private new Vector3 tempzwei;
    private new Vector3 Playertemp;
    private bool posibool = true;
    private bool single;
    private Animator anim;
    public bool einmalforce = false;
    private Animator animmausfalle;
    private TextClues clues;

    private GameObject endposi;
    private Vector3 adjusted;
    // Start is called before the first frame update
    void Start()
    {
        Kaeseeins = GameObject.Find("Käseemty");
        Kaesezwei = GameObject.Find("Käseemtyzwei");
        pickupscript = FindObjectOfType<PickUp>();
        Player = GameObject.Find("Player 1");
        playerscript = FindObjectOfType<PlayerController>();
        playebody = Player.GetComponent<Rigidbody>();
        pos = GameObject.Find("pos2");
        temp = Kaeseeins.transform.localScale;
        tempzwei = Kaesezwei.transform.localScale;
        anim= GameObject.FindGameObjectWithTag("Susi").GetComponent<Animator>();
        endposi = GameObject.Find("Endposi");
        animmausfalle = GameObject.Find("Mausefallemitanim").GetComponent<Animator>();
        clues = FindObjectOfType<TextClues>();
    }

    // Update is called once per frame
    void Update()
    {
        adjusted = endposi.transform.position;
        adjusted.y = 0;

        if (pickupscript.carriedObj != null)
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                if (pickupscript.carriedObj.name == ("Käseemty"))
                {
                    kaeseinderhand = true;
                }
                else if (pickupscript.carriedObj.name == ("Käseemtyzwei"))
                {
                    kaeseinderhand = true;
                }
            }
        }
        else
        {
            kaeseinderhand = false;
        }



        if (Fallenscript.gespannt == true)
        {
            if (posibool == true)
            {
                Playertemp = new Vector3(Player.transform.position.x, 0.2f, Player.transform.position.z);
                posibool = false;
            }


            if (Vector3.Distance(Kaeseeins.transform.position, transform.position) <= Mindistanz)
            {

                if (kaeseinderhand == false)
                {
                    if (single == false)
                    {
                        Player.transform.position = Vector3.Lerp(Playertemp, pos.transform.position, zaehler);
                        playebody.isKinematic = true;

                    }

                    playerscript.enabled = false;
                    peins = true;
                    Kaeseeins.transform.position = transform.position;

                    zaehler += Time.deltaTime * 0.2f;
                    if (zaehler >= 1)
                    {
                        anim.SetBool("aufabflug", true);
                        anim.SetBool("laufen", false);
                        anim.SetBool("abfluglaufen", false);

                        Player.transform.LookAt(adjusted);
                        single = true;
                        abflug();
                    }
                    else
                    {
                        anim.SetBool("abfluglaufen", true);
                    }


                }

            }







            if (Vector3.Distance(Kaesezwei.transform.position, transform.position) <= Mindistanz)
            {

                if (posibool == true)
                {
                    Playertemp = Player.transform.position;
                    posibool = false;
                }

                if (kaeseinderhand == false)
                {

                    if (single == false)
                    {
                        Player.transform.position = Vector3.Lerp(Playertemp, pos.transform.position, zaehler);
                       
                        playebody.isKinematic = true;
                    }
                    playerscript.enabled = false;
                    pzwei = true;
                    Kaesezwei.transform.position = transform.position;

                    zaehler += Time.deltaTime * 0.2f;

                    if (zaehler >= 1)
                    {
                        anim.SetBool("aufabflug", true);
                        anim.SetBool("laufen", false);
                        anim.SetBool("abfluglaufen", false);

                        Player.transform.LookAt(adjusted);
                        single = true;
                        abflug();
                    }
                    else
                    {
                        anim.SetBool("abfluglaufen", true);
                    }

                }

            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                if (Vector3.Distance(Kaeseeins.transform.position, transform.position) <= Mindistanz)
                {
                    StartCoroutine(clues.TextClue("I have to arm the trap first."));
                }

                if (Vector3.Distance(Kaesezwei.transform.position, transform.position) <= Mindistanz)
                {
                    StartCoroutine(clues.TextClue("I have to arm the trap first."));
                }
            }
        }
          

        }

    private void FixedUpdate()
    {
        if (abflugsbereit == true && Kaesezwei.transform.localScale != tempzwei && pzwei == true)
        {
            if (einmalforce == false)
            {
                animmausfalle.SetBool("ausloesen", true);
                anim.SetBool("aufabflug", false);
                playebody.isKinematic = false;
                anim.SetBool("endflug", true);
                playebody.AddForce(new Vector3(1, 2, -0.2f)*10f, ForceMode.Impulse);
                playebody.isKinematic = false;

               
                einmalforce = true;
            }
        }

        if (abflugsbereit == true && Kaeseeins.transform.localScale != temp && peins == true)
        {
            if (einmalforce == false)
            {
                animmausfalle.SetBool("ausloesen", true);
                anim.SetBool("aufabflug", false);
                anim.SetBool("endflug", true);
                playebody.isKinematic = false;
                playebody.AddForce(new Vector3(1, 2, -0.2f)*10f, ForceMode.Impulse);
                playebody.isKinematic = false;
               
                einmalforce = true;
            }
        }
    }
    //if abflugbereit true und Kaeseein oder Kaesezwei local scale kleiner 0 add force
    public void abflug()
    {
        playebody.detectCollisions = true;
        abflugsbereit = true;
    }
 

} 


        

    
