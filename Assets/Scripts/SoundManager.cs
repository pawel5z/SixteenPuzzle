using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance = null;
    public AudioSource audioSource;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    public void PlaySoundVariation(AudioClip audioClip)
    {
        audioSource.clip = audioClip;
        audioSource.pitch = Random.Range(.95f, 1.05f);
        audioSource.Play();
    }
}
