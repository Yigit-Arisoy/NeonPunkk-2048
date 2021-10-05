using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using Random = UnityEngine.Random;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class tileMovement : MonoBehaviour
{

    // y 2.4 x 3.3
    // x -1.7 x 1.7

    public tileSpawner tileSpawner;
    public int destinationX, destinationY;
    public int[,] testArray;
    bool spawn_a_Tile = false;
    public float tileMoveTime;
    public TextMeshPro score, multiplierPrefab;
    public int scoreMultiplier = 0;
    public GameObject gameOverUI;
    public float tileCount;
    public scoreValue scoreValue;
    public AudioManager audioManager;

    [ColorUsageAttribute(true, true, 0f, 8f, 0.125f, 3f)]
    public Color[] tileColors;

    [ColorUsageAttribute(true, true, 0f, 8f, 0.125f, 3f)]
    public Color scoreColor,multiplierColor;

    [ColorUsageAttribute(true, true, 0f, 8f, 0.125f, 3f)]
    public Color[] tileColorsGlow;

    [ColorUsageAttribute(true, true, 0f, 8f, 0.125f, 3f)]
    public Color[] tileColors2;

    [ColorUsageAttribute(true, true, 0f, 8f, 0.125f, 3f)]
    public Color[] tileColorsGlow2, tileTextColorsGlow2;



    [ColorUsageAttribute(true, true, 0f, 8f, 0.125f, 3f)]
    public Color[] backgroundTileColors;

    [ColorUsageAttribute(true, true, 0f, 8f, 0.125f, 3f)]
    public Color[] backgroundTileColorsGlow;

    public Volume volumeFx, volumeFx2;

    public GameObject darkenedScreen;

    
    // 2 2 2 2
    // 0 0 0 0
    // 0 0 0 0
    // 8 4 4 2

    public Color pickColor(Color currentColor)
    {
        int currentIndex = Array.IndexOf(tileColors2, currentColor);
        int nextIndex;
        
        if (currentIndex == tileColors2.Length - 1)
            nextIndex = 0;

        else
            nextIndex = currentIndex + 1;

        return tileColors2[nextIndex];
    }

    public Color pickGlowColor(Color currentColor)
    {
        int currentIndex = Array.IndexOf(tileColors2, currentColor);
        int nextIndex;

        if (currentIndex == tileColors2.Length - 1)
            nextIndex = 0;

        else
            nextIndex = currentIndex + 1;

        return tileColorsGlow2[nextIndex];
    }

    public Color pickTextGlowColor(Color currentColor)
    {
        int currentIndex = Array.IndexOf(tileColors2, currentColor);
        int nextIndex;

        if (currentIndex == tileColors2.Length - 1)
            nextIndex = 0;

        else
            nextIndex = currentIndex + 1;

        return tileTextColorsGlow2[nextIndex];
    }

    public void changeTileColor(GameObject theTile)
    {
        Color nextColor = pickColor(theTile.GetComponent<Renderer>().material.GetColor("tileColor"));
        Color nextGlowColor = pickGlowColor(theTile.GetComponent<Renderer>().material.GetColor("tileColor"));
        Color nextTextGlowColor = pickTextGlowColor(theTile.GetComponent<Renderer>().material.GetColor("tileColor"));

        theTile.GetComponent<Renderer>().material.SetColor("tileColor", nextColor);
        theTile.transform.Find("outerGlow").GetComponent<Renderer>().material.SetColor("_EmissionColor", nextGlowColor);
        theTile.transform.Find("outerGlow").GetComponent<Renderer>().material.SetVector("_BaseColor", nextGlowColor);
        theTile.transform.Find("tileValueText").GetComponent<Renderer>().material.SetVector("_FaceColor", nextTextGlowColor);
    }

    public void checkGameOver()
    {
        tileCount = 0;
        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                if (tileSpawner.createdTiles[i, j] == null)
                    continue;

                tileCount += 1;
            }
        }
        if (tileCount == 16)
            gameOver();
    }
    public void gameOver()
    {

        if(volumeFx2.profile.TryGet(out DepthOfField depthOfField))
        {
            depthOfField.active = true;
            volumeFx2.GetComponent<Animator>().SetBool("isGameOver", true);
            volumeFx.GetComponent<Animator>().SetBool("isGameOver", true);
        }
        scoreValue.score = int.Parse(score.text);

        darkenedScreen.SetActive(true);
        gameOverUI.SetActive(true);
        gameObject.SetActive(false);
    }

    public void addScore(int multiplier, int tileValue)
    {
        score.text = (int.Parse(score.text) + multiplier * tileValue).ToString();

        if(multiplier > 1)
        {
            float yPos = Random.Range(2.8f, 3.3f);
            float xPos = Random.Range(-1.7f, 1.7f);
            Vector2 multiplierTextPos = new Vector2(xPos, yPos);

            multiplierPrefab.text += multiplier.ToString();
            multiplierPrefab.fontSize = 0.1f;
            

            TextMeshPro theMultiplier = Instantiate(multiplierPrefab, multiplierTextPos, Quaternion.identity);
            theMultiplier.GetComponent<Renderer>().material.SetVector("_FaceColor", multiplierColor);

            multiplierPrefab.text = "x";
        }
    }
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.A))
        {
            scoreMultiplier = 0;
            spawn_a_Tile = false;
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    if(tileSpawner.createdTiles[i, j] != null)
                    {
                        destinationX = j;
                        destinationY = i;

                        for (int k = j - 1; k >= 0; k--)
                        {
                            if (tileSpawner.createdTiles[i, k] == null)
                            {
                                destinationX = k;
                                destinationY = i;
                            }

                            if (tileSpawner.createdTiles[i, k] != null)
                            {
                                if (tileSpawner.createdTiles[i, k].GetComponent<tileData>().tileValue == tileSpawner.createdTiles[i, j].GetComponent<tileData>().tileValue)
                                {

                                    Destroy(tileSpawner.createdTiles[i, k]);
                                    tileSpawner.createdTiles[i, k] = null;

                                    destinationX = k;
                                    destinationY = i;

                                    changeTileColor(tileSpawner.createdTiles[i, j]);

                                    tileSpawner.createdTiles[i, j].GetComponent<tileData>().tileValue *= 2;
                                    tileSpawner.createdTiles[i, j].GetComponent<tileData>().tileValueText.text = tileSpawner.createdTiles[i, j].GetComponent<tileData>().tileValue.ToString();
                                    scoreMultiplier += 1;

                                    addScore(scoreMultiplier, tileSpawner.createdTiles[i, j].GetComponent<tileData>().tileValue);
                                    audioManager.playSoundFx(audioManager.mergeClip);

                                }
                                else
                                    break;

                            }
                        }
                        LeanTween.move(tileSpawner.createdTiles[i, j], tileSpawner.tilePositions[destinationY, destinationX], tileMoveTime).setEase(LeanTweenType.easeOutExpo);
                        tileSpawner.createdTiles[destinationY, destinationX] = tileSpawner.createdTiles[i, j];

                        if (i != destinationY || j != destinationX)
                        {
                            tileSpawner.createdTiles[destinationY, destinationX] = tileSpawner.createdTiles[i, j];
                            tileSpawner.createdTiles[i, j] = null;
                            spawn_a_Tile = true;
                        }
                           
                    }
                }
            }
            if (spawn_a_Tile)
                tileSpawner.spawnTiles(1);

            checkGameOver();

        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            scoreMultiplier = 0;
            spawn_a_Tile = false;
            for (int i = 0; i < 4; i++)
            {
                for (int j = 3; j >= 0; j--)
                {
                    if (tileSpawner.createdTiles[i, j] != null)
                    {
                        destinationX = j;
                        destinationY = i;

                        for (int k = j + 1; k < 4; k++)
                        {
                            if (tileSpawner.createdTiles[i, k] == null)
                            {
                                destinationX = k;
                                destinationY = i;
                            }

                            if (tileSpawner.createdTiles[i, k] != null)
                            {
                                if (tileSpawner.createdTiles[i, k].GetComponent<tileData>().tileValue == tileSpawner.createdTiles[i, j].GetComponent<tileData>().tileValue)
                                {
                                    Destroy(tileSpawner.createdTiles[i, k]);

                                    destinationX = k;
                                    destinationY = i;

                                    changeTileColor(tileSpawner.createdTiles[i, j]);

                                    tileSpawner.createdTiles[i, j].GetComponent<tileData>().tileValue *= 2;
                                    tileSpawner.createdTiles[i, j].GetComponent<tileData>().tileValueText.text = tileSpawner.createdTiles[i, j].GetComponent<tileData>().tileValue.ToString();
                                    scoreMultiplier += 1;

                                    addScore(scoreMultiplier, tileSpawner.createdTiles[i, j].GetComponent<tileData>().tileValue);
                                    audioManager.playSoundFx(audioManager.mergeClip);
                                }
                                else
                                    break;
                            }
                        }

                        LeanTween.move(tileSpawner.createdTiles[i, j], tileSpawner.tilePositions[destinationY, destinationX], tileMoveTime).setEase(LeanTweenType.easeOutExpo);
                        tileSpawner.createdTiles[destinationY, destinationX] = tileSpawner.createdTiles[i, j];

                        if (i != destinationY || j != destinationX)
                        {
                            tileSpawner.createdTiles[i, j] = null;
                            spawn_a_Tile = true;
                        }
                    }
                }
            }
            if (spawn_a_Tile)
                tileSpawner.spawnTiles(1);

            checkGameOver();
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            scoreMultiplier = 0;
            spawn_a_Tile = false;
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    if (tileSpawner.createdTiles[i, j] != null)
                    {
                        destinationX = j;
                        destinationY = i;

                        for (int k = i - 1; k >= 0; k--)
                        {
                            if (tileSpawner.createdTiles[k, j] == null)
                            {
                                destinationX = j;
                                destinationY = k;
                            }

                            if (tileSpawner.createdTiles[k, j] != null)
                            {
                                if (tileSpawner.createdTiles[k, j].GetComponent<tileData>().tileValue == tileSpawner.createdTiles[i, j].GetComponent<tileData>().tileValue)
                                {
                                    Destroy(tileSpawner.createdTiles[k, j]);

                                    destinationX = j;
                                    destinationY = k;

                                    changeTileColor(tileSpawner.createdTiles[i, j]);

                                    tileSpawner.createdTiles[i, j].GetComponent<tileData>().tileValue *= 2;
                                    tileSpawner.createdTiles[i, j].GetComponent<tileData>().tileValueText.text = tileSpawner.createdTiles[i, j].GetComponent<tileData>().tileValue.ToString();
                                    scoreMultiplier += 1;

                                    addScore(scoreMultiplier, tileSpawner.createdTiles[i, j].GetComponent<tileData>().tileValue);
                                    audioManager.playSoundFx(audioManager.mergeClip);
                                }
                                else
                                    break;
                            }
                           
                        }

                        LeanTween.move(tileSpawner.createdTiles[i, j], tileSpawner.tilePositions[destinationY, destinationX], tileMoveTime).setEase(LeanTweenType.easeOutExpo);
                        tileSpawner.createdTiles[destinationY, destinationX] = tileSpawner.createdTiles[i, j];

                        if (i != destinationY || j != destinationX)
                        {
                            tileSpawner.createdTiles[i, j] = null;
                            spawn_a_Tile = true;
                        }
                    }
                }
            }
            if (spawn_a_Tile)
                tileSpawner.spawnTiles(1);

            checkGameOver();
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            scoreMultiplier = 0;
            spawn_a_Tile = false;
            for (int i = 3; i >= 0; i--)
            {
                for (int j = 0; j < 4; j++)
                {
                    if (tileSpawner.createdTiles[i, j] != null)
                    {
                        destinationX = j;
                        destinationY = i;

                        for (int k = i + 1; k < 4; k++)
                        {
                            if (tileSpawner.createdTiles[k, j] == null)
                            {
                                destinationX = j;   
                                destinationY = k;
                            }

                            if (tileSpawner.createdTiles[k, j] != null)
                            {
                                if (tileSpawner.createdTiles[k, j].GetComponent<tileData>().tileValue == tileSpawner.createdTiles[i, j].GetComponent<tileData>().tileValue)
                                {
                                    Destroy(tileSpawner.createdTiles[k, j]);

                                    destinationX = j;
                                    destinationY = k;

                                    changeTileColor(tileSpawner.createdTiles[i, j]);

                                    tileSpawner.createdTiles[i, j].GetComponent<tileData>().tileValue *= 2;
                                    tileSpawner.createdTiles[i, j].GetComponent<tileData>().tileValueText.text = tileSpawner.createdTiles[i, j].GetComponent<tileData>().tileValue.ToString();
                                    scoreMultiplier += 1;

                                    addScore(scoreMultiplier, tileSpawner.createdTiles[i, j].GetComponent<tileData>().tileValue);
                                    audioManager.playSoundFx(audioManager.mergeClip);
                                }
                                else
                                    break;
                            }                 
                        }

                        LeanTween.move(tileSpawner.createdTiles[i, j], tileSpawner.tilePositions[destinationY, destinationX], tileMoveTime).setEase(LeanTweenType.easeOutExpo);
                        tileSpawner.createdTiles[destinationY, destinationX] = tileSpawner.createdTiles[i, j];

                        if (i != destinationY || j != destinationX)
                        {
                            tileSpawner.createdTiles[i, j] = null;
                            spawn_a_Tile = true;
                        }                      
                    }
                }
            }
            if(spawn_a_Tile)
                tileSpawner.spawnTiles(1);

            checkGameOver();
        }
    }
}
