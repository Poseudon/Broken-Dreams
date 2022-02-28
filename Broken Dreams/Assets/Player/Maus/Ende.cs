using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ende : MonoBehaviour
{
    public ScreenFade screenFade;
    private void Start()
    {
        screenFade = FindObjectOfType<ScreenFade>();
    }

    private void OnTriggerEnter(Collider other)
    {
        screenFade.win();
    }
}
