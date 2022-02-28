using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingBook : MonoBehaviour
{
    private Quaternion startRot;
    private Vector3 startPos;
    private Vector3 endPos;
    private float gelaufeneZeit = 0f;
    private bool abgeschossen = false;

    private void Start()
    {
        startRot = this.transform.rotation;
        startPos = this.transform.position;
        endPos = startPos;
        endPos.y = 0.5f;
    }

    // Outline aktivieren wenn Kanone drauf zielt?
    //public void outlineON()
    //{
    //    line.enabled = true;
    //}

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.name == "Cannonball(Clone)")
        {
            abgeschossen = true;
        }
    }

    private void Update()
    {
        if (abgeschossen)
        {
            this.gameObject.transform.rotation = Quaternion.Lerp(startRot, Quaternion.Euler(0, 90, -45), gelaufeneZeit);
            gelaufeneZeit += Time.deltaTime * 1.3f;
            this.gameObject.transform.position = Vector3.Lerp(startPos, endPos, gelaufeneZeit);
        }
    }
}