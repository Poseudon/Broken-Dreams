using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Cannon : MonoBehaviour
{
    public CannonController Controller;
    public Camera mainCamera;
    public Camera cannonCamera;
    public GameObject Player;
    public Canvas crosshair;
    public PickUp pickup;
    private TextClues clues;
    private bool incannon = false;
    bool inRange = false;

    private Billboard[] TestArray;

    string marble = "Marble";

    // Start is called before the first frame update
    void Start()
    {
        Controller.enabled = false;
        cannonCamera.enabled = false;
        this.GetComponent<Outline>().enabled = false;
        crosshair.enabled = false;
        Player = GameObject.Find("Player 1");
        mainCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
        
        clues = FindObjectOfType<TextClues>();
        TestArray = FindObjectsOfType<Billboard>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            this.GetComponent<Outline>().enabled = true;
            inRange = true;
        }
    }


    private void OnTriggerExit()
    {
        this.GetComponent<Outline>().enabled = false;
        inRange = false;
    }


    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player")) 
        {
            if (pickup.carriedObj != null)
            {
                if (pickup.carriedObj.name.StartsWith(marble) || pickup.carriedObj.name == marble)
                {
                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        Controller.canShoot = true;
                        pickup.Interact();
                        StartCoroutine(clues.TextClue("Cannon loaded"));
                    }
                }
                else
                {
                    StartCoroutine(clues.TextClue("I dont think this will fit"));
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (incannon)
            {
                climbOut();
            }
            else if (!incannon && inRange)
            {
                climbIn();
            }
        }
    }

    void climbIn()
    {
        Player.gameObject.SetActive(false);
        //Player.GetComponent<PlayerController>().enabled = false;
        Billboard_ON(false);
        cannonCamera.enabled = true;
        crosshair.enabled = true;
        mainCamera.enabled = false;
        Controller.enabled = true;
        incannon = true;
        StartCoroutine(test());
    }
    void climbOut()
    {
        Player.gameObject.SetActive(true);
        //Player.GetComponent<PlayerController>().enabled = true;
        Billboard_ON(true);
        mainCamera.enabled = true;
        Controller.enabled = false;
        cannonCamera.enabled = false;
        crosshair.enabled = false;
        incannon = false;
        StartCoroutine(test());
    }


    public IEnumerator test()
    {
        yield return new WaitForSeconds(0.5f);
    }

    private void Billboard_ON(bool test)
    {
        for (int i = 0; i < TestArray.Length; i++)
        {
            if (TestArray[i] != null)
            TestArray[i].enabled = test;
        }
    }
}