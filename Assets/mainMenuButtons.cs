using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mainMenuButtons : MonoBehaviour
{
    public GameObject title1, title2, buttonSprite, score;
    [SerializeField]
    float scaleDownTime = 1f;
    public tileSpawner tileSpawner;
    public tileMovement tileMovement;
    public backgroundTileSpawner backgroundTileSpawner;

    public void startTheGame()
    {
        LeanTween.scale(title1, Vector3.zero, scaleDownTime).setEaseInBack();
        LeanTween.scale(title2, Vector3.zero, scaleDownTime).setEaseInBack().setOnStart(tileSpawn);
        LeanTween.scale(buttonSprite, Vector3.zero, scaleDownTime).setEaseInBack().setOnComplete(closeMenu);
    }

    void closeMenu()
    {
        gameObject.SetActive(false);
    }
    void tileSpawn()
    {
        tileSpawner.spawnTiles(3, true);
        score.SetActive(true);
        score.GetComponent<Renderer>().material.SetVector("_FaceColor", tileMovement.scoreColor);
        score.transform.localScale = Vector3.zero;
        LeanTween.scale(score, Vector3.one, scaleDownTime).setEaseOutBack();
    }

}
