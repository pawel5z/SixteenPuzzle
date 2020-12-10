using UnityEngine;

public class BoardSounds : MonoBehaviour
{
    public AudioClip[] audioClips;

    public void PlayMovedSound()
    {
        SoundManager.instance.PlaySoundVariation(audioClips[Random.Range(0, audioClips.Length)]);
    }
}
