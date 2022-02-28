using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    private Material material;
    private MeshRenderer meshRenderer;
    private GameObject player;
    private Vector3 playerPosition;
    private float distanceToPlayer;
    private float alphaValue = 1;

    public float invisibleDistance = 5;
    public float visibleDistance = 20;

    private void Start()
    {
        player = GameObject.Find("Player 1");
        playerPosition = player.transform.position;

        distanceToPlayer = Vector3.Distance(this.transform.position, playerPosition);

        material = new Material(GetComponent<MeshRenderer>().sharedMaterial);
        meshRenderer = GetComponent<MeshRenderer>();
        meshRenderer.sharedMaterial = material;

        setAlpha();
    }

    private void setAlpha()
    {
        if(distanceToPlayer <= invisibleDistance)
        {
            material.SetFloat("_Distance", 0);
        }
        else if(distanceToPlayer >= visibleDistance)
        {
            material.SetFloat("_Distance", 1);
        }
        else
        {
            alphaValue = (distanceToPlayer - invisibleDistance) / (visibleDistance - invisibleDistance);
            Debug.Log(alphaValue);
            material.SetFloat("_Distance", alphaValue);
        }
    }

    private void Update()
    {
        playerPosition = player.transform.position;
        distanceToPlayer = Vector3.Distance(this.transform.position, playerPosition);
        //Debug.Log(distanceToPlayer);
        setAlpha();
    }
}
