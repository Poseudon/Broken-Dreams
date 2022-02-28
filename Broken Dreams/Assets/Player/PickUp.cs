using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    private bool carrying;
    public GameObject carriedObj;
    public Transform carryPosition;
    public GameObject carriedObjafterq;

    // Start is called before the first frame update
    void Start()
    {
        carriedObj = null;
        carrying = false;
        carriedObjafterq = null;

    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "pickable" && !carrying)
        {
            carriedObj = other.gameObject;
        }
        
    }

    private void OnTriggerExit(Collider other)
    {
        if (!carrying)
            carriedObj = null;
    }



    // Update is called once per frame
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (carrying)
            {
                FindObjectOfType<AudioManager>().Play("DropItem");
                carriedObj.transform.parent = null;
                carriedObj.GetComponent<Rigidbody>().isKinematic = false;
                carriedObj.GetComponent<Collider>().enabled = true;
                this.gameObject.GetComponent<CapsuleCollider>().radius = 0.2f;
                carrying = false;
                carriedObj = null;
            }
            else
            {
                if (carriedObj != null)
                {
                    //hier stöhnen
                    carriedObjafterq = carriedObj;
                    carriedObj.transform.parent = this.gameObject.transform;
                    carrying = true;
                    carriedObj.GetComponent<Rigidbody>().isKinematic = true;
                    carriedObj.GetComponent<Collider>().enabled = false;
                    carriedObj.transform.position = carryPosition.position;
                    carriedObj.transform.rotation = carryPosition.rotation * Quaternion.EulerAngles(0,90,0);
                    this.gameObject.GetComponent<CapsuleCollider>().radius = 0.3f;
                }
            }
        }
    }

    public void Interact()
    {
        if (carrying)
        {
            FindObjectOfType<AudioManager>().Play("Interaction");

            carriedObj.transform.parent = null;
            carrying = false;
            carriedObj.gameObject.SetActive(false);
            carriedObj = null;
            carriedObjafterq = null;
        }
        else
        {
            Debug.LogError("Nope LoL");
        }
    }

}
