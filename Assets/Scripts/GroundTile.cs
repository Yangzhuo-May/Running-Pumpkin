using Unity.VisualScripting;
using UnityEngine;

public class GroundTile : MonoBehaviour
{
    GroundSpawner groundSpawner;
    private bool hasSpawned = false;
    private bool JumpObstacle = false;
    private bool MoveObstacle = false;

    public int numberOfCoins = 6;

    public GameObject obstaclePrebabMove;
    public GameObject obstaclePrebabJump;
    public GameObject Coins;

    // Start is called before the first frame update
    void Start()
    {
        groundSpawner = GameObject.FindObjectOfType<GroundSpawner>();
        SpawnObstacle();
        SpawnCoin();
    }

    private void OnTriggerExit (Collider other)
    {
        if (!hasSpawned)
        {
            groundSpawner.SpawnTile();
            hasSpawned = true;
            JumpObstacle = false;
            MoveObstacle = false;
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
        Transform spawnPoint = transform.GetChild(obstacleSpawnIndex).transform;

        if (obstacleSpawnIndex == 3)
        {
            Instantiate(obstaclePrebabJump, spawnPoint.position, Quaternion.identity, transform);
            JumpObstacle = true;
        }
        else
        {
            Instantiate(obstaclePrebabMove, spawnPoint.position, Quaternion.identity, transform);
            MoveObstacle = true;
        }

    }

    void SpawnCoin()
    {
        if (JumpObstacle)
        {
            int number = Random.Range(1,3);
            
            if (number == 2)
            {
                Transform coinsTransform = transform.Find("CoinJumpLeft");
                for (int i = 0; i < 6; i++)
                {
                    Transform spawnPoint = coinsTransform.GetChild(i).transform;
                    Instantiate(Coins, spawnPoint.position, Coins.transform.rotation, transform);
                }
            }
            else if (number == 1)
            {
                Transform coinsTransform = transform.Find("CoinJumpRight");
                for (int i = 0; i < 6; i++)
                {
                    Transform spawnPoint = coinsTransform.GetChild(i).transform;
                    Instantiate(Coins, spawnPoint.position, Coins.transform.rotation, transform);
                }
            }
        }
        else if (MoveObstacle)
        {
            int number = Random.Range(1,3);
            
            if (number == 2)
            {
                Transform coinsTransform = transform.Find("CoinLeft");
                for (int i = 0; i < 6; i++)
                {
                    Transform spawnPoint = coinsTransform.GetChild(i).transform;
                    Instantiate(Coins, spawnPoint.position, Coins.transform.rotation, transform);
                }
            }
            else if (number == 1)
            {
                Transform coinsTransform = transform.Find("CoinRight");
                for (int i = 0; i < 6; i++)
                {
                    Transform spawnPoint = coinsTransform.GetChild(i).transform;
                    Instantiate(Coins, spawnPoint.position, Coins.transform.rotation, transform);
                }
            }
        }
    }
}
