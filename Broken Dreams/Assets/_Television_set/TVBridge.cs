using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TVBridge : MonoBehaviour
{
    private float gelaufeneZeit = 0f;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

        this.gameObject.transform.localPosition = Vector3.Lerp(this.gameObject.transform.localPosition, new Vector3(0, 1.12f, 4.3f), gelaufeneZeit);
        this.gameObject.transform.localScale = Vector3.Lerp(this.gameObject.transform.localScale, new Vector3(9.9f, 2.29f, 1.27f), gelaufeneZeit);
        gelaufeneZeit += Time.deltaTime * 0.3f;
    }
}
