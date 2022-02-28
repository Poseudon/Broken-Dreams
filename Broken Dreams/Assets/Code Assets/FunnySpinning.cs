using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FunnySpinning : MonoBehaviour
{
    [Range(-0.5f,0.5f)]
    public float SpinningSpeed = 0.01f;

    // Update is called once per frame
    void Update()
    {
        this.transform.Rotate(0f, SpinningSpeed, 0f);
    }
}
