using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Triggerbox : MonoBehaviour
{
    private GameObject collision;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Battery")
        {
            collision = other.gameObject;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.name == "Battery")
        {
            collision = null;
        }
    }

    public GameObject getcollisonobject()
    {

        return collision;
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(collision.name);
    }
}
