using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadLevel : MonoBehaviour
{
    public Fading fading;

    GameObject gunParts;

    private void Awake()
    {
        StartCoroutine(FadeIn());
        gunParts = GameObject.FindGameObjectWithTag("GunParts");
        for (int i = 0; i < GameState.PartsCollected.Length; i++)
        {
            gunParts.transform.GetChild(i).gameObject.SetActive(GameState.PartsCollected[i]);
        }
    }
    IEnumerator FadeIn()
    {
        float fadeTime = fading.BeginFade(-1);
        yield return new WaitForSeconds(fadeTime);
    }
}
