using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger : MonoBehaviour
{
    private bool gedrueckt = false;
    public Mousetrap Mousetrap;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    //private void OnMouseDown()
    //{
    //    gedrueckt=true;
    //}

    //private void OnMouseOver()
    //{
    //    if (Input.GetMouseButtonDown(0))
    //    {
    //        gedrueckt = true;
    //        Debug.Log("true");
    //    }
    //    else gedrueckt = false;
    //}
    //private void OnMouseExit()
    //{
    //    gedrueckt = false;
    //}

    private void OnMouseDown()
    {
        Mousetrap.boost();
    }

    public bool getgedrueckt()
    {
        return gedrueckt;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
