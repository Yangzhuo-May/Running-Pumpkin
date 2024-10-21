using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundSpawner : MonoBehaviour
{
    public GameObject groundTile;
    public GameObject BackgroundTile_1;
    public GameObject BackgroundTile_2;
    Vector3 nextSpawnPointGround;
    Vector3 nextSpawnPointBack;

    public void SpawnTile()
    {
        GameObject ground = Instantiate(groundTile, nextSpawnPointGround, Quaternion.identity);
        nextSpawnPointGround = ground.transform.GetChild(1).transform.position;

        int BGSpawnNumber = Random.Range(0, 2);
        if (BGSpawnNumber == 0)
        {
            GameObject background = Instantiate(BackgroundTile_1, nextSpawnPointBack, Quaternion.identity);
            nextSpawnPointBack = ground.transform.GetChild(9).transform.position;
        }
        else if (BGSpawnNumber == 1)
        {
            GameObject background = Instantiate(BackgroundTile_2, nextSpawnPointBack, Quaternion.identity);
            nextSpawnPointBack = ground.transform.GetChild(9).transform.position;
        }
    }

    private void Start()
    {
        for (int i = 0; i < 5; i++)
        {
            SpawnTile();
        }
    }
}
