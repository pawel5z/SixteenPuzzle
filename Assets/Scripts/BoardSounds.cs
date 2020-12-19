using UnityEngine;

public class BoardSounds : MonoBehaviour
{
    public AudioClip[] movedPieceClips;
    public AudioClip completedFragmentClip;

    public void PlayMovedSound()
    {
        SoundManager.instance.PlaySoundVariation(movedPieceClips[Random.Range(0, movedPieceClips.Length)]);
    }

    public void PlayCompletedFragmentClip()
    {
        SoundManager.instance.PlaySoundVariation(completedFragmentClip);
    }
}
