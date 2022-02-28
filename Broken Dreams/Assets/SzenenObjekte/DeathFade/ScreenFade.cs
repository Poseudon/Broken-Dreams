using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScreenFade : MonoBehaviour
{
    private bool dead = false;
    public float alpha;
    public DeathBorder border;
    public GameObject text;
    public GameObject button;

    public void death()
    {
        FindObjectOfType<AudioManager>().Play("DeathScream");
        dead = true;
    }   
    
    public void live()
    {
        dead = false;
    }

    public void win()
    {
        
        dead = true;
        text.SetActive(true);
        button.SetActive(true);
    }

    private void Update()
    {
        if (dead && alpha < 3)
        {
            alpha += 0.012f;
            this.gameObject.GetComponent<Image>().color = new Vector4(0, 0, 0, alpha);
        }
        else if (!dead && alpha > 0)
        {
            alpha -= 0.012f;
            this.gameObject.GetComponent<Image>().color = new Vector4(0, 0, 0, alpha);
        }
    }
}