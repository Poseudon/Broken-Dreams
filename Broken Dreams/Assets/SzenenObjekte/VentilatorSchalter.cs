using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;
using UnityEngine.Audio;

public class VentilatorSchalter : MonoBehaviour
{
    private GameObject player;
    private Outline outline;
    public Canvas canvas;
    public AccelerationZone ventilator;
    public BoxCollider vencol;
    public VisualEffect effect;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player 1");
        outline = transform.parent.GetComponent<Outline>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(transform.position, player.transform.position) < 2)
        {
            outline.enabled = true;
            canvas.gameObject.SetActive(true);
            if (Input.GetKey(KeyCode.E))
            {
                ventilator.GetComponent<AudioSource>().Play();

                iTween.RotateTo(this.gameObject, new Vector3(0, 0, -60), 0.3f);
                vencol.enabled = true;
                ventilator.enabled = true;
                effect.enabled = true;
                outline.enabled = false;
                canvas.gameObject.SetActive(false);
                Destroy(this.GetComponent<VentilatorSchalter>());
            }
        }
        else
        {
            outline.enabled = false;
            canvas.gameObject.SetActive(false);
        }
    }
}
