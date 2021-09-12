using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadLevel : MonoBehaviour
{
    public Fading fading;

    private void Awake() {
        StartCoroutine(FadeIn());
    }
    IEnumerator FadeIn()
    {
        float fadeTime = fading.BeginFade(-1);
        yield return new WaitForSeconds(fadeTime);
    }
}
