using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance = null;
    public AudioSource audioSource;
    public AudioClip launch;
    public AudioClip twinkle;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    public void PlaySoundVariation(AudioClip audioClip)
    {
        audioSource.pitch = Random.Range(.95f, 1.05f);
        audioSource.PlayOneShot(audioClip);
    }

    public void PlayWinGameSound()
    {
        StartCoroutine("PlayWinGameSoundCoro");
    }

    private IEnumerator PlayWinGameSoundCoro()
    {
        PlaySoundVariation(launch);
        yield return new WaitForSeconds(launch.length);
        PlaySoundVariation(twinkle);
    }
}
