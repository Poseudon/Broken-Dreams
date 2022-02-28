using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class Timer : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(pTimer());
    }

    public IEnumerator pTimer()
    {
        yield return new WaitForSeconds(0.4f);
        this.GetComponent<VisualEffect>().Stop();
        yield return new WaitForSeconds(1);
        Destroy(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
