using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mousetrap : MonoBehaviour
{
    //public GameObject Trigger;
    private GameObject collision;
    public float Booststaerke = 10f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void boost()
    {
        if(collision != null) collision.GetComponent<Rigidbody>().AddForce(new Vector3(0, Booststaerke, 0), ForceMode.Impulse);
    }

    private void OnTriggerEnter(Collider other)
    {
        collision = other.gameObject;
    }
    private void OnTriggerExit(Collider other)
    {
        collision = null;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
