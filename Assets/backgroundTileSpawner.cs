using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class backgroundTileSpawner : MonoBehaviour
{
    [SerializeField]
    tileSpawner tileSpawner;
    public float tileMoveSpeed;
    void Start()
    {
        StartCoroutine(spawnBackgroundTiles());
        StartCoroutine(moveBackgroundTiles());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator spawnBackgroundTiles()
    {
        while(true)
        {
            tileSpawner.spawnBackgroundTiles(3);
            yield return new WaitForSecondsRealtime(3);
        }
    }

    IEnumerator moveBackgroundTiles()
    {
        while(true)
        {
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    if (tileSpawner.createdBackgroundTiles[i, j] != null)
                    {
                        tileSpawner.createdBackgroundTiles[i, j].transform.position += Vector3.right * tileMoveSpeed * Time.deltaTime;

                        if (tileSpawner.createdBackgroundTiles[i, j].transform.position.x > 4)
                        {
                            Destroy(tileSpawner.createdBackgroundTiles[i, j]);
                            tileSpawner.createdBackgroundTiles[i, j] = null;
                        }       
                    }      
                }
            }
            yield return new WaitForEndOfFrame();
        }
    }

    public IEnumerator destroyBackgroundTile()
    {
        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                if (tileSpawner.createdBackgroundTiles[i, j] != null)
                {
                    tileSpawner.createdBackgroundTiles[i, j].GetComponent<Animator>().SetBool("isDestroyed", true);
                }
            }
        }

        yield return new WaitForSecondsRealtime(1f);

        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                if (tileSpawner.createdBackgroundTiles[i, j] != null)
                {
                    Destroy(tileSpawner.createdBackgroundTiles[i, j]);
                    tileSpawner.createdBackgroundTiles[i, j] = null;
                }
            }
        }
        gameObject.SetActive(false);
    }

    public void destroyTiles()
    {
        StartCoroutine(destroyBackgroundTile());
    }
}
