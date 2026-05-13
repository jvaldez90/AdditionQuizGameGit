using UnityEngine;

public class ButtonAudio : MonoBehaviour
{
    public AudioSource buttonSFX;

    public void PlayOnClickSound()
    {
        buttonSFX.Play();
    }
}
