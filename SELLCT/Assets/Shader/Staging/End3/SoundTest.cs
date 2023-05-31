using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundTest : MonoBehaviour
{
    public AudioClip soundEffect;  // çƒê∂Ç∑ÇÈå¯â âπÇÃAudioClip

    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlaySoundEffect()
    {
        audioSource.PlayOneShot(soundEffect);
    }
}
