using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mausbehaviour : MonoBehaviour
{
    private GameObject Player;
    private PlayerController Playerscript;
    private GameObject Neutralemty;
    private GameObject Durchbruchposi;
    private GameObject Kaese;
    private GameObject Kaesezwei;
    private Kaesebehavior Kaesescript;
    private Kaesebehavior Kaesescriptzwei;
    public Animator anim;
    private Animator playeranim;
    private Animator teddyanim;
    private PickUp pickupscript;
    private GameObject Respawnpoint;
    private ScreenFade Facette;
    private GameObject Teddy;
    private Teddycontroller Teddyscript;
    private Kaesespiessbehavior spiessscript;

   private float MausPlayerAbstand;
  private float NeutralPlayerAbstand;
    private float MausTeddyAbstand;

   
    private float chasecircle;
    private float killradius = 2;
    
    private float Minchasecirledistance = 15f;
    private float Mousespeed = 3f;
    private bool durchgebrochen = false;
    private float fressspeed = 0;
    private Vector3 adjusted;
    private bool kaeseinderhand=false;
    private bool kaseinderhandzwei = false;
    public bool kaeseeinsfocused;
    public bool Kaesezweifocused;
    private bool teddyamleben = true;
    private GameObject Teddyspawner;
    public GameObject Blut;
    public GameObject Spritzer;
    private bool Dieletzteboolichschwoere = false;

    public bool playertot;

    private float ablenkungszeitraum = 0.08f; // ist faktor für time delta time also nicht in sek

    private void Start()
    {
        Player = GameObject.Find("Player 1");
        Neutralemty = GameObject.Find("Neutralposition");
        Durchbruchposi = GameObject.Find("durchbruchposi");
        Kaese = GameObject.Find("Käseemty");
        Kaesescript =  Kaese.GetComponent<Kaesebehavior>();
        Kaesezwei = GameObject.Find("Käseemtyzwei");
        Kaesescriptzwei = Kaesezwei.GetComponent<Kaesebehavior>();
        playeranim = GameObject.FindGameObjectWithTag("Susi").GetComponent<Animator>();
        pickupscript = Player.GetComponent<PickUp>();
        Playerscript = Player.GetComponent < PlayerController>();
        Respawnpoint = GameObject.Find("Respawnpoint");
        Facette = FindObjectOfType<ScreenFade>();
        Teddy = GameObject.Find("Playerteddy");
        Teddyscript = Teddy.GetComponent<Teddycontroller>();
        teddyanim = GameObject.FindGameObjectWithTag("Teddyrigged").GetComponent<Animator>();
        Teddyspawner = GameObject.Find("Teddyspawn");
        spiessscript = GameObject.FindObjectOfType<Kaesespiessbehavior>();
     
    }


    private void Update()
    {

        adjusted = Player.transform.position;
        adjusted.y = 0;
      MausPlayerAbstand = Vector3.Distance(transform.position, Player.transform.position);
        MausTeddyAbstand= Vector3.Distance(transform.position, Teddy.transform.position);
        NeutralPlayerAbstand = Vector3.Distance(Neutralemty.transform.position, Player.transform.position);
        

     
        chasecircle = NeutralPlayerAbstand;

       
        if(spiessscript.einmalforce==true)
        {
            Instantiate(Blut, transform.position, transform.rotation);
            if (Dieletzteboolichschwoere == false)
            {
                Instantiate(Spritzer,transform.position, transform.rotation);
            transform.position = new Vector3(transform.position.x, -100, transform.position.z);
                Dieletzteboolichschwoere = true;
            }

        }

        if(kaeseeinsfocused==false && Kaesezweifocused ==false)
        {
            kaeseeinsfocused = Kaesescript.kasefocusanfang();



            Kaesezweifocused = Kaesescriptzwei.kasefocusanfang();
        }
   if(kaeseeinsfocused == true)
        {
            Kaesezweifocused = false;
            kaeseeinsfocused = Kaesescript.kasefocusanfang();
        }
   if(Kaesezweifocused==true)
        {
            kaeseeinsfocused = false;
            Kaesezweifocused = Kaesescriptzwei.kasefocusanfang();
        }
          
        
      
     
       
      

        if (pickupscript.carriedObj!=null)
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                if (pickupscript.carriedObj.name == ("Käseemty"))
                {
                    kaeseinderhand = true;
                }
                else if (pickupscript.carriedObj.name == ("Käseemtyzwei"))
                {
                    kaseinderhandzwei = true;
                }
            }
        }
        else
        {
            kaeseinderhand = false;
            kaseinderhandzwei = false;
        }

        //1
        if (durchgebrochen == true)
        {
            //2
            if (Kaesescript.ablenkungsmanoever == false&&Kaesescriptzwei.ablenkungsmanoever ==false)
            {              
                //wenn in kreis dann  chase
                //3w
                if (chasecircle <= Minchasecirledistance&&playertot==false)
                {                                  
                    Mousespeed = 3f;
                    anim.speed = 1f;
                    transform.LookAt(adjusted);
                    transform.position += transform.forward * Mousespeed * Time.deltaTime;
                    anim.SetBool("chase", true);

                    if(MausPlayerAbstand <= killradius)
                    {                    
                        playeranim.SetBool("tot", true);
                        anim.SetBool("schlagen", true);
                        playertot = true;
                        anim.SetBool("chase", true);
                        StartCoroutine(StartCooldown());
                    }
                    else if(MausTeddyAbstand <= killradius+ 1&&MausPlayerAbstand <= killradius+0.5f)
                    {
                        
                        teddyanim.SetBool("sterben", true);
                        playeranim.SetBool("tot", true);
                        playertot = true;
                        teddyamleben = false;
                        StartCoroutine(StartCooldown());
                    }

                }

                //zurücklaufen
                //3
                else
                {
                   
                    Mousespeed = 1f;
                    anim.speed = 0.5f;
                    transform.LookAt(Neutralemty.transform);
                    //Wenn wieder auf neurtralposi
                    //4
                    if (Vector3.Distance(transform.position, Neutralemty.transform.position) <= 1)
                    {
                        transform.LookAt(adjusted);
                        anim.SetBool("chase", false);
                        
                    }
                    //wenn noch nicht zurück weitermachen
                    //4
                    else
                    {
                        transform.position += transform.forward * Mousespeed * Time.deltaTime;
                        anim.SetBool("chase", true);
                    }
                }
            }

            //2
            else
            {
                //Wenn käse 1 näher an maus da hinlaufen
                //5
              //  if (Vector3.Distance(transform.position, Kaese.transform.position) < Vector3.Distance(transform.position, Kaesezwei.transform.position))
               // {
                    
                    if (kaeseeinsfocused==true)
                    {
                       
                        if (MausPlayerAbstand <= killradius)
                        {

                            playeranim.SetBool("tot", true);
                            anim.SetBool("schlagen", true);
                            playertot = true;
                            anim.SetBool("chase", true);
                            StartCoroutine(StartCooldown());

                        }
                    else if (MausTeddyAbstand <= killradius+1&& MausPlayerAbstand <= killradius+0.5f)
                    {
                        teddyanim.SetBool("sterben", true);
                        playeranim.SetBool("tot", true);
                        playertot = true;
                        teddyamleben = false;
                        StartCoroutine(StartCooldown());
                    }
                    if (kaeseinderhand == true&&kaseinderhandzwei==false)
                        {
                            Mousespeed = 3f;
                            anim.speed = 1f;
                            transform.LookAt(Kaese.transform);
                        }
                        else
                        {


                            Mousespeed = 1f;
                            anim.speed = 0.5f;
                            transform.LookAt(Kaese.transform);
                        }

                        //auf käseposition
                        if (Vector3.Distance(transform.position, Kaese.transform.position) <= 0.1 && playertot == false)
                        {
                            transform.LookAt(Kaese.transform);
                            anim.SetBool("chase", false);

                            Kaese.transform.localScale = Vector3.Lerp(new Vector3(1, 1, 1), new Vector3(0, 0, 0), fressspeed);
                            fressspeed += Time.deltaTime * ablenkungszeitraum;
                            anim.SetBool("essen", true);
                            if (Kaese.transform.localScale == new Vector3(0, 0, 0))
                            {
                                fressspeed = 0;
                                Kaese.transform.position = Kaesescript.spawnpoint;
                                Kaese.transform.localScale = new Vector3(1, 1, 1);
                                anim.SetBool("essen", false);
                                kaeseeinsfocused = false;
                            }
                        }

                        //wenn noch nicht bei käse weitermachen
                        else
                        {
                            transform.position += transform.forward * Mousespeed * Time.deltaTime;
                            anim.SetBool("chase", true);
                        }
                    }
             //   }



                //5
                //wenn käse zwei näher an maus
               // else
             //   {
                    

                    if (Kaesezweifocused == true)
                    {
                   
                        if (MausPlayerAbstand <= killradius)
                        {

                            playeranim.SetBool("tot", true);
                            anim.SetBool("schlagen", true);
                            playertot = true;
                            anim.SetBool("chase", true);
                            StartCoroutine(StartCooldown());

                        }
                   else if (MausTeddyAbstand <= killradius+ 1&&MausPlayerAbstand <= killradius+0.5f)
                    {
                        teddyanim.SetBool("sterben", true);
                        playeranim.SetBool("tot", true);
                        playertot = true;
                        teddyamleben = false;
                        StartCoroutine(StartCooldown());
                    }
                    if (kaseinderhandzwei == true && kaeseinderhand == false)
                        {
                            Mousespeed = 3f;
                            anim.speed = 1f;
                            transform.LookAt(Kaesezwei.transform);
                        }

                        else
                        {


                            Mousespeed = 1f;
                            anim.speed = 0.5f;
                            transform.LookAt(Kaesezwei.transform);
                        }

                        //auf käseposition
                        if (Vector3.Distance(transform.position, Kaesezwei.transform.position) <= 0.1 && playertot == false)
                        {
                            transform.LookAt(Kaesezwei.transform);
                            anim.SetBool("chase", false);

                            Kaesezwei.transform.localScale = Vector3.Lerp(new Vector3(1, 1, 1), new Vector3(0, 0, 0), fressspeed);
                            fressspeed += Time.deltaTime * ablenkungszeitraum;
                            anim.SetBool("essen", true);
                            if (Kaesezwei.transform.localScale == new Vector3(0, 0, 0))
                            {
                                fressspeed = 0;
                                Kaesezwei.transform.position = Kaesescriptzwei.spawnpoint;
                                Kaesezwei.transform.localScale = new Vector3(1, 1, 1);
                                anim.SetBool("essen", false);
                                Kaesezweifocused = false;
                            }
                        }

                        //wenn noch nicht bei käse weitermachen
                        else
                        {
                            transform.position += transform.forward * Mousespeed * Time.deltaTime;
                            anim.SetBool("chase", true);
                        }
                    }
               // }
            }
        }

        // Bis zur default durchbruchposi laufen dann normalen code aktivieren
        //1
        else
        {
            transform.LookAt(Durchbruchposi.transform);
            Mousespeed = 3f;
            anim.speed = 1f;
            anim.SetBool("chase", true);
            transform.position += transform.forward * Mousespeed * Time.deltaTime;
            if (Vector3.Distance(transform.position, Durchbruchposi.transform.position) <= 0.2)
            {
                durchgebrochen = true;
            }
        }
    }


   

    public IEnumerator StartCooldown()
    {
        Playerscript.Tot();
        Teddyscript.TotTeddy();       
        yield return new WaitForSeconds(0.2f);
        Facette.death();
        anim.SetBool("schlagen", false);
        yield return new WaitForSeconds(4f);
        Player.transform.position = Respawnpoint.transform.position;
        if (teddyamleben == false)
        {
            Teddy.transform.position = Teddyspawner.transform.position;
            Player.transform.position = Teddyspawner.transform.position;
        }
        yield return new WaitForSeconds(3f);
        playeranim.SetBool("tot", false);
        teddyanim.SetBool("sterben", false);
        anim.SetBool("schlagen", false);
        playertot = false;
        Playerscript.Leben();
        Teddyscript.LebenTeddy();
        Facette.live();
     
        //hier respawn
    }


}
