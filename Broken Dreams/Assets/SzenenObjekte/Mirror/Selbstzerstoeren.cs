using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Selbstzerstoeren : MonoBehaviour
{
    public float SecondsBeforeFadeOut = 10.0f; 
    private Material material;
    private MeshRenderer meshRenderer;
    private float dissolveState = 0.0f;
    private bool startfertig = false;
    private float time = 0.0f;

    private IEnumerator Start()
    {
        material = new Material(GetComponent<MeshRenderer>().sharedMaterial);
        meshRenderer = GetComponent<MeshRenderer>();
        meshRenderer.sharedMaterial = material;
        yield return new WaitForSeconds(SecondsBeforeFadeOut);
        startfertig = true;
    }

    private void Update()
    {
        if(startfertig)
        {
            dissolveState = Mathf.Lerp(0.0f, 1.0f, 0.25f * time);
            material.SetFloat("_Dissolve_State", dissolveState);
            time += Time.deltaTime;

            if (dissolveState == 1)
            {
                Destroy(this.gameObject);
            }
        }
    }
}
