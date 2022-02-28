using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fernbedienungsscriot : MonoBehaviour
{
    public GameObject Battery1;
    private bool bat1;
    public GameObject Battery2;
    private bool bat2;
    public PickUp pickup;
    public bool geklont = false;
    private Outline outline;
    public Canvas canvas;
    public GameObject TVScreen;

    private TextClues clues;

    // Start is called before the first frame update
    void Start()
    {
        outline = this.transform.parent.GetComponent<Outline>();
        outline.enabled = false;
        canvas.gameObject.SetActive(false);
        clues = FindObjectOfType<TextClues>();
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (geklont)
            {
                if (pickup.carriedObj != null)
                {
                    
                    if (pickup.carriedObj.name == "Battery")
                    {
                        outline.enabled = true;
                        canvas.gameObject.SetActive(true);
                        if (Input.GetKey(KeyCode.E))
                        {
                            //Debug.Log(pickup.carriedObj.name);
                            Battery1.gameObject.SetActive(true);
                            bat1 = true;
                            pickup.Interact();
                        }
                    }
                    else if (pickup.carriedObj.name == "Battery(Clone)")
                    {
                        outline.enabled = true;
                        canvas.gameObject.SetActive(true);
                        if (Input.GetKey(KeyCode.E))
                        {
                            //Debug.Log(pickup.carriedObj.name);
                            Battery2.gameObject.SetActive(true);
                            bat2 = true;
                            pickup.Interact();
                        }
                    }
                }
                if(bat1 && bat2)
                {
                   TVScreen.gameObject.SetActive(true);
                }
            }
            else
            {
                
                if (pickup.carriedObjafterq != null && pickup.carriedObjafterq.name == "Battery")
                {
                    //sieht so aus als ob ich zwei Batterien brauche, könnte ich sie doch nur duplizieren
                    StartCoroutine(clues.TextClue("looks like i need two batteries, if only i could duplicate mine"));
                }
                else
                {
                    //vielleicht hat die Fernbedienung keine Batterien
                    StartCoroutine(clues.TextClue("Maybe the remote has no batteries?"));
                }

            }

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            outline.enabled = false;
            canvas.gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
