using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Credits : MonoBehaviour
{
    Image image;
    public Fading fading;

    public Sprite secondScreen;

    void Awake()
    {
        image = GetComponent<Image>();
    }

    void Start()
    {
        StartCoroutine(SwitchScreen());
    }

    IEnumerator SwitchScreen()
    {
        yield return new WaitForSeconds(2);
        StartCoroutine(FadeOut());
    }

    IEnumerator FadeIn()
    {
        float fadeTime = fading.BeginFade(-1);
        yield return new WaitForSeconds(fadeTime);
    }

    IEnumerator FadeOut()
    {
        float fadeTime = fading.BeginFade(1);
        yield return new WaitForSeconds(fadeTime);
        image.sprite = secondScreen;
        StartCoroutine(FadeIn());
    }

}
