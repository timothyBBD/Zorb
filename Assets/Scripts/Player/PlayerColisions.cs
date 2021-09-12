using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerColisions : MonoBehaviour
{
    string GunPartTag = "GunPart";

    private void OnTriggerEnter2D(Collider2D other)
    {
        GameObject collidedObject = other.gameObject;
        string collisionTag = other.gameObject.tag;
        if (collisionTag == GunPartTag)
        {
            int partId = collidedObject.GetComponent<GunPart>().id;
            GameObject gunPartsUI = GameObject.FindGameObjectWithTag("GunPartsUI");
            gunPartsUI.transform.GetChild(partId).gameObject.SetActive(true);
            GameState.PartsCollected[partId] = true;
            if(ShouldEndGame(GameState.PartsCollected)) {
                StartCoroutine(EndGame());
            }
        }
    }

    private bool ShouldEndGame(bool[] partsCollected)
    {
        foreach (bool collected in partsCollected)
        {
            if (!collected)
                return false;
        }
        return true;
    }


    IEnumerator EndGame()
    {
        yield return new WaitForSeconds(2f);
        float fadeTime = GameObject.FindObjectOfType<Fading>().BeginFade(1);
        yield return new WaitForSeconds(fadeTime);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

}
