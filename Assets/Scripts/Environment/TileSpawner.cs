using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileSpawner : MonoBehaviour
{
    [SerializeField] public GroundTile groundTile;
    [SerializeField] public BackgroundTile[] backgroundTiles;

    private Vector3 nextSpawnPointGround;
    private Vector3 nextSpawnPointBack;

    private void Start()
    {
        nextSpawnPointGround = new Vector3(0, 0, 15);
        nextSpawnPointBack = new Vector3(0, 0, 15);

        for (int i = 0; i < 5; i++)
        {
            SpawnTile();
        }
    }

    public void SpawnTile()
    {
        GameObject ground = groundTile.CreateItems(nextSpawnPointGround);
        nextSpawnPointGround = ground.transform.GetChild(1).transform.position;

        int index = Random.Range(0, 2); 
        GameObject background = backgroundTiles[index].CreateItems(nextSpawnPointBack);
        nextSpawnPointBack = ground.transform.GetChild(9).transform.position;
    }
}


