using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathBorder : MonoBehaviour
{
    public ScreenFade fade;
    public List<Transform> RespawnPoints = new List<Transform>();
    public GameObject player;
    public GameObject bear;
    private int counter = 0;


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            fade.death();
            StartCoroutine(playerSpawn());
        }
        else if (other.gameObject.tag == "Teddy")
        {
            fade.death();
            StartCoroutine(bearSpawn());
        }
    }

    public void nextStage()
    {
        counter++;
    }

    IEnumerator playerSpawn()
    {
        yield return new WaitForSeconds(2f);
        player.gameObject.transform.position = RespawnPoints[0].position;
        fade.live();
    }
    IEnumerator bearSpawn()
    {
        yield return new WaitForSeconds(2f);
        bear.gameObject.transform.position = RespawnPoints[0].position;
        fade.live();
    }
}