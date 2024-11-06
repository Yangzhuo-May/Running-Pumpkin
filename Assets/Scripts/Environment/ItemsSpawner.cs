using Unity.VisualScripting;
using System.Collections.Generic;
using UnityEngine;

public class ItemsSpawner : MonoBehaviour
{
    TileSpawner tileSpawner;
    private bool hasSpawned = false;
    private bool isJumpObstacle = false;

    public int numberOfCoins = 6;
    [SerializeField] public Coin coin;
    [SerializeField] public Obstacle[] obstacles;

    private Dictionary<int, string> coinsPosition = new Dictionary<int, string>
    {
        { 0, "CoinJumpLeft" },
        { 1, "CoinJumpRight" },
        { 2, "CoinLeft" },
        { 3, "CoinRight" }
    };

    // Start is called before the first frame update
    void Start()
    {
        tileSpawner = GameObject.FindObjectOfType<TileSpawner>();
        SpawnObstacle();
        SpawnCoin();
    }

    private void OnTriggerExit (Collider other)
    {
        if (!hasSpawned)
        {
            tileSpawner.SpawnTile();
            hasSpawned = true;
            isJumpObstacle = false;
            Destroy(gameObject, 3f);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SpawnObstacle()
    {
        int obstacleSpawnIndex = Random.Range(2, 5);
        int obstacleIndex = (obstacleSpawnIndex == 3? 0:1);
        Transform spawnPoint = transform.GetChild(obstacleSpawnIndex).transform;
        
        GameObject ob = obstacles[obstacleIndex].CreateItems(spawnPoint.position, transform);
        isJumpObstacle = obstacleSpawnIndex == 3;
    }

    void SpawnCoin()
    {
        int number;
        if (isJumpObstacle)
        { number = Random.Range(0,2); }
        else
        { number = Random.Range(2,4); }

        string position = coinsPosition[number];
        Transform coinsTransform = transform.Find(position);
        coin.SpawnCoins(coinsTransform, transform);
    }
}
