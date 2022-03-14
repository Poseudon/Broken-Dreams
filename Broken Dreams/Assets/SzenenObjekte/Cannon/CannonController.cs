using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonController : MonoBehaviour
{
    public float rotationSpeed = 2f;
    public float BlastPower = 5;
    public bool canShoot = false;

    public GameObject Cannonball;
    public Transform ShotPoint;
    private TextClues clues;

    private void Start()
    {
        clues = FindObjectOfType<TextClues>();
    }

    //public GameObject Explosion;
    private void Update()
    {
        float HorizontalRotation = Input.GetAxis("Horizontal");
        float VericalRotation = -Input.GetAxis("Vertical");

        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles +
        new Vector3(VericalRotation * rotationSpeed, HorizontalRotation * rotationSpeed, 0));

        if (Input.GetKeyDown(KeyCode.Space) && canShoot)
        {
            FindObjectOfType<AudioManager>().Play("CannonShot");
            canShoot = false;
            GameObject CreatedCannonball = Instantiate(Cannonball, ShotPoint.position, ShotPoint.rotation);
            CreatedCannonball.transform.localScale = new Vector3(0.25f, 0.25f, 0.25f);
            CreatedCannonball.GetComponent<Rigidbody>().velocity = ShotPoint.transform.up * BlastPower;
        }
        else if (Input.GetKeyDown(KeyCode.Space) && !canShoot)
        {
            StartCoroutine(clues.TextClue("Cannon has to be loaded first"));
        }
    }
}