using System;
using UnityEngine;

[Serializable]
public class SoundEffect
{
    public AudioSource audioSource;
    public AudioClip soundClip;
    public bool sourceOnObject = true;

    public float[] pitchRange = new float[] { 0.8f, 1.2f };

    public void PlaySound(GameObject gameObject)
    {
        audioSource = sourceOnObject ? gameObject.GetComponent<AudioSource>() : audioSource;
        audioSource.pitch = UnityEngine.Random.Range(pitchRange[0], pitchRange[1]);
        audioSource.PlayOneShot(soundClip);
    }

}