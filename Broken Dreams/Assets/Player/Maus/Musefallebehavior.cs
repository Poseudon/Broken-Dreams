using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Musefallebehavior : MonoBehaviour
{
    public Animator anim;
    public Animator animteddy;
    public bool gespannt = false;
    public bool stopYouViolatedTheLaw;
    private bool inplace = false;
    private Vector3 adjusted;
    private GameObject Teddy;
    private bool weakinplace=false;
    private TextClues clues;
    private Rigidbody teedybody;
    private GameObject Player;
    private Outline outi;

    private void Start()
    {
        Teddy = GameObject.Find("Playerteddy");
        clues = FindObjectOfType<TextClues>();
        teedybody = Teddy.GetComponent<Rigidbody>();
        Player = GameObject.Find("Player 1");
        outi = gameObject.GetComponent<Outline>();
    }


    // Update is called once per frame
    private void Update()
    {
        adjusted = transform.position;
        adjusted.y = 0;

        if (inplace==true&& gespannt==false)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                anim.SetBool("spannen", true);
                animteddy.SetBool("spannen", true);
                gespannt = true;
                StartCoroutine(StartCooldown());
                
            }
            
        }
       if( weakinplace==true&&gespannt==false)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
               
                StartCoroutine(clues.TextClue("You'd have to be as strong as a bear to set this trap."));
            }
        }
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Teddy")
        {
            inplace = true;
            
        }
       if(other.gameObject.tag=="Player")

        {
            outi.enabled = true;
                weakinplace = true;
           
                     
            
          
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Teddy")
        {
            inplace = false;
           
        }
        if(other.gameObject.tag=="Player")
        {
            outi.enabled = false;
            weakinplace = false;
        }
    }
    public IEnumerator StartCooldown()
    {

        yield return new WaitForSeconds(0.2f);
        teedybody.freezeRotation = true;
        animteddy.SetBool("spannen", false);
        stopYouViolatedTheLaw = true;
        Teddy.transform.LookAt(adjusted);
        yield return new WaitForSeconds(2.4f);
        teedybody.freezeRotation = false;
        stopYouViolatedTheLaw = false;



    }

}
