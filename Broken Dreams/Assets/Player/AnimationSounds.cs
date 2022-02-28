using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationSounds : MonoBehaviour
{
    AudioManager audioManager;

    private void Start()
    {
        audioManager = FindObjectOfType<AudioManager>();
    }

    private void PlayerFootstepSound()
    {
        audioManager.Play("PlayerRunning" + Random.Range(0, 5));
    }

    private void TeddyFootstepSound()
    {
        audioManager.Play("TeddyRunning" + Random.Range(1, 5));
    }

    private void MoustrapSnapSound()
    {
        audioManager.Play("TeddyRunning" + Random.Range(1, 5));
    }
}
