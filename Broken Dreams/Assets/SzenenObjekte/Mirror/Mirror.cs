using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mirror : MonoBehaviour
{
    private Quaternion startrot;
    private int gedrueckt = 0;
    private float gelaufeneZeit = 0f;
    public Animator anim;
    public GameObject Teddy;
    public Triggerbox trigger;
    private GameObject Colissionobject;
    public GameObject spiegelndeOberflaeche;
    public GameObject neueOberflaeche;
    private bool zerstoeren = false;
    public Fernbedienungsscriot fernbedienung;
    public Canvas canvas;

    private TextClues clues;

    // Start is called before the first frame update
    void Start()
    { 
        startrot = this.transform.localRotation;
        this.GetComponent<Outline>().enabled = false;
        clues = FindObjectOfType<TextClues>();
        canvas.gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Teddy")) {
            this.GetComponent<Outline>().enabled = true;
            canvas.gameObject.SetActive(true);
        }
        else if (other.CompareTag("Player"))
        {
            StartCoroutine(clues.TextClue("i need to be strong like a bear to move that"));
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Teddy"))
        {
            this.GetComponent<Outline>().enabled = false;
            canvas.gameObject.SetActive(false);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (Input.GetKey(KeyCode.E) && other.CompareTag("Teddy") && !zerstoeren)
        {
            FindObjectOfType<AudioManager>().Play("MirrorBreaking");

            //Debug.Log("gedrueckt");
            Colissionobject = trigger.getcollisonobject();
            gedrueckt = 1;
            Teddy.GetComponent<Teddycontroller>().Freeze();
            Teddy.transform.forward = this.transform.position - Teddy.transform.position;
            iTween.RotateTo(this.gameObject, iTween.Hash("rotation", new Vector3(82.5f, 90f, 0f), "easetype", iTween.EaseType.spring, "onstart", "pull", "oncomplete", "pullfall", "time", 1.3f));
        }
        //this.gameObject.transform.rotation = Quaternion.Euler(0, 0, -45);
    }

    // Update is called once per frame
    void Update()
    {
            if (gedrueckt == 1)
            {
            
                //this.gameObject.transform.localRotation = Quaternion.Lerp(this.gameObject.transform.localRotation, Quaternion.Euler(82.5f, 0, 0), gelaufeneZeit);
                gelaufeneZeit += Time.deltaTime;
                if (gelaufeneZeit >= 1.3f)
                {
                    gedrueckt = 2;
                    if (Colissionobject != null) {
                    if (Colissionobject.name == "Battery")
                        {
                            Vector3 richtung = Vector3.Normalize(Colissionobject.transform.position - this.transform.position);
                            Instantiate(Colissionobject, this.transform.position + richtung*1.2f, Colissionobject.transform.rotation * Quaternion.Euler(0, 0, 45));
                            //Instantiate(neueOberflaeche, spiegelndeOberflaeche.transform.position, spiegelndeOberflaeche.transform.rotation);
                            neueOberflaeche.gameObject.SetActive(true);
                            //neueOberflaeche.transform.parent = this.transform;
                            spiegelndeOberflaeche.gameObject.SetActive(false);
                            zerstoeren = true;
                        }
                    }
                }
            }
            else if (gedrueckt == 2)
            {

                iTween.RotateTo(this.gameObject, iTween.Hash("rotation", new Vector3(0f,90f, 0f), "easetype", iTween.EaseType.spring, "time", 1.3f));
                //this.gameObject.transform.localRotation = Quaternion.Lerp(startrot, this.gameObject.transform.localRotation, gelaufeneZeit);
                gelaufeneZeit = Mathf.Clamp(gelaufeneZeit -= Time.deltaTime, 0, 1.5f);
                if (gelaufeneZeit <= 0) gedrueckt = 0;
                if (zerstoeren && gelaufeneZeit <= 0)
                {
                    this.GetComponent<Outline>().enabled = false;
                    Teddy.GetComponent<Teddycontroller>().Move();
                    fernbedienung.geklont = true;
                    canvas.gameObject.SetActive(false);
                    Destroy(this.GetComponent<Mirror>());
                }
                
            }

      
    }
  
    private void pull()
    {
        anim.SetBool("dreh", true);
    }
    private void pullfall()
    {
        anim.SetBool("dreh", false);
    }
}
