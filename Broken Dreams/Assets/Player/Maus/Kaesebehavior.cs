using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kaesebehavior : MonoBehaviour
{
    private float ablageradius =15f;
    public GameObject neutralposi;
    public bool ablenkungsmanoever = false;
    public Vector3 spawnpoint;
    public Rigidbody rigi;
    public GameObject Kaesespawn;
    private bool hilfeeins;
    private GameObject Player;
  
   
   
  
    private float distanz;
   private void Start()
    {
        Player = GameObject.Find("Player 1");
      
    }

    // Update is called once per frame
  private void Update()
    {
        spawnpoint = Kaesespawn.transform.position;
       if (rigi.isKinematic==false)
        {
            rigi.isKinematic = true;
        }
        distanz = Vector3.Distance(transform.position, neutralposi.transform.position);
        if (distanz <= ablageradius)
        {

            
            ablenkungsmanoever = true;
            hilfeeins = true;
           

        }
        else
        {
           ablenkungsmanoever = false;
            hilfeeins = false;
           
           
        }

    }



    public bool kasefocusanfang()
    {
        return hilfeeins;
    }

   


}
