using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mousehole : MonoBehaviour
{
    public GameObject Brocken;
    public GameObject Plane;
    // Update is called once per frame
    private void OnTriggerEnter(Collider other)
    {
        Destroy(Plane);
        Destroy(gameObject);
       
    }

    private void OnDestroy()
    {
        Instantiate(Brocken);
    }
}
