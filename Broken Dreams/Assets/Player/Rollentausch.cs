using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rollentausch : MonoBehaviour
{
    public GameObject Player;
    public GameObject Teddy;
    public PlayerController Playerscript;
    public Teddycontroller Teddyscript;
    private Animator Playeranim;
    private Animator Teddyanim;
    public bool isplayer = true;
    private Rigidbody bodyteddy;
    private Rigidbody bodyplayer;
    private float distanz;
    private float mindistanz = 3f;
    private bool mitnehmen = false;
    private Collider playercolli;
    private bool firstencounter = true;
    public GameObject Particals;
    private Outline outi;
    private GameObject E_Tasteliegen;
    private GameObject E_Tastestehend;




    private void Start()
    {
        bodyteddy = Teddy.GetComponent<Rigidbody>();
        bodyplayer = Player.GetComponent<Rigidbody>();
        Playeranim = GameObject.FindGameObjectWithTag("Susi").GetComponent<Animator>();
        Teddyanim = GameObject.FindGameObjectWithTag("Teddyrigged").GetComponent<Animator>();
        playercolli = Player.GetComponent<Collider>();
        outi = Teddy.GetComponent<Outline>();
        E_Tasteliegen = GameObject.Find("Eknopfliegend");
        E_Tastestehend = GameObject.Find("Eknopfstehend");
    }


    private void Update()
    {
        if(Player.transform.localScale != new Vector3(0, 0, 0))
        {
            bodyteddy.isKinematic = true;
        }

        if (mitnehmen == true)
        {
            playercolli.enabled = false;
            Player.transform.position = new Vector3(Teddy.transform.position.x, Teddy.transform.position.y + 1, Teddy.transform.position.z);

            Player.transform.rotation = Teddy.transform.rotation;
        }

        distanz = Vector3.Distance(Player.transform.position, Teddy.transform.position);
        if (distanz < mindistanz)
        {




            if (Input.GetKeyDown(KeyCode.F))
            {

                if (isplayer == true)
                {
                    Instantiate(Particals, Player.transform.position, Player.transform.rotation);
                    Player.transform.localScale = new Vector3(0, 0, 0);
                   


                    mitnehmen = true;
                    Teddyscript.enabled = true;
                    Playerscript.enabled = false;
                    bodyplayer.isKinematic = true;
                    bodyteddy.isKinematic = false;
                    isplayer = false;
                    Playeranim.SetTrigger("warten");
                    Playeranim.SetBool("warteauf", true);
                    Teddyanim.SetBool("warteauf", false);
                    StartCoroutine(StartCooldown());

                }

                else if (firstencounter == false)
                {
                    if (isplayer == false && Teddyscript.grounded==true)
                    {
                        Player.transform.position = Player.transform.position - Player.transform.forward;
                        Player.transform.localScale = new Vector3(1, 1, 1);
                        mitnehmen = false;
                        playercolli.enabled = true;


                        Teddyscript.enabled = false;
                        Playerscript.enabled = true;
                        bodyplayer.isKinematic = false;
                        bodyteddy.isKinematic = true;
                        isplayer = true;
                        Teddyanim.SetTrigger("warten");
                        Teddyanim.SetBool("warteauf", true);
                        Playeranim.SetBool("warteauf", false);
                        bodyplayer.velocity = new Vector3(0, 0, 0);

                    }
                }
            }


        }
        if (distanz < mindistanz && !mitnehmen)
        {

            outi.enabled = true;
            if (E_Tasteliegen != null)
            {
                E_Tasteliegen.SetActive(true);
            }
            else
            {
                E_Tastestehend.SetActive(true);
            }

        }

        else
        {
            outi.enabled = false;
            if (E_Tasteliegen != null)
            {
                E_Tasteliegen.SetActive(false);
            }


            E_Tastestehend.SetActive(false);


        }

    }



    public IEnumerator StartCooldown()
    {
        yield return new WaitForSeconds(Teddyscript.CooldownDuration);
        firstencounter = false;


    }


}
