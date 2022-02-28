using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class Kwispezial : MonoBehaviour
{
    private GameObject Player;
    public VideoPlayer vid;

    // Start is called before the first frame update
    private void Start()
    {
        Player = GameObject.Find("Player 1");
       
    }

    // Update is called once per frame
    private void Update()
    {
        if(Vector3.Distance(transform.position,Player.transform.position)<=1.5f)
        {
            if(Input.GetKeyDown(KeyCode.E))
            {
                vid.Play();
            }
        }
    }
}
