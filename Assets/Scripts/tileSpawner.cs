using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class tileSpawner : MonoBehaviour
{

    public Vector2 [,] tilePositions = new Vector2[4, 4];
    public Vector2 [,] backgroundTilePositions = new Vector2[4, 4];
    int iIndex, jIndex;
    public int startTileCount;
    public GameObject tile, backgroundTile;
    public GameObject[,] createdTiles = new GameObject[4, 4];
    public GameObject[,] createdBackgroundTiles = new GameObject[4, 4];
    public GameObject[] firstLine = new GameObject[4];
    public GameObject[] secondLine = new GameObject[4];
    public GameObject[] thirdLine = new GameObject[4];
    public GameObject[] fourthLine = new GameObject[4];

    public GameObject[] BackgroundfirstLine = new GameObject[4];
    public GameObject[] BackgroundsecondLine = new GameObject[4];
    public GameObject[] BackgroundthirdLine = new GameObject[4];
    public GameObject[] BackgroundfourthLine = new GameObject[4];

    public tileMovement tileMovement;

    public GameObject positionTiles, backgroundPositionTiles;
    void Start()
    {
        for (int i = 0; i < 4; i++)
        {
            tilePositions[0, i] = positionTiles.transform.GetChild(i).position;
        }

        for (int i = 0; i < 4; i++)
        {
            tilePositions[1, i] = positionTiles.transform.GetChild(i + 4).position;
        }

        for (int i = 0; i < 4; i++)
        {
            tilePositions[2, i] = positionTiles.transform.GetChild(i + 8).position;
        }

        for (int i = 0; i < 4; i++)
        {
            tilePositions[3, i] = positionTiles.transform.GetChild(i + 12).position;
        }


        for (int i = 0; i < 4; i++)
        {
            backgroundTilePositions[0, i] = backgroundPositionTiles.transform.GetChild(i).position;
        }

        for (int i = 0; i < 4; i++)
        {
            backgroundTilePositions[1, i] = backgroundPositionTiles.transform.GetChild(i + 4).position;
        }

        for (int i = 0; i < 4; i++)
        {
            backgroundTilePositions[2, i] = backgroundPositionTiles.transform.GetChild(i + 8).position;
        }

        for (int i = 0; i < 4; i++)
        {
            backgroundTilePositions[3, i] = backgroundPositionTiles.transform.GetChild(i + 12).position;
        }
    }

    void Update()
    {
        for (int i = 0; i < 4; i++)
        {
            firstLine[i]  = createdTiles[0, i];
            secondLine[i] = createdTiles[1, i];
            thirdLine[i]  = createdTiles[2, i];
            fourthLine[i] = createdTiles[3, i];
        }

        for (int i = 0; i < 4; i++)
        {
            BackgroundfirstLine[i]  = createdBackgroundTiles[0, i];
            BackgroundsecondLine[i] = createdBackgroundTiles[1, i];
            BackgroundthirdLine[i]  = createdBackgroundTiles[2, i];
            BackgroundfourthLine[i] = createdBackgroundTiles[3, i];
        }
    }
    // TilePrefab scale = 0.85 0.85
    // outer Glow scale = 1.1 1.1
    // value text scale = 0.2 0.2
    public void spawnTiles(int tileCount, bool firstSpawn = false)
    {

        if(firstSpawn)
        {
            while (tileCount > 0)
            {
                iIndex = (Random.Range(0, 4));
                jIndex = (Random.Range(0, 4));

                if (createdTiles[iIndex, jIndex] == null)
                {
                    Vector2 spawningPos = new Vector2(tilePositions[iIndex, jIndex].x, tilePositions[iIndex, jIndex].y);
                    createdTiles[iIndex, jIndex] = Instantiate(tile, spawningPos, Quaternion.identity);
                    createdTiles[iIndex, jIndex].GetComponent<Renderer>().material.SetColor("tileColor", tileMovement.tileColors2[0]);

                    createdTiles[iIndex, jIndex].transform.Find("outerGlow").GetComponent<Renderer>().material.SetColor("_EmissionColor", tileMovement.tileColorsGlow2[0]);
                    createdTiles[iIndex, jIndex].transform.Find("outerGlow").GetComponent<Renderer>().material.SetVector("_BaseColor", tileMovement.tileColorsGlow2[0]);

                    createdTiles[iIndex, jIndex].transform.Find("tileValueText").GetComponent<Renderer>().material.SetVector("_FaceColor", tileMovement.tileTextColorsGlow2[0]);

                    createdTiles[iIndex, jIndex].transform.localScale = Vector3.zero;
                    createdTiles[iIndex, jIndex].transform.Find("outerGlow").localScale = Vector3.zero;
                    createdTiles[iIndex, jIndex].transform.Find("tileValueText").localScale = Vector3.zero;

                    LeanTween.scale(createdTiles[iIndex, jIndex].transform.Find("outerGlow").gameObject, new Vector3(0.85f, 0.85f, 1), .5f).setEaseOutBack();
                    LeanTween.scale(createdTiles[iIndex, jIndex].transform.Find("outerGlow").gameObject, new Vector3(1.1f, 1.1f, 1), .5f).setEaseOutBack();
                    LeanTween.scale(createdTiles[iIndex, jIndex].transform.Find("tileValueText").gameObject, new Vector3(0.2f, 0.2f, 1), .5f).setEaseOutBack();

                    tileCount--;
                }
            }
        }
        while (tileCount > 0)
        {
            iIndex = (Random.Range(0, 4));
            jIndex = (Random.Range(0, 4));

            if (createdTiles[iIndex, jIndex] == null)
            {
                Vector2 spawningPos = new Vector2(tilePositions[iIndex, jIndex].x, tilePositions[iIndex, jIndex].y);
                createdTiles[iIndex, jIndex] = Instantiate(tile, spawningPos, Quaternion.identity);
                createdTiles[iIndex, jIndex].GetComponent<Renderer>().material.SetColor("tileColor", tileMovement.tileColors2[0]);

                createdTiles[iIndex, jIndex].transform.Find("outerGlow").GetComponent<Renderer>().material.SetColor("_EmissionColor", tileMovement.tileColorsGlow2[0]);
                createdTiles[iIndex, jIndex].transform.Find("outerGlow").GetComponent<Renderer>().material.SetVector("_BaseColor", tileMovement.tileColorsGlow2[0]);

                createdTiles[iIndex, jIndex].transform.Find("tileValueText").GetComponent<Renderer>().material.SetVector("_FaceColor", tileMovement.tileTextColorsGlow2[0]);


                tileCount--;
            }
        }
    }

    public void spawnBackgroundTiles(int tileCount)
    {
        while (tileCount > 0)
        {
            iIndex = (Random.Range(0, 4));
            jIndex = (Random.Range(0, 4));

            if (createdBackgroundTiles[iIndex, jIndex] == null)
            {
                Vector2 spawningPos = new Vector2(backgroundTilePositions[iIndex, jIndex].x, backgroundTilePositions[iIndex, jIndex].y);
                createdBackgroundTiles[iIndex, jIndex] = Instantiate(backgroundTile, spawningPos, Quaternion.identity);

                int colorIndex = Random.Range(0, 4);
                createdBackgroundTiles[iIndex, jIndex].GetComponent<Renderer>().material.SetColor("tileColor", tileMovement.backgroundTileColors[colorIndex]);

                createdBackgroundTiles[iIndex, jIndex].transform.Find("outerGlow").GetComponent<Renderer>().material.SetColor("_EmissionColor", tileMovement.backgroundTileColorsGlow[colorIndex]);
                createdBackgroundTiles[iIndex, jIndex].transform.Find("outerGlow").GetComponent<Renderer>().material.SetVector("_BaseColor", tileMovement.backgroundTileColorsGlow[colorIndex]);

                tileCount--;
            }
        }
    }
}
