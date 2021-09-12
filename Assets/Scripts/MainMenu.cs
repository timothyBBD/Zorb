using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public Fading fading;
    PlayerControls controls;

    private void Awake()
    {
        controls = new PlayerControls();
        controls.UI.Submit.performed += _ => StartCoroutine(StartGame());
        controls.Enable();
    }
    
    IEnumerator StartGame()
    {
        float fadeTime = GameObject.FindObjectOfType<Fading>().BeginFade(1);
        yield return new WaitForSeconds(fadeTime);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

    }

}
