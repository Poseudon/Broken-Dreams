using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathBorder : MonoBehaviour
{
    private ScreenFade fade;
    // more respawn points were planned but time ran out
    public List<Transform> RespawnPoints = new List<Transform>();
    public GameObject player;
    public GameObject bear;


    private void Start()
    {
        fade = FindObjectOfType<ScreenFade>();    
    }

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

    private IEnumerator playerSpawn()
    {
        yield return new WaitForSeconds(2f);
        player.gameObject.transform.position = RespawnPoints[0].position;
        fade.live();
    }
    private IEnumerator bearSpawn()
    {
        yield return new WaitForSeconds(2f);
        bear.gameObject.transform.position = RespawnPoints[0].position;
        fade.live();
    }
}