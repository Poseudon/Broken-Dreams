using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class Zsmziehen : MonoBehaviour
{
    public List<GameObject> Anker = new List<GameObject>();
    public GameObject Empty;
    public List<GameObject> Kurbel = new List<GameObject>();
    private bool gelockt = false;
    private bool druecktgerade = false;
    private float gelaufeneZeit = 0;
    private List<Vector3> Startanker = new List<Vector3>();
    private List<Quaternion> Startrot = new List<Quaternion>();
    public List<VisualEffect> visualEffect = new List<VisualEffect>();
    public OrbitCamera Camera;
    public GameObject Zuglol;


    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < Anker.Count; i++)
        {
            Startanker.Add(Anker[i].transform.position);
            Startrot.Add(Anker[i].transform.rotation);
        }
        this.GetComponent<Outline>().enabled = false;
        Empty.gameObject.SetActive(false);
    }

    private void OnTriggerStay(Collider other)
    {
        if (!gelockt && other.gameObject.tag == "Player")
        {
            this.GetComponent<Outline>().enabled = true;
            Empty.gameObject.SetActive(true);
            if (Input.GetKey(KeyCode.E))
            {
                druecktgerade = true;
                //Camera.setkurbelt(true);
                //Camera.distance = 35f;
                for (int i = 0; i < Anker.Count; i++)
                {
                    Anker[i].transform.parent.position = Vector3.Lerp(Startanker[i], Kurbel[i].transform.position, gelaufeneZeit) + (Anker[i].transform.parent.position - Anker[i].transform.position);
                    Anker[i].transform.parent.rotation = Quaternion.Lerp(Startrot[i], Quaternion.Euler(0, 0, 0), gelaufeneZeit);
                    visualEffect[i].SetInt("Attraction Force", 100);
                    visualEffect[i].SetInt("Stick Force", 6000);
                    visualEffect[i].SetInt("SpeedTails", 2);
                    visualEffect[i].SetFloat("Attraction Speed", 1f);
                    visualEffect[i].SetInt("Spawnrate", 15);
                    if (gelaufeneZeit >= 1)
                    {
                        Debug.Log(i);
                        visualEffect[i].enabled = false;
                        gelockt = true;
                        Anker[i].transform.parent.position = new Vector3(0, 0, 0);
                        //Camera.setkurbelt(false);
                        //Camera.distance = 5f;
                        if(Zuglol.activeSelf == false)
                            Zuglol.SetActive(true);

                    }
                }
                gelaufeneZeit = Mathf.Clamp(gelaufeneZeit += Time.deltaTime * 0.1f, 0, 1);

            }
            else
            {
                for (int i = 0; i < Anker.Count; i++)
                {
                    visualEffect[i].SetInt("Attraction Force", 50);
                    visualEffect[i].SetInt("Stick Force", 200);
                    visualEffect[i].SetInt("SpeedTails", 2);
                    visualEffect[i].SetFloat("Attraction Speed", 11f);
                    visualEffect[i].SetInt("Spawnrate", 15);
                }

                druecktgerade = false;
            } 
        }
        else
        {
            this.GetComponent<Outline>().enabled = false;
            Empty.gameObject.SetActive(false);
            druecktgerade = false;
        }

    }

    private void OnTriggerExit()
    {
        this.GetComponent<Outline>().enabled = false;
        Empty.gameObject.SetActive(false);
        druecktgerade = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!druecktgerade)
        {

            if ((Vector3.Distance(Anker[0].transform.position, Kurbel[0].transform.position) < Vector3.Distance(Startanker[0], this.transform.position)) && !gelockt)
            {
                for (int i = 0; i < Anker.Count; i++)
                {
                    Anker[i].transform.parent.position = Vector3.Lerp(Startanker[i], Kurbel[i].transform.position, gelaufeneZeit) + (Anker[i].transform.parent.position - Anker[i].transform.position);
                    Anker[i].transform.parent.rotation = Quaternion.Lerp(Startrot[i], Quaternion.Euler(0, 0, 0), gelaufeneZeit);
                    gelaufeneZeit = Mathf.Clamp(gelaufeneZeit -= Time.deltaTime * 1f, 0, 1);
                }
            }

        }
        for (int i = 0; i < Anker.Count; i++)
        {
            visualEffect[i].SetVector3("AnkerPosition", Anker[i].transform.position - Kurbel[i].transform.position);
        }
    }
}
