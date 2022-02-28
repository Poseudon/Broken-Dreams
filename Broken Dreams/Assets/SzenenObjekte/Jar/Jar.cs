using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jar : MonoBehaviour
{
    private Outline line;
    private Canvas letter;
    private  TextClues clues;
    private Quaternion startRot;
    private Vector3 startPos;
    private Vector3 endPos;
    private float gelaufeneZeit = 0f;
    private bool geschubst = false;

    private void Start()
    {

        clues = FindObjectOfType<TextClues>();
        line = GetComponent<Outline>();
        line.enabled = false;
        letter = this.gameObject.GetComponentInChildren<Canvas>();
        letter.enabled = false;

        startRot = this.transform.localRotation;
        startPos = this.transform.position;
        endPos = startPos;
        endPos.y = 0.7f;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Teddy")
        {
            line.enabled = true;
            letter.enabled = true;
        }
        else
        {
            line.enabled = false;
            letter.enabled = false;
        }
    }


    private void OnTriggerStay(Collider other)
    {
        if (Input.GetKey(KeyCode.E))
        {
            if(other.CompareTag("Teddy"))
            {
                geschubst = true;
            }
            else if (other.CompareTag("Player"))
            {
                StartCoroutine(clues.TextClue("I am to weak to topple the glass"));
            } 
        }



        if (!geschubst && other.gameObject.tag == "Player")
        {
            other.gameObject.GetComponent<BoxCollider>().enabled = false;
        }
        else if (geschubst && other.gameObject.tag == "Player")
        {
            other.gameObject.GetComponent<BoxCollider>().enabled = true;
        }
    }

    private void OnTriggerExit()
    {
        line.enabled = false;
        letter.enabled = false;
    }

    private void Update()
    {
        if (geschubst)
        {
            this.gameObject.transform.rotation = Quaternion.Lerp(startRot, Quaternion.Euler(0, 0, -90), gelaufeneZeit);
            gelaufeneZeit += Time.deltaTime * 1.3f;
            this.gameObject.transform.position = Vector3.Lerp(startPos, endPos, gelaufeneZeit);
        }
    }
}