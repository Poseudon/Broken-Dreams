using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainPathAnimation : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

        iTween.MoveTo(gameObject, iTween.Hash(
        "path", iTweenPath.GetPath("TrainPath"),
        "speed", 3,
        "easetype", "easeInOutQuad",
        "looptype", "loop",
        "orienttopath", true)
        );
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
