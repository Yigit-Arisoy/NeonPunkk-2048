using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class scoreValue : MonoBehaviour
{
    public int score;
    public float scalingTime;
    public float scoreAnimTime;
    public TextMeshProUGUI scoreText;
    public GameObject gameOverText, scoreTextObject, restartButton, restartSprite;


    private void OnEnable()
    {
        LeanTween.scale(gameOverText, new Vector3(1, 1, 1), scalingTime).setEaseOutBack().setOnComplete(scaleScoreTextObject);
    }

    void scaleScoreTextObject()
    {
        LeanTween.scale(scoreTextObject, new Vector3(1, 1, 1), scalingTime).setEaseOutBack().setOnComplete(scaleScore);
    }

    void scaleScore()
    {
        LeanTween.scale(gameObject, new Vector3(1.65f, 1.65f, 1.65f), scalingTime).setEaseOutBack().setOnComplete(scaleRestartButton);
        scoreAnimTween();
    }

    void scaleRestartButton()
    {
        LeanTween.scale(restartButton, new Vector3(2, 12, 2.6252f), scalingTime).setEaseOutBack().setOnComplete(scaleRestartSprite);
    }

    void scaleRestartSprite()
    {
        LeanTween.scale(restartSprite, new Vector3(42.0584f, 42.0584f, 42.0584f), scalingTime).setEaseOutBack();
    }

    void scoreAnimTween()
    {
        LeanTween.value(gameObject, 0, score, scoreAnimTime).setEaseOutExpo().setOnUpdate(scoreAnim);
    }

    void scoreAnim(float value)
    {
        scoreText.text = value.ToString("F0");
    }
}
