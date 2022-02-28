using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pickupborderline : MonoBehaviour
{
    private GameObject player;
    private Outline outline;
    public Canvas canvas;
    private PickUp pickup;
    public float distance = 2;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player 1");
        outline = transform.parent.GetComponent<Outline>();
        pickup = player.GetComponent<PickUp>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Vector3.Distance(transform.position, player.transform.position) < distance && pickup.carriedObjafterq != transform.parent.gameObject)
        {
            outline.enabled = true;
            canvas.gameObject.SetActive(true);
        }
        else
        {
            outline.enabled = false;
            canvas.gameObject.SetActive(false);
        }
    }
}
